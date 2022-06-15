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
