using UexCorpDataRunner.DesktopClient.Views;
using UexCorpDataRunner.DesktopClient.ViewModels;
using UexCorpDataRunner.DesktopClient.Services;

namespace UexCorpDataRunner.DesktopClient;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        //builder.Services.AddTransient<MainPage>();
        //builder.Services.AddTransient<MainPageViewModel>();

        builder.Services.AddTransient<ActiveInterfaceView>();
        builder.Services.AddTransient<ActiveInterfaceViewModel>();

        builder.Services.AddTransient<HiddenInterfaceView>();
        builder.Services.AddTransient<HiddenInterfaceViewModel>();

        builder.Services.AddSingleton<INavigationService, NavigationService>();

        return builder.Build();
    }
}
