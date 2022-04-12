using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Windows;
using UexCorpDataRunner.DesktopClient.Core;
using UexCorpDataRunner.DesktopClient.Settings;
using UexCorpDataRunner.DesktopClient.ViewModels;
using UexCorpDataRunner.DesktopClient.Views;

namespace UexCorpDataRunner.DesktopClient;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public static IServiceProvider? ServiceProvider { get; private set; }
    public static IConfiguration? Configuration { get; private set; }
    public static Serilog.ILogger Logger { get; private set; }

    public App()
    {
        Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        ServiceCollection services = new ServiceCollection();

        Logger = new LoggerConfiguration().ReadFrom.Configuration(Configuration).CreateLogger();

        ConfigureServices(services);

        ServiceProvider = services.BuildServiceProvider();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        Logger.Information("Configuring Services");

        if (Configuration is null)
        {
            throw new Exception($"{nameof(Configuration)} cannot be null.");
        }

        services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));

        services.AddSingleton<Core.ILogger, Logger>();

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
        try
        {
            Logger.Information("Starting up");
            base.OnStartup(e);

            var settingsService = ServiceProvider?.GetRequiredService<ISettingsService>();
            if (settingsService is not null)
            {
                settingsService.LoadSettings();
            }

            var mainWindow = ServiceProvider?.GetRequiredService<MainWindow>();

            if (mainWindow is null)
            {
                throw new Exception($"Main Window could not be resolved!");
            }

            mainWindow.Show();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Unhandled exception");
        }
        finally
        {
            Log.Information("Shut down complete");
            Log.CloseAndFlush();
        }
    }
}
