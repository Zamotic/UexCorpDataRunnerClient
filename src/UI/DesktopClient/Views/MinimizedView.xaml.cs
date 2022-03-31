using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UexCorpDataRunner.DesktopClient.Core;
using UexCorpDataRunner.DesktopClient.Notifications;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;

namespace UexCorpDataRunner.DesktopClient.Views;

/// <summary>
/// Interaction logic for MinimizedView.xaml
/// </summary>
public partial class MinimizedView : UserControl//, INotificationHandler<HideUserInterfaceClickedNotification>
{
    public MinimizedView()
    {
        InitializeComponent();
    }

    public void HideUserInterfaceNotified(HideUserInterfaceNotification notification)
    {
        Window window = Application.Current.MainWindow;
        window.WindowStyle = WindowStyle.None;
        window.Width = 38;
        window.Height = 78;
        window.ResizeMode = ResizeMode.NoResize;
        window.Topmost = true;
    }

    private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        IMessenger? messenger = App.ServiceProvider?.GetService<IMessenger>();
        messenger?.Register<HideUserInterfaceNotification>(this, HideUserInterfaceNotified);
    }
}
