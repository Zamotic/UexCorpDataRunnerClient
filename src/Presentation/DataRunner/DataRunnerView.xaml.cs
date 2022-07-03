using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using UexCorpDataRunner.Application.Common;
using UexCorpDataRunner.Interface.MessengerMessages;
using CommunityToolkit.Mvvm.Messaging;
using UexCorpDataRunner.Domain.Minimized;
using System.Windows.Media;
using System.Windows.Input;
using System;
using UexCorpDataRunner.Domain.Services;

namespace UexCorpDataRunner.Presentation.DataRunner;

/// <summary>
/// Interaction logic for MainPage.xaml
/// </summary>
public partial class DataRunnerView : UserControl
{
    Point _Location;
    ISettingsService? _settingsService;
    Window _window = System.Windows.Application.Current.MainWindow;

    public DataRunnerView()
    {
        InitializeComponent();
    }

    //public void ShowUserInterfaceNotified(ShowUserInterfaceMessage notification)
    public void ShowUserInterfaceMessageHandler(object sender, ShowUserInterfaceMessage notification)
    {
        double x = _Location.X;
        double y = _Location.Y;

        _window.Visibility = Visibility.Hidden;
        _window.WindowStyle = WindowStyle.SingleBorderWindow;
        _window.Width = MinimizedValues.MaximizedWidth;
        _window.Height = MinimizedValues.MaximizedHeight;
        _window.Left = x;
        _window.Top = y;
        _window.ResizeMode = ResizeMode.CanResize;
        _window.Visibility = Visibility.Visible;
        SetTopMostValueFromSettings();
    }

    //public void WindowMovedNotified(WindowMovedMessage notification)
    public void WindowMovedMessageHandler(object sender, WindowMovedMessage notification)
    {
        SaveWindowLocation();
    }

    public void CloseSettingsInterfaceMessageHandler(object sender, CloseSettingsInterfaceMessage notification)
    {
        if(notification.AlwaysOnTopChanged == true)
        {
            SetTopMostValueFromSettings();
        }
    }

    private void SetTopMostValueFromSettings()
    {
        if (_settingsService?.Settings?.AlwaysOnTop == Domain.Globals.Settings.Always)
        {
            _window.Topmost = true;
            return;
        }
        _window.Topmost = false;
    }

    private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        IMessenger? messenger = StartupExtensions.ServiceProvider?.GetService<IMessenger>();
        _settingsService = StartupExtensions.ServiceProvider?.GetService<ISettingsService>();

        messenger?.Register<ShowUserInterfaceMessage>(this, ShowUserInterfaceMessageHandler);
        messenger?.Register<WindowMovedMessage>(this, WindowMovedMessageHandler);
        messenger?.Register<CloseSettingsInterfaceMessage>(this, CloseSettingsInterfaceMessageHandler);
        SaveWindowLocation();
        SetTopMostValueFromSettings();
    }

    public void SaveWindowLocation()
    {
        Window window = System.Windows.Application.Current.MainWindow;
        if (window.WindowStyle == WindowStyle.SingleBorderWindow)
        {
            _Location = new Point(window.Left, window.Top);
            MinimizedValues.MaximizedWidth = Convert.ToInt32(window.Width);
            MinimizedValues.MaximizedHeight = Convert.ToInt32(window.Height);
        }
    }

    private void ClickSelectTextBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        var textBox = sender as TextBox;
        if (textBox is null)
            return;

        var context = textBox.DataContext as Application.DataRunner.CommodityWrapper;
        if(context is null)
        {
            return;
        }

        if(context.CurrentPrice is null)
        {
            context.CurrentPrice = context.ListedPrice;
            return;
        }

        if(context.CurrentPrice is not null)
        {
            context.CurrentPrice = null;
        }
    }

    //private void ItemsControl_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
    //{
    //    ItemsControl? itemsControl = sender as ItemsControl;
    //    if (itemsControl is null)
    //        return;

    //    var textbox = FindVisualChild<TextBox>(itemsControl.ItemContainerGenerator.ContainerFromIndex(0));

    //    FocusManager.SetFocusedElement(itemsControl, textbox);
    //}

    //public static T FindVisualChild<T>(DependencyObject depObj) where T : DependencyObject
    //{
    //    if (depObj != null)
    //    {
    //        for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
    //        {
    //            DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
    //            if (child != null
    //                && child is T)
    //            {
    //                return (T)child;
    //            }

    //            T childItem = FindVisualChild<T>(child);
    //            if (childItem != null)
    //                return childItem;
    //        }
    //    }
    //    return null;
    //}

    //private void TabItem_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
    //{
    //    TabItem? tabItem = sender as TabItem;
    //    if (tabItem is null)
    //        return;

    //    var textbox = FindVisualChild<TextBox>(tabItem);

    //    FocusManager.SetFocusedElement(tabItem, textbox);
    //}

    //private void TabItem_GotFocus(object sender, RoutedEventArgs e)
    //{
    //    var s = sender as UIElement;
    //    if(s is null)
    //    { 
    //        return;
    //    }
    //    s.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
    //}
}
