using Microsoft.Extensions.DependencyInjection;
using UexCorpDataRunner.Persistence.Api.Mock.Uex;
using UexCorpDataRunner.Persistence.Api.Uex;

namespace UexCorpDataRunner.Persistence.Api;
public static class StartupExtensions
{
    public static IServiceCollection AddPersistenceApiMock(this IServiceCollection services)
    {
        services.AddSingleton<IUexCorpWebApiClient, UexCorpWebApiClientMock>()
                ;

        return services;
    }
}
