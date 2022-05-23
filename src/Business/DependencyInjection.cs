using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UexCorpDataRunner.Business.Settings;

namespace UexCorpDataRunner.Business;
public static class DependencyInjection
{
    public static void RegisterDependencyInjectionTypes(IServiceCollection services)
    {
        services.AddSingleton<ISettingsService, SettingsService>();

    }
}
