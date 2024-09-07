using System.Collections.Concurrent;
using System.Text;
using UexCorpDataRunner.Application.DataRunnerV2;
using UexCorpDataRunner.Domain.DataRunnerV2;
using UexCorpDataRunner.Domain.Services;

namespace UexCorpDataRunner.Application.DataRunnerV2;
public class DataSubmitter : IDataSubmitter
{
    private IUexDataServiceV2 _uexDataService;
    private ICommodityWrapperToDataSubmitConverter _converter;

    public DataSubmitter(IUexDataServiceV2 uexDataService, ICommodityWrapperToDataSubmitConverter converter)
    {
        _uexDataService = uexDataService;
        _converter = converter;
    }

    public async Task<Dictionary<string, bool>> SubmitAllReports(IEnumerable<CommodityWrapper> commodities, int terminalId, ConcurrentQueue<string> statusBufferQueue)
    {
        Dictionary<string, bool> responses = new Dictionary<string, bool>();

        if(commodities.Any(x => x.MarkedForSubmittal) == false)
        {
            return new Dictionary<string, bool>();
        }

        var commoditiesToSubmit = commodities.Where(x => x.MarkedForSubmittal).OrderBy(x => x.Operation).ThenBy(x => x.Name).ToArray();

        DataSubmit dataToSubmit = _converter.Convert(commoditiesToSubmit, terminalId);

        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"Uploading {dataToSubmit.DataSubmitPrices.Count} reports:");
        foreach(CommodityWrapper commodity in commoditiesToSubmit)
        {
            sb.AppendLine($"[{commodity.Operation}] {commodity.Name} ({commodity.Code}) at {commodity.CurrentPrice}...");
        }

        statusBufferQueue.Enqueue(sb.ToString());
        statusBufferQueue.Enqueue("Sending...");

        var response = await _uexDataService.SubmitDataAsync(dataToSubmit).ConfigureAwait(false);

        string responseLine;
        if (response is null)
        {
            statusBufferQueue.Enqueue("No Response Received");
            return new Dictionary<string, bool>();
        }
        if(response.Response == false)
        {
            statusBufferQueue.Enqueue("Failed!\n");
            statusBufferQueue.Enqueue($"{"??"} out of {commoditiesToSubmit.Length} successfully saved.");
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