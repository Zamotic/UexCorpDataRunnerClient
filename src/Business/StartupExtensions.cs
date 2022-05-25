using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UexCorpDataRunner.Business.Settings;

namespace UexCorpDataRunner.Business;
public static class StartupExtensions
{
    public static IServiceCollection AddBusiness(this IServiceCollection services)
    {
        services.AddSingleton<ISettingsService, SettingsService>();
        
        return services;
    }
}
