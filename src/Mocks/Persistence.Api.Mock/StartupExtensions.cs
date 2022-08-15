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
                .ConfigureAll<HttpClientFactoryOptions>(options =>
                {
                    options.HttpMessageHandlerBuilderActions.Add(builder =>
                    {
                        builder.AdditionalHandlers.Add(builder.Services.GetRequiredService<UexWebApiMockHttpMessageHandler>());
                    });
                })
                //.AddSingleton<IHttpClientFactory, UexMockHttpClientFactory>()
                //AddSingleton<IUexCorpWebApiClient, UexCorpWebApiClientMock>()
                ;

        return services;
    }
}
