using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using UexCorpDataRunner.DesktopClient.Core;
using UexCorpDataRunner.DesktopClient.Notifications;
using UexCorpDataRunner.DesktopClient.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace UexCorpDataRunner.DesktopClient.Views;

/// <summary>
/// Interaction logic for MainPage.xaml
/// </summary>
public partial class MainView : UserControl
{

    public MainView()
    {
        InitializeComponent();
    }

    public void ShowUserInterfaceNotified(ShowUserInterfaceNotification notification)
    {
        Window window = Application.Current.MainWindow;
        window.WindowStyle = WindowStyle.SingleBorderWindow;
        window.Width = 350;
        window.Height = 700;
        window.ResizeMode = ResizeMode.CanResize;
        window.Topmost = true;
    }

    private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        IMessenger? messenger = App.ServiceProvider?.GetService<IMessenger>();
        messenger?.Register<ShowUserInterfaceNotification>(this, ShowUserInterfaceNotified);
    }
}
