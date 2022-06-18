﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
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
                .AddSingleton<IUexCorpWebApiClientAdapter, UexCorpWebApiClientAdapter>()
                .AddSingleton<IUexDataService, UexCacheDataService>()
                .AddSingleton<IUexCorpWebApiClient, UexCorpWebApiClient>()
                .TryAddSingleton<IHttpClientFactory, UexHttpClientFactory>()
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
