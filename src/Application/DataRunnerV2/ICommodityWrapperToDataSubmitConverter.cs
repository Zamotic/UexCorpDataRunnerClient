using UexCorpDataRunner.Domain.DataRunnerV2;

namespace UexCorpDataRunner.Application.DataRunnerV2;
public interface ICommodityWrapperToDataSubmitConverter
{
    DataSubmit Convert(IEnumerable<CommodityWrapper> commodities, int terminalId);
}
