using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using UexCorpDataRunner.Application;
using UexCorpDataRunner.Common;
using UexCorpDataRunner.Common.Logging;
using UexCorpDataRunner.Domain.Services;
using UexCorpDataRunner.Interface;
using UexCorpDataRunner.Persistence.Api;
using UexCorpDataRunner.Presentation;

namespace UexCorpDataRunner.DesktopClient;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : System.Windows.Application
{
    public static IServiceProvider? ServiceProvider { get; private set; }
    public static IConfiguration? Configuration { get; private set; }
    public static ILogger? Logger { get; private set; }

    public App()
    {
        Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        ServiceCollection services = new ServiceCollection();

        ConfigureServices(services);

        ServiceProvider = services.BuildServiceProvider();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        if (Configuration is null)
        {
            throw new Exception($"{nameof(Configuration)} cannot be null.");
        }

        services.AddSingleton<IConfiguration>(Configuration);
        services.AddSingleton<IMessenger>(new WeakReferenceMessenger());

        Logger?.Information("Configuring Services");

        services.AddCommon()
                .AddPersistenceApi()
                .AddPresentation()
                .AddInterface()
                .AddApplication();

        services.AddSingleton<MainWindow>();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        try
        {
            Logger?.Information("Starting up");
            base.OnStartup(e);
            if (ServiceProvider is null)
            {
                throw new Exception("ServiceProvider was not properly loaded");
            }

            var messenger = ServiceProvider.GetRequiredService<IMessenger>();
            if (messenger is null)
            {
                throw new Exception("IMessenger service was not properly loaded");
            }

            messenger.Send(new Interface.MessengerMessages.ServiceProviderBuiltMessage(ServiceProvider));

            var settingsService = ServiceProvider.GetRequiredService<ISettingsService>();
            if (settingsService is not null)
            {
                settingsService.LoadSettings();
            }

            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();

            if (mainWindow is null)
            {
                throw new Exception($"Main Window could not be resolved!");
            }

            mainWindow.Show();
        }
        catch (Exception ex)
        {
            Logger?.Fatal(ex, "Unhandled exception");
        }
        finally
        {
            Logger?.Information("Shut down complete");
        }
    }
}
