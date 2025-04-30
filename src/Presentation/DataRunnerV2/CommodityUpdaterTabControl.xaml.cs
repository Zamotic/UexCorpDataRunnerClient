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

namespace UexCorpDataRunner.Presentation.DataRunnerV2;
/// <summary>
/// Interaction logic for CommodityUpdaterTabControl.xaml
/// </summary>
public partial class CommodityUpdaterTabControl : UserControl
{
    public CommodityUpdaterTabControl()
    {
        InitializeComponent();
    }

    private int LastTabIndex = 0;
    private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        TabControl? obj = sender as TabControl;
        if (obj is null)
            return;

        if (LastTabIndex == obj.SelectedIndex)
        {
            return;
        }

        LastTabIndex = obj.SelectedIndex;

        if (obj.SelectedIndex == 0)
        {
            BuyTabScrollViewer.ScrollToTop();
            return;
        }

        SellTabScrollViewer.ScrollToTop();
    }
}
