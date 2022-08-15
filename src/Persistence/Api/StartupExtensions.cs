using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
using System.Reflection;
using UexCorpDataRunner.Domain.Services;
using UexCorpDataRunner.Persistence.Api.Common;
using UexCorpDataRunner.Persistence.Api.Uex;

namespace UexCorpDataRunner.Persistence.Api;
public static class StartupExtensions
{
    public static IServiceCollection AddPersistenceApi(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly())
                .AddUexCorpWebApiConfiguration()
                .AddSingleton<HttpLoggingHandler>()
                .ConfigureAll<HttpClientFactoryOptions>(options =>
                {
                    options.HttpMessageHandlerBuilderActions.Add(builder =>
                    {
                        builder.AdditionalHandlers.Insert(0,builder.Services.GetRequiredService<HttpLoggingHandler>());
                        //builder.AdditionalHandlers.Add(builder.Services.GetRequiredService<HttpLoggingHandler>());
                    });
                })
                .AddSingleton<IUexCorpWebApiClientAdapter, UexCorpWebApiClientAdapter>()
                .AddSingleton<IUexDataService, UexCacheDataService>()
                //.TryAddSingleton<IUexCorpWebApiClient, UexCorpWebApiClient>()
                .AddHttpClient<IUexCorpWebApiClient, UexCorpWebApiClient>()
                //.AddHttpMessageHandler<HttpLoggingHandler>()
                ;

        return services;
    }

    private static IServiceCollection AddUexCorpWebApiConfiguration(this IServiceCollection services)
    {
        var sp = services.BuildServiceProvider();

        var configuration = sp.GetService<IConfiguration>();

        if(configuration is null)
        {
            throw new Exception("Configuration cannot be null.");
        }

        services.AddSingleton<IUexCorpWebApiConfiguration>(configuration.GetSection(UexCorpWebApiConfiguration.ConfigurationSectionName).Get<UexCorpWebApiConfiguration>());

        return services;
    }
}
