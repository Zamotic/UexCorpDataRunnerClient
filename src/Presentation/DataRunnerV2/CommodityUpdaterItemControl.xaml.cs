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
using UexCorpDataRunner.UILibrary.Extensions;

namespace UexCorpDataRunner.Presentation.DataRunnerV2;
/// <summary>
/// Interaction logic for CommodityUpdaterItemControl.xaml
/// </summary>
public partial class CommodityUpdaterItemControl : UserControl
{
    static Color greenColor = (Color)ColorConverter.ConvertFromString("#FF009E21");

    public CommodityUpdaterItemControl()
    {
        InitializeComponent();
    }

    private void ClickSelectTextBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        var textBox = sender as TextBox;
        if (textBox is null)
            return;

        var context = textBox.DataContext as Application.DataRunnerV2.CommodityWrapper;
        if (context is null)
        {
            return;
        }

        if (context.CurrentPrice is null)
        {
            context.CurrentPrice = context.ListedPrice;
            return;
        }

        if (context.CurrentPrice is not null)
        {
            context.CurrentPrice = null;
        }
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        var button = sender as Button;
        if (button is null)
            return;

        var context = button.DataContext as Application.DataRunnerV2.CommodityWrapper;
        if (context is null)
        {
            return;
        }

        if (short.TryParse(button.Content.ToString(), out short currentValue) == false)
        {
            return;
        }

        if(currentValue < 1)
        {
            return;
        }

        if (((SolidColorBrush)button.Background).Color == Colors.Gray)
        {
            if (!context.ContainerSizes.Contains(currentValue))
            {
                context.ContainerSizes.Add(currentValue);
            }
            button.Background = new SolidColorBrush(greenColor);
            return;
        }

        if (context.ContainerSizes.Contains(currentValue))
        {
            context.ContainerSizes.Remove(currentValue);
        }
        button.Background = new SolidColorBrush(Colors.Gray);
    }

    bool hasBeenLoaded = false;
    private void UserControl_Loaded(object sender, RoutedEventArgs e)
    {
        if(hasBeenLoaded)
        {
            return;
        }

        var context = this.DataContext as Application.DataRunnerV2.CommodityWrapper;

        if (context is null)
        {
            return;
        }

        foreach (Button b in DependencyObjectUtilities.FindVisualChildren<Button>((DependencyObject)sender))
        {
            if (short.TryParse(b.Content.ToString(), out short currentValue) == false)
            {
                continue;
            }

            if (currentValue < 1)
            {
                continue;
            }

            if(context.ContainerSizes.Contains(currentValue) == false)
            {
                b.Background = new SolidColorBrush(Colors.Gray);
            }
        }

        hasBeenLoaded = true;
    }
}
