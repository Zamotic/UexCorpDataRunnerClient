using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using UexCorpDataRunner.DesktopClient.Core;
using UexCorpDataRunner.DesktopClient.Model;
using UexCorpDataRunner.DesktopClient.ViewModels;

namespace UexCorpDataRunner.DesktopClient.DesignData;
public class MainViewModelData: MainViewModel
{
#if DEBUG
    /// <summary>
    /// Initializes a new instance of the MainViewModel class.
    /// </summary>
    public MainViewModelData() : base(new Messenger())
    {
        if (DesignerProperties.GetIsInDesignMode(new DependencyObject()) == false)
        {
            base.BindableCommodityPrices = new List<BindableCommodityPrice>()
            {
                new BindableCommodityPrice(new CommodityPrice()
                {
                    Name = "Aluminum",
                    OldPrice = 1.11m,
                    MinPrice = 1.10m,
                    MaxPrice = 1.20m,
                    BestPrice = 1.10m,
                    BestLocation = "HDMS-Perlman, Magda"
                }),
            new BindableCommodityPrice(new CommodityPrice()
            {
                Name = "Diamond",
                OldPrice = 5.85m,
                MinPrice = 5.85m,
                MaxPrice = 6.27m,
                BestPrice = 5.85m,
                BestLocation = "HDMS-Hahn, Magda"
            })
            };
        }
    }
#endif


}
