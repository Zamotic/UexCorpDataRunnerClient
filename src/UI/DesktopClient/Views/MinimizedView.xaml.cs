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
using UexCorpDataRunner.DesktopClient.Settings;

namespace UexCorpDataRunner.DesktopClient.Views;

/// <summary>
/// Interaction logic for MinimizedView.xaml
/// </summary>
public partial class MinimizedView : UserControl//, INotificationHandler<HideUserInterfaceClickedNotification>
{
    const int CollapsedWindowWidth = 38;
    const int CollapsedWindowHeight = 78;

    ISettingsService? _SettingsService;

    public MinimizedView()
    {
        InitializeComponent();
    }

    private void HideUserInterfaceNotified(HideUserInterfaceNotification notification)
    {
        Window window = Application.Current.MainWindow;
        window.Visibility = Visibility.Hidden;
        window.WindowStyle = WindowStyle.None;
        window.ResizeMode = ResizeMode.NoResize;
        window.Topmost = true;

        if(_SettingsService?.Settings is null)
        {
            window.Visibility = Visibility.Visible;
            return;
        }

        if (_SettingsService.Settings.CollapseOrientation == "Vertical")
        {
            window.Width = CollapsedWindowWidth;
            window.Height = CollapsedWindowHeight;
            SetVerticalWindowLocation(window, _SettingsService.Settings.CollapseLocation);
        }
        if (_SettingsService.Settings.CollapseOrientation == "Horizontal")
        {
            window.Width = CollapsedWindowHeight;
            window.Height = CollapsedWindowWidth;
            SetHorizontalWindowLocation(window, _SettingsService.Settings.CollapseLocation);
        }

        window.Visibility = Visibility.Visible;
    }

    private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        IMessenger? messenger = App.ServiceProvider?.GetService<IMessenger>();
        messenger?.Register<HideUserInterfaceNotification>(this, HideUserInterfaceNotified);

        _SettingsService = App.ServiceProvider?.GetService<ISettingsService>();
    }

    private void SetVerticalWindowLocation(Window window, string collapseLocation)
    {
        double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
        double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;

        var halfXWidth = screenWidth / 2;
        var centerXLocation = halfXWidth - (CollapsedWindowWidth / 2);
        var rightXLocation = screenWidth - CollapsedWindowWidth;

        var halfHeight = screenHeight / 2;
        var centerYLocation = halfHeight - (CollapsedWindowHeight / 2);
        var bottomYLocation = screenHeight - CollapsedWindowHeight;

        if (collapseLocation.Equals("TopLeft") == true)
        {
            window.Left = 0;
            window.Top = 0;
            return;
        }

        if (collapseLocation.Equals("TopCenter") == true)
        {
            window.Left = centerXLocation;
            window.Top = 0;
            return;
        }

        if (collapseLocation.Equals("TopRight") == true)
        {
            window.Left = rightXLocation;
            window.Top = 0;
            return;
        }

        if (collapseLocation.Equals("CenterLeft") == true)
        {
            window.Left = 0;
            window.Top = centerYLocation;
            return;
        }

        if (collapseLocation.Equals("CenterRight") == true)
        {
            window.Left = rightXLocation;
            window.Top = centerYLocation;
            return;
        }

        if (collapseLocation.Equals("BottomLeft") == true)
        {
            window.Left = 0;
            window.Top = bottomYLocation;
            return;
        }

        if (collapseLocation.Equals("BottomCenter") == true)
        {
            window.Left = centerXLocation;
            window.Top = bottomYLocation;
            return;
        }

        if (collapseLocation.Equals("BottomRight") == true)
        {
            window.Left = rightXLocation;
            window.Top = bottomYLocation;
            return;
        }
    }
    private void SetHorizontalWindowLocation(Window window, string collapseLocation)
    {
        double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
        double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;

        var halfXWidth = screenWidth / 2;
        var centerXLocation = halfXWidth - (CollapsedWindowHeight / 2);
        var rightXLocation = screenWidth - CollapsedWindowHeight;

        var halfHeight = screenHeight / 2;
        var centerYLocation = halfHeight - (CollapsedWindowWidth / 2);
        var bottomYLocation = screenHeight - CollapsedWindowWidth;

        if (collapseLocation.Equals("TopLeft") == true)
        {
            window.Left = 0;
            window.Top = 0;
            return;
        }

        if (collapseLocation.Equals("TopCenter") == true)
        {
            window.Left = centerXLocation;
            window.Top = 0;
            return;
        }

        if (collapseLocation.Equals("TopRight") == true)
        {
            window.Left = rightXLocation;
            window.Top = 0;
            return;
        }

        if (collapseLocation.Equals("CenterLeft") == true)
        {
            window.Left = 0;
            window.Top = centerYLocation;
            return;
        }

        if (collapseLocation.Equals("CenterRight") == true)
        {
            window.Left = rightXLocation;
            window.Top = centerYLocation;
            return;
        }

        if (collapseLocation.Equals("BottomLeft") == true)
        {
            window.Left = 0;
            window.Top = bottomYLocation;
            return;
        }

        if (collapseLocation.Equals("BottomCenter") == true)
        {
            window.Left = centerXLocation;
            window.Top = bottomYLocation;
            return;
        }

        if (collapseLocation.Equals("BottomRight") == true)
        {
            window.Left = rightXLocation;
            window.Top = bottomYLocation;
            return;
        }
    }

}
