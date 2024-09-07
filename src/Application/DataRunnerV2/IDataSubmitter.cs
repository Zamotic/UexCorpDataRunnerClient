using System.Collections.Concurrent;

namespace UexCorpDataRunner.Application.DataRunnerV2;
public interface IDataSubmitter
{
    Task<Dictionary<string, bool>> SubmitAllReports(IEnumerable<CommodityWrapper> commodities, int terminalId, ConcurrentQueue<string> statusBufferQueue);
}