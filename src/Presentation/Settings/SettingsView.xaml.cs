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
using UexCorpDataRunner.Application.Common;
using UexCorpDataRunner.Application.MessengerMessages;
using CommunityToolkit.Mvvm.Messaging;

namespace UexCorpDataRunner.Presentation.Settings;

/// <summary>
/// Interaction logic for SettingsView.xaml
/// </summary>
public partial class SettingsView : UserControl
{
    public SettingsView()
    {
        InitializeComponent();
    }

    //public void ShowSettingsInterfaceNotified(ShowSettingsInterfaceMessage notification)
    public void ShowSettingsInterfaceMessageHandler(object sender, ShowSettingsInterfaceMessage notification)
    {
        Window window = System.Windows.Application.Current.MainWindow;
        window.WindowStyle = WindowStyle.SingleBorderWindow;
        window.Width = 425;
        window.Height = 700;
        window.ResizeMode = ResizeMode.CanResize;
        window.Topmost = true;
    }

    private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        IMessenger? messenger = StartupExtensions.ServiceProvider?.GetService<IMessenger>();
        messenger?.Register<ShowSettingsInterfaceMessage>(this, ShowSettingsInterfaceMessageHandler);
    }
}
