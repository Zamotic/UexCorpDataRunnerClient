using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UexCorpDataRunner.Application.DataRunner;
using UexCorpDataRunner.Application.Settings;
using UexCorpDataRunner.Domain.Services;

namespace UexCorpDataRunner.Application;
public static class StartupExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<ISettingsService, SettingsService>();
        services.AddScoped<ICommodityWrapperToPriceReportConverter, CommodityWrapperToPriceReportConverter>();
        services.AddScoped<IPriceReportSubmitter, PriceReportSubmitter>();
        services.AddScoped<ITradeportCommodityBuilder, TradeportCommodityBuilder>();
        services.AddScoped<DataRunnerV2.ITerminalCommodityBuilder, DataRunnerV2.TerminalCommodityBuilder>();

        return services;
    }
}
