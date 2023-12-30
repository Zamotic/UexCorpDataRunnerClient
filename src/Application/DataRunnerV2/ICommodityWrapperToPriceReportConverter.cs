using UexCorpDataRunner.Domain.DataRunnerV2;

namespace UexCorpDataRunner.Application.DataRunnerV2;
public interface ICommodityWrapperToPriceReportConverter
{
    PriceReport Convert(CommodityWrapper commodity, string tradeportCode);
}
