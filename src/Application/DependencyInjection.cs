﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UexCorpDataRunner.Application.Common;
using UexCorpDataRunner.Application.ViewModels;
using CommunityToolkit.Mvvm.Messaging;

namespace UexCorpDataRunner.Application;
public static class DependencyInjection
{
    public static void RegisterDependencyInjectionTypes(IServiceCollection services)
    {
        //services.AddSingleton<IMessenger, Messenger>();
        services.AddSingleton<IMessenger, WeakReferenceMessenger>();
        services.AddSingleton<MainViewModel>();
        services.AddSingleton<MinimizedViewModel>();
        services.AddSingleton<SettingsViewModel>();
    }
}