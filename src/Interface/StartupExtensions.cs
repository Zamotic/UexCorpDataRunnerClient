using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using UexCorpDataRunner.Domain.Services;
using UexCorpDataRunner.Interface.DataRunner;
using UexCorpDataRunner.Interface.DataRunnerV2;
using UexCorpDataRunner.Interface.Minimized;
using UexCorpDataRunner.Interface.Settings;

namespace UexCorpDataRunner.Interface;
public static class StartupExtensions
{
    public static IServiceCollection AddInterface(this IServiceCollection services)
    {
        services.AddSingleton<TransmissionStatusViewModel>();
        services.AddSingleton<ReleaseNotesViewModel>();
        //services.AddSingleton<DataRunnerViewModel>();
        services.AddSingleton<DataRunnerV2ViewModel>();
        services.AddSingleton<MinimizedViewModel>();
        services.AddSingleton<SettingsViewModel>();
        services.AddSingleton<MainViewModel>();

        return services;
    }
}
