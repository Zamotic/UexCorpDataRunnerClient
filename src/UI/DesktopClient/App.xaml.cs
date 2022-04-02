using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UexCorpDataRunner.DesktopClient.Core;
using UexCorpDataRunner.DesktopClient.ViewModels;
using UexCorpDataRunner.DesktopClient.Views;
using UexCorpDataRunner.DesktopClient.Notifications;
using UexCorpDataRunner.DesktopClient.Settings;

namespace UexCorpDataRunner.DesktopClient;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public static IServiceProvider? ServiceProvider { get; private set; }
    public static IConfiguration? Configuration { get; private set; }

    public App()
    {
        Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        ServiceCollection services = new ServiceCollection();

        ConfigureServices(services);

        ServiceProvider = services.BuildServiceProvider();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        if(Configuration is null)
        {
            throw new Exception($"{nameof(Configuration)} cannot be null.");
        }

        services.AddSingleton(Configuration);

        services.AddSingleton<ISettingsService, SettingsService>();

        services.AddSingleton<IMessenger, Messenger>();

        services.AddSingleton<MainWindow>();

        services.AddSingleton<MainView>();
        services.AddSingleton<MainViewModel>();

        services.AddSingleton<MinimizedView>();
        services.AddSingleton<MinimizedViewModel>();

        services.AddSingleton<SettingsView>();
        services.AddSingleton<SettingsViewModel>();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var settingsService = ServiceProvider?.GetRequiredService<ISettingsService>();
        if(settingsService is not null)
        {
            settingsService.LoadSettings();
        }

        var mainWindow = ServiceProvider?.GetRequiredService<MainWindow>();

        if(mainWindow is null)
        {
            throw new Exception($"Main Window could not be resolved!");
        }

        mainWindow.Show();
    }
}
