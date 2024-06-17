using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;
using GrabAndScanPoC.Interface;
using GrabAndScanPoC.Imaging;
using Microsoft.VisualBasic.Logging;
using System.Diagnostics;

namespace GrabAndScanPoC.Presentation;
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public static IServiceProvider? ServiceProvider { get; private set; }
    public static IConfiguration? Configuration { get; private set; }

    private static TraceSource ts =
            new TraceSource("Tesseract");

    public App()
    {
        SourceSwitch sourceSwitch = new SourceSwitch("Tesseract", "Verbose");
        ts.Switch = sourceSwitch;
        int idxConsole = ts.Listeners.Add(new ConsoleTraceListener());
        var fileListener = new FileLogTraceListener();
        idxConsole = ts.Listeners.Add(fileListener);

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

        //Logger?.Information("Configuring Services");

        services.AddImaging()
                .AddInterface()
                .AddPresentation();

        services.AddSingleton<MainWindow>();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        try
        {
            //Logger?.Information("Starting up");
            base.OnStartup(e);
            if (ServiceProvider is null)
            {
                throw new Exception("ServiceProvider was not properly loaded");
            }

            //EventManager.RegisterClassHandler(typeof(TextBox), UIElement.KeyDownEvent, new KeyEventHandler(UIElement_KeyDown));
            //EventManager.RegisterClassHandler(typeof(ComboBox), UIElement.KeyDownEvent, new KeyEventHandler(UIElement_KeyDown));

            var messenger = ServiceProvider.GetRequiredService<IMessenger>();
            if (messenger is null)
            {
                throw new Exception("IMessenger service was not properly loaded");
            }

            messenger.Send(new Core.Messengers.ServiceProviderBuiltMessage(ServiceProvider));

            //var settingsService = ServiceProvider.GetRequiredService<ISettingsService>();
            //if (settingsService is not null)
            //{
            //    settingsService.LoadSettings();
            //}

            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();

            if (mainWindow is null)
            {
                throw new Exception($"Main Window could not be resolved!");
            }

            mainWindow.Show();
        }
        catch (Exception ex)
        {
            //Logger?.Fatal(ex, "Unhandled exception");
        }
        finally
        {
            //Logger?.Information("Shut down complete");
        }
    }
}

