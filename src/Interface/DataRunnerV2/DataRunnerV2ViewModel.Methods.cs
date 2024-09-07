using UexCorpDataRunner.Application.DataRunnerV2;
using UexCorpDataRunner.Domain.DataRunnerV2;

namespace UexCorpDataRunner.Interface.DataRunnerV2;
public partial class DataRunnerV2ViewModel
{
    public async Task UpdateCommoditiesForTerminal(int? terminalId)
    {
        if (terminalId is null)
        {
            return;
        }

        if (terminalId < 1)
        {
            return;
        }

        if (_commodityList is null)
        {
            return;
        }

        
        ClearCommodities();

        IList<CommodityWrapper> commodities = await _TerminalCommodityBuilder.BuildCommodityListAsync(terminalId.Value, _commodityList);

        Commodities = commodities;
    }
}
