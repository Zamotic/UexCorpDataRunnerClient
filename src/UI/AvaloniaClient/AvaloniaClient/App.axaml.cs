using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using UexCorpDataRunner.Interface;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using UexCorpDataRunner.Common.Logging;
using UexCorpDataRunner.Common;
using UexCorpDataRunner.Persistence.Api;
using UexCorpDataRunner.Application;

namespace AvaloniaClient;
public partial class App : Application
{
    public static IServiceProvider? ServiceProvider { get; private set; }
    public static IConfiguration? Configuration { get; private set; }
    public static ILogger? Logger { get; private set; }

    //public static Skin Skin { get; set; } = Skin.Dark;

    public App()
    {
        Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        ServiceCollection services = new ServiceCollection();

        ConfigureServices(services);

        ServiceProvider = services.BuildServiceProvider();
        //SetupThemeChanges();
    }

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainViewModel()
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {
                DataContext = new MainViewModel()
            };
        }

        base.OnFrameworkInitializationCompleted();
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
#if DEBUG
                .AddPersistenceApiMock()
#endif
                .AddPersistenceApi()
                //.AddPresentation()
                .AddInterface()
                .AddApplication();

        services.AddSingleton<MainWindow>();
    }
}