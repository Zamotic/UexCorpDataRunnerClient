using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UexCorpDataRunner.DesktopClient.Core;

namespace UexCorpDataRunner.DesktopClient.Model;
public class BindableCommodityPrice : BindableBase
{
    private CommodityPrice _CommodityPrice;

    public string? Name { get => _CommodityPrice.Name; }
    public decimal NewPrice
    {
        get => _CommodityPrice.NewPrice;
        set => SetProperty((value) => { _CommodityPrice.NewPrice = value; }, value);
    }
    public int AvailableSupply
    {
        get => _CommodityPrice.AvailableSupply;
        set => SetProperty((value) => { _CommodityPrice.AvailableSupply = value; }, value);
    }
    public decimal OldPrice  { get => _CommodityPrice.OldPrice; }
    public decimal MinPrice  { get => _CommodityPrice.MinPrice; }
    public decimal MaxPrice  { get => _CommodityPrice.MaxPrice; }
    public decimal BestPrice  { get => _CommodityPrice.BestPrice; }
    public string BestLocation { get => _CommodityPrice.BestLocation; }

    private BindableCommodityPrice() { _CommodityPrice = new CommodityPrice(); }
    public BindableCommodityPrice(CommodityPrice commodityPrice)
    {
        _CommodityPrice = commodityPrice;
    }
}
