using UexCorpDataRunner.DesktopClient.Services;
using UexCorpDataRunner.DesktopClient.Views;

namespace UexCorpDataRunner.DesktopClient;

public partial class App : Application
{
    public App(ActiveInterfaceView activeInterfaceView)
    {
        InitializeComponent();

        MainPage = new NavigationPage(activeInterfaceView);
    }

}
