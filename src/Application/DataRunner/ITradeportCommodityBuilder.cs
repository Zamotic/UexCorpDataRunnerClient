using UexCorpDataRunner.Domain.DataRunner;

namespace UexCorpDataRunner.Application.DataRunner;
public interface ITradeportCommodityBuilder
{
    Task<IList<CommodityWrapper>> BuildCommodityListAsync(string tradeportCode, IReadOnlyCollection<Commodity> commodityCollection);
}