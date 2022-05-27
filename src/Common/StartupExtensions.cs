using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using AppLogging = UexCorpDataRunner.Common.Logging;

namespace UexCorpDataRunner.Common;
public static class StartupExtensions
{
    public static IServiceCollection AddCommon(this IServiceCollection services)
    {
        services.AddLogger();

        return services;
    }

    private static IServiceCollection AddLogger(this IServiceCollection services)
    {
        var sp = services.BuildServiceProvider();

        var configuration = sp.GetService<IConfiguration>();

        services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));
        var SerilogLogger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
        services.AddSingleton<AppLogging.ILogger>(new AppLogging.Logger(SerilogLogger));

        return services;
    }
}
