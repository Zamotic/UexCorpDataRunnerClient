using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UexCorpDataRunner.Application.Common;
using UexCorpDataRunner.Application.ViewModels;
using CommunityToolkit.Mvvm.Messaging;
using System.Reflection;
using UexCorpDataRunner.Domain.Services;
using UexCorpDataRunner.Application.Services;

namespace UexCorpDataRunner.Application;
public static class StartupExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        //services.AddSingleton<IMessenger, Messenger>();
        services.AddSingleton<IMessenger, WeakReferenceMessenger>();
        services.AddSingleton<MainViewModel>();
        services.AddSingleton<MinimizedViewModel>();
        services.AddSingleton<SettingsViewModel>();

        services.AddSingleton<IUexDataService, UexDataService>();

        return services;
    }
}
