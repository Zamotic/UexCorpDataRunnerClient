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

namespace UexCorpDataRunner.DesktopClient.Views;

/// <summary>
/// Interaction logic for MinimizedView.xaml
/// </summary>
public partial class MinimizedView : UserControl
{
    public MinimizedView()
    {
        InitializeComponent();
    }

    private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this) == true)
        {
            return;
        }

        var viewModel = e.NewValue as IMinimizedVewModel;
        if(viewModel is null)
        {
            return;
        }

        viewModel.ShowUserInterfaceClicked += ViewModel_ShowUserInterfaceClicked;
    }

    private void ViewModel_ShowUserInterfaceClicked(object sender, EventArgs e)
    {
        Window window = Window.GetWindow(this);
        window.WindowStyle = WindowStyle.SingleBorderWindow;
        window.Width = 350;
        window.Height = 700;
        window.ResizeMode = ResizeMode.CanResize;
        window.Topmost = true;
    }
}
