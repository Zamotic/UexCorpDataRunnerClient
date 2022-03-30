using System.Windows;
using System.Windows.Controls;
using UexCorpDataRunner.DesktopClient.Core;
using UexCorpDataRunner.DesktopClient.ViewModels;

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

    private void UserControl_DataContextChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
    {
        if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this) == true)
        {
            return;
        }

        var viewModel = e.NewValue as IMainVewModel;
        if (viewModel is null)
        {
            return;
        }

        viewModel.HideUserInterfaceClicked += ViewModel_HideUserInterfaceClicked;
    }

    private void ViewModel_HideUserInterfaceClicked(object sender, System.EventArgs e)
    {
        Window window = Window.GetWindow(this);
        window.WindowStyle = WindowStyle.None;
        window.Width = 38;
        window.Height = 78;
        window.ResizeMode = ResizeMode.NoResize;
        window.Topmost = true;
    }
}
