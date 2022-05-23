using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using UexCorpDataRunner.Application.Common;
using UexCorpDataRunner.Application.MessengerMessages;
using CommunityToolkit.Mvvm.Messaging;

namespace UexCorpDataRunner.DesktopClient.Views;

/// <summary>
/// Interaction logic for MainPage.xaml
/// </summary>
public partial class MainView : UserControl
{
    Point _Location;

    public MainView()
    {
        InitializeComponent();
    }

    //public void ShowUserInterfaceNotified(ShowUserInterfaceMessage notification)
    public void ShowUserInterfaceMessageHandler(object sender, ShowUserInterfaceMessage notification)
    {
        double x = _Location.X;
        double y = _Location.Y;

        Window window = System.Windows.Application.Current.MainWindow;
        window.Visibility = Visibility.Hidden;
        window.WindowStyle = WindowStyle.SingleBorderWindow;
        window.Width = 425;
        window.Height = 700;
        window.Left = x;
        window.Top = y;
        window.ResizeMode = ResizeMode.CanResize;
        window.Topmost = true;
        window.Visibility = Visibility.Visible;
    }

    //public void WindowMovedNotified(WindowMovedMessage notification)
    public void WindowMovedMessageHandler(object sender, WindowMovedMessage notification)
    {
        SaveWindowLocation();
    }

    private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        IMessenger? messenger = App.ServiceProvider?.GetService<IMessenger>();
        messenger?.Register<ShowUserInterfaceMessage>(this, ShowUserInterfaceMessageHandler);
        messenger?.Register<WindowMovedMessage>(this, WindowMovedMessageHandler);
        SaveWindowLocation();
    }

    public void SaveWindowLocation()
    {
        Window window = System.Windows.Application.Current.MainWindow;
        if (window.WindowStyle == WindowStyle.SingleBorderWindow)
        {
            _Location = new Point(window.Left, window.Top);
        }
    }
}
