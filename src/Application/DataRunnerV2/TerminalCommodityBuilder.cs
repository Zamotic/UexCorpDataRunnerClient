using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UexCorpDataRunner.Domain;
using UexCorpDataRunner.Domain.DataRunnerV2;
using UexCorpDataRunner.Domain.Services;

namespace UexCorpDataRunner.Application.DataRunnerV2;
public class TerminalCommodityBuilder : ITerminalCommodityBuilder
{
    private readonly IUexDataServiceV2 _UexDataService;
    private readonly ISettingsService _SettingsService;

    public TerminalCommodityBuilder(IUexDataServiceV2 uexDataService, ISettingsService settingsService)
    {
        _UexDataService = uexDataService;
        _SettingsService = settingsService;
    }

    public async Task<IList<CommodityWrapper>> BuildCommodityListAsync(int terminalId, IReadOnlyCollection<Commodity> commodityCollection)
    {
        var currentCommodityPrices = await _UexDataService.GetCommodityPricesAsync(terminalId);

        List<CommodityWrapper> commodities = new List<CommodityWrapper>();
        foreach (var currentPrice in currentCommodityPrices)
        {
            var commodityWrapper = CreateCommodityWrapper(currentPrice,  commodityCollection);

            if(commodityWrapper is null)
            {
                continue;
            }

            commodities.Add(commodityWrapper);
        }

        return commodities;
    }

    private CommodityWrapper? CreateCommodityWrapper(CommodityPrice commodityPrice, IReadOnlyCollection<Commodity> commodityCollection)
    {
        if(commodityPrice is null)
        {
            return null;
        }

        if (commodityCollection.Any(x => x.Id == commodityPrice.CommodityId) == false)
        {
            return null;
        }

        var locatedCommodity = commodityCollection.First(x => x.Id == commodityPrice.CommodityId);

        if (locatedCommodity.IsIllegal == true)
        {
            return null;
        }

        if (_SettingsService?.Settings?.ShowTemporaryCommodities == Globals.Settings.HideTemporary)
        {
            if (locatedCommodity.IsTemporary == true)
            {
                return null;
            }
        }

        if (locatedCommodity.IsAvailable == false)
        {
            return null;
        }

        if (locatedCommodity.IsVisible == false)
        {
            return null;
        }

        var commodityWrapper = new CommodityWrapper(locatedCommodity, commodityPrice);
        return commodityWrapper;
    }
}
