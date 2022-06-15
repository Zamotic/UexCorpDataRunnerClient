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
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using UexCorpDataRunner.Interface.MessengerMessages;
using UexCorpDataRunner.Application.Common;
using CommunityToolkit.Mvvm.Messaging;
using UexCorpDataRunner.Domain.Minimized;
using UexCorpDataRunner.Domain.Services;

namespace UexCorpDataRunner.Presentation.Minimized;

/// <summary>
/// Interaction logic for MinimizedView.xaml
/// </summary>
public partial class MinimizedView : UserControl//, INotificationHandler<HideUserInterfaceClickedNotification>
{
    ISettingsService? _SettingsService;

    public MinimizedView()
    {
        InitializeComponent();
    }

    //private void HideUserInterfaceNotified(HideUserInterfaceMessage notification)
    private void HideUserInterfaceMessageHandler(object sender, HideUserInterfaceMessage notification)
    {
        Window window = System.Windows.Application.Current.MainWindow;
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
            window.Width = MinimizedValues.MinimizedWidth;
            window.Height = MinimizedValues.MinimizedHeight;
            SetVerticalWindowLocation(window, _SettingsService.Settings.CollapseLocation);
        }
        if (_SettingsService.Settings.CollapseOrientation == "Horizontal")
        {
            window.Width = MinimizedValues.MinimizedHeight;
            window.Height = MinimizedValues.MinimizedWidth;
            SetHorizontalWindowLocation(window, _SettingsService.Settings.CollapseLocation);
        }

        window.Visibility = Visibility.Visible;
    }

    private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        IMessenger? messenger = StartupExtensions.ServiceProvider?.GetService<IMessenger>();
        messenger?.Register<HideUserInterfaceMessage>(this, HideUserInterfaceMessageHandler);

        _SettingsService = StartupExtensions.ServiceProvider?.GetService<ISettingsService>();
    }

    private void SetVerticalWindowLocation(Window window, string collapseLocation)
    {
        double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
        double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;

        var halfXWidth = screenWidth / 2;
        var centerXLocation = halfXWidth - (MinimizedValues.MinimizedWidth / 2);
        var rightXLocation = screenWidth - MinimizedValues.MinimizedWidth;

        var halfHeight = screenHeight / 2;
        var centerYLocation = halfHeight - (MinimizedValues.MinimizedHeight / 2);
        var bottomYLocation = screenHeight - MinimizedValues.MinimizedHeight;

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
        var centerXLocation = halfXWidth - (MinimizedValues.MinimizedHeight / 2);
        var rightXLocation = screenWidth - MinimizedValues.MinimizedHeight;

        var halfHeight = screenHeight / 2;
        var centerYLocation = halfHeight - (MinimizedValues.MinimizedWidth / 2);
        var bottomYLocation = screenHeight - MinimizedValues.MinimizedWidth;

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
