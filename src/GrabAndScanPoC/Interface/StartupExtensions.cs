using Microsoft.Extensions.DependencyInjection;

namespace GrabAndScanPoC.Interface;
public static class StartupExtensions
{
    public static IServiceCollection AddInterface(this IServiceCollection services)
    {
        services.AddSingleton<MainViewModel>();

        return services;
    }
}
