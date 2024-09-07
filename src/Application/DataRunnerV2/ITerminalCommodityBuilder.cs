using UexCorpDataRunner.Domain.DataRunnerV2;

namespace UexCorpDataRunner.Application.DataRunnerV2;
public interface ITerminalCommodityBuilder
{
    Task<IList<CommodityWrapper>> BuildCommodityListAsync(int terminalId, IReadOnlyCollection<Commodity> commodityCollection);
}