using Microsoft.Extensions.DependencyInjection;
using UexCorpDataRunner.Application.DataRunner;
using UexCorpDataRunner.Application.DataRunnerV2;
using UexCorpDataRunner.Application.Settings;
using UexCorpDataRunner.Domain.Services;

namespace UexCorpDataRunner.Application;
public static class StartupExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<ISettingsService, SettingsService>();
        services.AddScoped<ICommodityWrapperToPriceReportConverter, CommodityWrapperToPriceReportConverter>();
        services.AddScoped<ICommodityWrapperToDataSubmitConverter, CommodityWrapperToDataSubmitConverter>();
        services.AddScoped<IPriceReportSubmitter, PriceReportSubmitter>();
        services.AddScoped<IDataSubmitter, DataSubmitter>();
        services.AddScoped<ITradeportCommodityBuilder, TradeportCommodityBuilder>();
        services.AddScoped<ITerminalCommodityBuilder, TerminalCommodityBuilder>();

        return services;
    }
}
