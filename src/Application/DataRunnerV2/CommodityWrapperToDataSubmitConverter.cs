using UexCorpDataRunner.Domain.DataRunnerV2;
using UexCorpDataRunner.Domain.Services;
using System;

namespace UexCorpDataRunner.Application.DataRunnerV2;

public class CommodityWrapperToDataSubmitConverter : ICommodityWrapperToDataSubmitConverter
{
    ISettingsService _SettingsService;

    public CommodityWrapperToDataSubmitConverter(ISettingsService settingsService)
    {
        _SettingsService = settingsService;
    }

    public DataSubmit Convert(IEnumerable<CommodityWrapper> commodities, int terminalId)
    {
        DataSubmit dataSubmit = new DataSubmit();

        dataSubmit.TerminalId = terminalId;
#if DEBUG
        dataSubmit.IsProduction = false;
        //dataSubmit.IsProduction = true;
#else
        dataSubmit.IsProduction = true;
#endif
        dataSubmit.GameVersion = GetVersion(_SettingsService);

        foreach(var commodity in commodities)
        {
            var dataSubmitPrice = GetDataSubmitPrice(commodity);
            dataSubmit.DataSubmitPrices.Add(dataSubmitPrice);
        }

        return dataSubmit;
    }

    private DataSubmitPrice GetDataSubmitPrice(CommodityWrapper commodity)
    {
        DataSubmitPrice dataSubmitPrice = new DataSubmitPrice();
        dataSubmitPrice.CommodityId = commodity.Id;

        if(commodity.Operation == OperationType.Buy)
        {
            dataSubmitPrice.BuyPrice = GetCurrentPrice(commodity);
            dataSubmitPrice.BuyScu = GetCurrentScu(commodity);
            dataSubmitPrice.BuyStatus = GetCurrentStatus(commodity);
            return dataSubmitPrice;
        }

        dataSubmitPrice.SellPrice = GetCurrentPrice(commodity);
        dataSubmitPrice.SellScu = GetCurrentScu(commodity);
        dataSubmitPrice.SellStatus = GetCurrentStatus(commodity);

        return dataSubmitPrice;
    }

    private float GetCurrentPrice(CommodityWrapper commodity)
    {
        if(commodity.CurrentPrice.HasValue == true)
        {
            return System.Convert.ToSingle(commodity.CurrentPrice);
        }

        return 0;
    }

    private int GetCurrentScu(CommodityWrapper commodity)
    {
        if(commodity.CurrentScu.HasValue == true)
        {
            return commodity.CurrentScu.Value;
        }

        return 0;
    }
    private int GetCurrentStatus(CommodityWrapper commodity)
    {
        return commodity.CurrentStatus;
    }

    private string GetUserSecretKey(ISettingsService settingsService)
    {
        if(settingsService is null)
        {
            return "xxxxxx";
        }
        if (settingsService.Settings is null)
        {
            return "xxxxxx";
        }
        return settingsService.Settings.UserSecretKey;
    }

    private string GetVersion(ISettingsService settingsService)
    {
        if (settingsService is null)
        {
            return "xxxxxx";
        }
        if (settingsService.Settings is null)
        {
            return "xxxxxx";
        }
        return settingsService.Settings.CurrentApiGameVersion;
    }
}