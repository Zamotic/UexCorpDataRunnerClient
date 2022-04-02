using Microsoft.Extensions.DependencyInjection;
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

namespace UexCorpDataRunner.DesktopClient.Views;

/// <summary>
/// Interaction logic for SettingsView.xaml
/// </summary>
public partial class SettingsView : UserControl
{
    public SettingsView()
    {
        InitializeComponent();
    }

    public void ShowSettingsInterfaceNotified(ShowSettingsInterfaceNotification notification)
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
        messenger?.Register<ShowSettingsInterfaceNotification>(this, ShowSettingsInterfaceNotified);
    }
}
