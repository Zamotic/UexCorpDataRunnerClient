using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UexCorpDataRunner.Application.Settings;
using UexCorpDataRunner.Domain.Services;

namespace UexCorpDataRunner.Application;
public static class StartupExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<ISettingsService, SettingsService>();
        
        return services;
    }
}
