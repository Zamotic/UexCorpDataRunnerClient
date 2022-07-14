using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UexCorpDataRunner.Domain;
using UexCorpDataRunner.Domain.DataRunner;
using UexCorpDataRunner.Domain.Services;

namespace UexCorpDataRunner.Application.DataRunner;
public class TradeportCommodityBuilder : ITradeportCommodityBuilder
{
    private readonly IUexDataService _UexDataService;
    private readonly ISettingsService _SettingsService;

    public TradeportCommodityBuilder(IUexDataService uexDataService, ISettingsService settingsService)
    {
        _UexDataService = uexDataService;
        _SettingsService = settingsService;
    }

    public async Task<IList<CommodityWrapper>> BuildCommodityListAsync(string tradeportCode, IReadOnlyCollection<Commodity> commodityCollection)
    {
        var currentTradeport = await _UexDataService.GetTradeportAsync(tradeportCode);

        List<CommodityWrapper> commodities = new List<CommodityWrapper>();
        foreach (var tradeListingValue in currentTradeport.Prices)
        {
            var commodityWrapper = CreateCommodityWrapper(tradeListingValue,  commodityCollection);

            if(commodityWrapper is null)
            {
                continue;
            }

            commodities.Add(commodityWrapper);
        }

        return commodities;
    }

    private CommodityWrapper? CreateCommodityWrapper(TradeListing tradeListing, IReadOnlyCollection<Commodity> commodityCollection)
    {
        if(tradeListing is null)
        {
            return null;
        }

        if (string.IsNullOrEmpty(tradeListing.Code) == true)
        {
            return null;
        }

        if (IsTradeListingCodeInCommodityList(tradeListing.Code, commodityCollection) == false)
        {
            return null;
        }

        var locatedCommodity = GetCommodityFromCommodityCollection(tradeListing.Code, commodityCollection);

        if (locatedCommodity.Illegal == true)
        {
            return null;
        }

        if (locatedCommodity.Temporary == true)
        {
            if (_SettingsService?.Settings?.ShowTemporaryCommodities == Globals.Settings.HideTemporary)
            {
                return null;
            }
        }

        if (locatedCommodity.Available == false)
        {
            return null;
        }

        var commodityWrapper = new CommodityWrapper(locatedCommodity, tradeListing);
        return commodityWrapper;
    }


    private bool IsTradeListingCodeInCommodityList(string? tradeListingCode, IReadOnlyCollection<Commodity> commodityCollection)
    {
        if(string.IsNullOrEmpty(tradeListingCode) == true)
        {
            return false;
        }

        if (commodityCollection.Any(x => x.Code.Equals(tradeListingCode)) == true)
        {
            return true;
        }

        return false;
    }

    private Commodity GetCommodityFromCommodityCollection(string? tradeListingCode, IReadOnlyCollection<Commodity> commodityCollection)
    {
        Commodity commodity = commodityCollection.First(x => x.Code.Equals(tradeListingCode));
        return commodity;
    }
}
