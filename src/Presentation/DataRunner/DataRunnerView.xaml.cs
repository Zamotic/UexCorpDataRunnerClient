using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using UexCorpDataRunner.Application.Common;
using UexCorpDataRunner.Application.MessengerMessages;
using CommunityToolkit.Mvvm.Messaging;
using UexCorpDataRunner.Domain.Minimized;

namespace UexCorpDataRunner.Presentation.DataRunner;

/// <summary>
/// Interaction logic for MainPage.xaml
/// </summary>
public partial class DataRunnerView : UserControl
{
    Point _Location;

    public DataRunnerView()
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
        window.Width = MinimizedValues.MinimizedWidth;
        window.Height = MinimizedValues.MinimizedHeight;
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
        IMessenger? messenger = StartupExtensions.ServiceProvider?.GetService<IMessenger>();
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
