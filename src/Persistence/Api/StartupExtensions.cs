using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
using System.Reflection;
using UexCorpDataRunner.Domain.Services;
using UexCorpDataRunner.Persistence.Api.Common;
using UexCorpDataRunner.Persistence.Api.Mappers;
using UexCorpDataRunner.Persistence.Api.Uex;
using UexCorpDataRunner.Persistence.Api.UexV2;

namespace UexCorpDataRunner.Persistence.Api;
public static class StartupExtensions
{
    public static IServiceCollection? AddPersistenceApi(this IServiceCollection services)
    {
        if(services is null)
        {
            return services;
        }

        services.AddAutoMapper(Assembly.GetExecutingAssembly())
                .AddUexCorpWebApiConfiguration()
                .AddTransient<HttpLoggingHandler>()
                .Configure<HttpClientFactoryOptions>("UEX1.0", options =>
                {
                    options.HttpMessageHandlerBuilderActions.Add(builder =>
                    {
                        builder.AdditionalHandlers.Insert(0, builder.Services.GetRequiredService<HttpLoggingHandler>());
                    });
                })
                .Configure<HttpClientFactoryOptions>("UEX2.0", options =>
                {
                    options.HttpMessageHandlerBuilderActions.Add(builder =>
                    {
                        builder.AdditionalHandlers.Insert(0, builder.Services.GetRequiredService<HttpLoggingHandler>());
                    });
                })
                .AddSingleton<Uex.IUexCorpWebApiClientAdapter, Uex.UexCorpWebApiClientAdapter>()
                .AddSingleton<UexV2.IUexCorpWebApiClientAdapter, UexV2.UexCorpWebApiClientAdapter>()
                .AddSingleton<IUexDataService, UexCacheDataService>()
                .AddSingleton<IUexDataServiceV2, UexCacheDataServiceV2>()
                .AddSingleton<IMapperV2, V2DtoMapper>()
                ;

        services.AddHttpClient<Uex.IUexCorpWebApiClient, Uex.UexCorpWebApiClient>("UEX1.0");
        services.AddHttpClient<UexV2.IUexCorpWebApiClient, UexV2.UexCorpWebApiClient>("UEX2.0");

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

        services.AddSingleton<Uex.IUexCorpWebApiConfiguration>(configuration.GetSection(Uex.UexCorpWebApiConfiguration.ConfigurationSectionName).Get<Uex.UexCorpWebApiConfiguration>());
        services.AddSingleton<UexV2.IUexCorpWebApiConfiguration>(configuration.GetSection(UexV2.UexCorpWebApiConfiguration.ConfigurationSectionName).Get<UexV2.UexCorpWebApiConfiguration>());

        return services;
    }
}
