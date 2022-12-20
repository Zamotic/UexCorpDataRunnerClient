using UexCorpDataRunner.Application.DataRunner;
using UexCorpDataRunner.Domain.DataRunner;
using UexCorpDataRunner.Domain.Services;

namespace UexCorpDataRunner.Application;

public class CommodityWrapperToPriceReportConverter : ICommodityWrapperToPriceReportConverter
{
    ISettingsService _SettingsService;

    public CommodityWrapperToPriceReportConverter(ISettingsService settingsService)
    {
        _SettingsService = settingsService;
    }

    public PriceReport Convert(CommodityWrapper commodity, string tradeportCode)
    {
        PriceReport priceReport = new PriceReport();
        priceReport.CommodityCode = commodity.Code;
        priceReport.TradeportCode = tradeportCode;
        priceReport.Price = GetCurrentPrice(commodity);
        priceReport.Operation = GetOperation(commodity);
        priceReport.AccessCode = GetUserAccessCode(_SettingsService);
        priceReport.Version = GetVersion(_SettingsService);

        return priceReport;
    }

    private string GetOperation(CommodityWrapper commodity)
    {
        if(commodity.Operation == OperationType.Buy)
        {
            return "buy";
        }
        return "sell";
    }

    private string GetCurrentPrice(CommodityWrapper commodity)
    {
        if(commodity.CurrentPrice.HasValue == true)
        {
            return commodity.CurrentPrice.Value.ToString();
        }

        return 0.ToString();
    }

    private string GetUserAccessCode(ISettingsService settingsService)
    {
        if(settingsService is null)
        {
            return "xxxxxx";
        }
        if (settingsService.Settings is null)
        {
            return "xxxxxx";
        }
        return settingsService.Settings.UserAccessCode;
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
        if(settingsService.Settings.SelectedGameVersion == GameVersion.PtuValue)
        {
            return settingsService.Settings.LoadedGameVersion!.Ptu!;
        }
        return settingsService.Settings.LoadedGameVersion!.Live;
    }
}