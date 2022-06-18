using System.Collections.Concurrent;
using UexCorpDataRunner.Application.DataRunner;
using UexCorpDataRunner.Domain.DataRunner;
using UexCorpDataRunner.Domain.Services;

namespace UexCorpDataRunner.Application;
public class PriceReportSubmitter : IPriceReportSubmitter
{
    private IUexDataService _uexDataService;
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
}