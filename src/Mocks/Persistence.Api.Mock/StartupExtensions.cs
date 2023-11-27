using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
using System.Reflection;
using UexCorpDataRunner.Persistence.Api.Common;
using UexCorpDataRunner.Persistence.Api.Mock.Uex;
using UexCorpDataRunner.Persistence.Api.Uex;

namespace UexCorpDataRunner.Persistence.Api.Mock;
public static class StartupExtensions
{
    public static IServiceCollection AddPersistenceApiMock(this IServiceCollection services)
    {
        services
                .AddSingleton<UexWebApiMockHttpMessageHandler>()
                .AddSingleton<UexV2.UexWebApiMockHttpMessageHandler>()
                .Configure<HttpClientFactoryOptions>("UEX1.0", options =>
                {
                    options.HttpMessageHandlerBuilderActions.Add(builder =>
                    {
                        builder.AdditionalHandlers.Add(builder.Services.GetRequiredService<UexWebApiMockHttpMessageHandler>());
                    });
                })
                .Configure<HttpClientFactoryOptions>("UEX2.0", options =>
                {
                    options.HttpMessageHandlerBuilderActions.Add(builder =>
                    {
                        builder.AdditionalHandlers.Add(builder.Services.GetRequiredService<UexV2.UexWebApiMockHttpMessageHandler>());
                    });
                })
                //.AddSingleton<IHttpClientFactory, UexMockHttpClientFactory>()
                //AddSingleton<IUexCorpWebApiClient, UexCorpWebApiClientMock>()
                ;

        return services;
    }
}
