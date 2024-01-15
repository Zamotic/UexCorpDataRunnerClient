using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using UexCorpDataRunner.Application;
using UexCorpDataRunner.Common;
using UexCorpDataRunner.Common.Logging;
using UexCorpDataRunner.Interface.Theme;
using UexCorpDataRunner.Domain.Services;
using UexCorpDataRunner.Interface;
using UexCorpDataRunner.Persistence.Api;
using UexCorpDataRunner.Persistence.Api.Mock;
using UexCorpDataRunner.Presentation;
using UexCorpDataRunner.Interface.MessengerMessages;
using System.Windows.Input;
using System.Windows.Controls;

namespace UexCorpDataRunner.DesktopClient;


/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : System.Windows.Application
{
    public static IServiceProvider? ServiceProvider { get; private set; }
    public static IConfiguration? Configuration { get; private set; }
    public static ILogger? Logger { get; private set; }

    public static Skin Skin { get; set; } = Skin.Dark;

    public App()
    {
        Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        ServiceCollection services = new ServiceCollection();

        ConfigureServices(services);

        ServiceProvider = services.BuildServiceProvider();
        SetupThemeChanges();
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
                .AddApplication()
#if DEBUG
//                .AddPersistenceApiMock()
#endif
                .AddPersistenceApi()
                .AddInterface()
                .AddPresentation();

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

            EventManager.RegisterClassHandler(typeof(TextBox), UIElement.KeyDownEvent, new KeyEventHandler(UIElement_KeyDown));
            EventManager.RegisterClassHandler(typeof(ComboBox), UIElement.KeyDownEvent, new KeyEventHandler(UIElement_KeyDown));

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

    private void SetupThemeChanges()
    {
        if(ServiceProvider is null)
        {
            return;
        }

        var messenger = ServiceProvider.GetService<IMessenger>();
        messenger?.Register<UexCorpDataRunner.Interface.MessengerMessages.ThemeChangedMessage>(this, ThemeChangedMessageHandler);
    }

    private void ThemeChangedMessageHandler(object sender, ThemeChangedMessage notification)
    {
        if(notification is null)
        {
            return;
        }

        ChangeSkin(notification.SelectedSkin);
    }

    public void ChangeSkin(Skin newSkin)
    {
        Skin = newSkin;

        foreach (ResourceDictionary dict in Resources.MergedDictionaries)
        {

            if (dict is SkinResourceDictionary skinDict)
                skinDict.UpdateSource();
            else
                dict.Source = dict.Source;
        }
    }

    void UIElement_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter) MoveToNextUIElement(e);
    }

    void MoveToNextUIElement(KeyEventArgs e)
    {
        // Creating a FocusNavigationDirection object and setting it to a
        // local field that contains the direction selected.
        FocusNavigationDirection focusDirection = FocusNavigationDirection.Next;

        // MoveFocus takes a TraveralReqest as its argument.
        TraversalRequest request = new TraversalRequest(focusDirection);

        // Gets the element with keyboard focus.
        UIElement elementWithFocus = Keyboard.FocusedElement as UIElement;

        // Change keyboard focus.
        if (elementWithFocus != null)
        {
            if (elementWithFocus.MoveFocus(request)) e.Handled = true;
        }
    }
}
