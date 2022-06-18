using UexCorpDataRunner.Domain.DataRunner;

namespace UexCorpDataRunner.Application.DataRunner;
public interface ICommodityWrapperToPriceReportConverter
{
    PriceReport Convert(CommodityWrapper commodity, string tradeportCode);
}
