using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Windows;
using UexCorpDataRunner.Business.Settings;
using UexCorpDataRunner.DesktopClient.Common;
using UexCorpDataRunner.DesktopClient.Views;

namespace UexCorpDataRunner.DesktopClient;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : System.Windows.Application
{
    public static IServiceProvider? ServiceProvider { get; private set; }
    public static IConfiguration? Configuration { get; private set; }
    public static Business.Common.ILogger? Logger { get; private set; }

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
        services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));
        var SerilogLogger = new LoggerConfiguration().ReadFrom.Configuration(Configuration).CreateLogger();
        Logger = new Logger(SerilogLogger);
        services.AddSingleton<Business.Common.ILogger>(Logger);

        Logger?.Information("Configuring Services");

        if (Configuration is null)
        {
            throw new Exception($"{nameof(Configuration)} cannot be null.");
        }

        UexCorpDataRunner.Business.DependencyInjection.RegisterDependencyInjectionTypes(services);
        UexCorpDataRunner.Application.DependencyInjection.RegisterDependencyInjectionTypes(services);

        services.AddSingleton(Configuration);

        services.AddSingleton<MainWindow>();

        services.AddSingleton<MainView>();

        services.AddSingleton<MinimizedView>();

        services.AddSingleton<SettingsView>();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        try
        {
            Logger?.Information("Starting up");
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
