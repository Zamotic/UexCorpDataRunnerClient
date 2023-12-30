using System.Collections.Concurrent;
using System.Text;
using UexCorpDataRunner.Application.DataRunnerV2;
using UexCorpDataRunner.Domain.DataRunnerV2;
using UexCorpDataRunner.Domain.Services;

namespace UexCorpDataRunner.Application.DataRunnerV2;
public class PriceReportSubmitter : IPriceReportSubmitter
{
    private IUexDataServiceV2 _uexDataService;
    private ICommodityWrapperToPriceReportConverter _converter;

    public PriceReportSubmitter(IUexDataService uexDataService, ICommodityWrapperToPriceReportConverter converter)
    {
        _uexDataService = uexDataService;
        _converter = converter;
    }

    public async Task<Dictionary<string, bool>> SubmitReports(IEnumerable<CommodityWrapper> commodities, string tradeportCode, ConcurrentQueue<string> statusBufferQueue)
    {
        Dictionary<string, bool> responses = new Dictionary<string, bool>();
        foreach (var commodity in commodities.OrderBy(x => x.Operation).ThenBy(x => x.Name))
        {
            if(commodity.MarkedForSubmittal == false)
            {
                continue;
            }

            var priceReport = _converter.Convert(commodity, tradeportCode);

            string statusLine = $"Uploading {commodity.Operation.ToString()} report for {commodity.Name} ({commodity.Code}) at {commodity.CurrentPrice}..."; //{commodity.Kind}
            statusBufferQueue.Enqueue(statusLine);
            var response = await _uexDataService.SubmitPriceReportAsync(priceReport).ConfigureAwait(false);

            string responseLine;
            if (response.Response == true)
            {
                responseLine = $"Success!\n";
                statusBufferQueue.Enqueue(responseLine);
                responses.Add(commodity.Code, true);
                continue;
            }

            responseLine = $"Failed!\nReason: {response.StatusMessage.Replace("_"," ")}\n";
            statusBufferQueue.Enqueue(responseLine);
            responses.Add(commodity.Code, false);
        }

        return responses;
    }

    public async Task<Dictionary<string, bool>> SubmitAllReports(IEnumerable<CommodityWrapper> commodities, string tradeportCode, ConcurrentQueue<string> statusBufferQueue)
    {
        Dictionary<string, bool> responses = new Dictionary<string, bool>();

        if(commodities.Any(x => x.MarkedForSubmittal) == false)
        {
            return new Dictionary<string, bool>();
        }

        var commoditiesToSubmit = commodities.Where(x => x.MarkedForSubmittal).OrderBy(x => x.Operation).ThenBy(x => x.Name).ToArray();

        List<PriceReport> priceReportsToSubmit = commoditiesToSubmit.Select(x => _converter.Convert(x, tradeportCode)).ToList();

        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"Uploading {priceReportsToSubmit.Count} the following reports:");
        foreach(CommodityWrapper commodity in commoditiesToSubmit)
        {
            sb.AppendLine($"[{commodity.Operation}] {commodity.Name} ({commodity.Code}) at {commodity.CurrentPrice}...");
        }

        statusBufferQueue.Enqueue(sb.ToString());
        statusBufferQueue.Enqueue("Sending...");

        var response = await _uexDataService.SubmitPriceReportsAsync(priceReportsToSubmit.ToArray()).ConfigureAwait(false);

        string responseLine;
        if (response is null)
        {
            statusBufferQueue.Enqueue("No Response Received");
            return new Dictionary<string, bool>();
        }
        if(response.Response == false)
        {
            statusBufferQueue.Enqueue("Failed!\n");
            statusBufferQueue.Enqueue($"{response.ReturnedDataList.Count} out of {commoditiesToSubmit.Length} successfully saved.");
            return new Dictionary<string, bool>();
        }
            
        responseLine = $"Success!\n";
        statusBufferQueue.Enqueue(responseLine);
        return new Dictionary<string, bool>(commoditiesToSubmit.Select(x => new KeyValuePair<string, bool>(x.Code, true)));        

        //    responseLine = $"Failed!\nReason: {response.StatusMessage.Replace("_", " ")}\n";
        //    statusBufferQueue.Enqueue(responseLine);
        //    responses.Add(commodity.Code, false);
        //}

        //return responses;
    }
}