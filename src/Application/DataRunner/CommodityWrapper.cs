using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UexCorpDataRunner.Domain.DataRunner;
using CommunityToolkit.Mvvm.ComponentModel;
using UexCorpDataRunner.Extensions;

namespace UexCorpDataRunner.Application.DataRunner;
public class CommodityWrapper : ObservableObject
{
    private const decimal TolerancePercentage = .1m;
    private readonly Commodity _commodity;
    private readonly TradeListing _tradeListing;

    public string Name { get => _commodity.Name; }

    public OperationType Operation { get => _tradeListing.Operation; }
    
    public decimal ListedPrice
    {
        get
        {
            if (Operation == OperationType.Buy)
            {
                return _tradeListing.PriceBuy;
            }
            return _tradeListing.PriceSell;
        }
    }

    private decimal? _currentPrice = null;
    public decimal? CurrentPrice 
    {
        get => _currentPrice;
        set
        {
            SetProperty(ref _currentPrice, value);
            ValidatePriceIsWithinTolerances();
            SetMarkForSubmittalValue();
        }
    }

    public decimal MinPrice { get => _commodity.BuyPrice; }
    public decimal MaxPrice { get => _commodity.SellPrice; }

    public bool? _IsPriceWithinTolerance = null;
    public bool? IsPriceWithinTolerance
    {
        get => _IsPriceWithinTolerance;
        set => SetProperty(ref _IsPriceWithinTolerance, value);
    }

    public bool _MarkedForSubmittal = false;
    public bool MarkedForSubmittal 
    { 
        get => _MarkedForSubmittal;
        set
        {
            if(CurrentPrice is null)
            {
                value = false;
            }
            SetProperty(ref _MarkedForSubmittal, value);
        }
    }

    public CommodityWrapper(Commodity commodity, TradeListing tradeListing)
    {
        _commodity = commodity;
        _tradeListing = tradeListing;
    }

    private void ValidatePriceIsWithinTolerances()
    {
        if(CurrentPrice is null)
        {
            IsPriceWithinTolerance = null;
            return;
        }

        decimal minimumTolerance = MinPrice - (MinPrice * TolerancePercentage);
        decimal maximumTolerance = MaxPrice + (MaxPrice * TolerancePercentage);

        if(CurrentPrice.Value.IsLessThan(minimumTolerance) == true)
        {
            IsPriceWithinTolerance = false;
            return;
        }
        if(CurrentPrice.Value.IsGreaterThan(maximumTolerance) == true)
        {
            IsPriceWithinTolerance = false;
            return;
        }

        IsPriceWithinTolerance = true;
    }
    private void SetMarkForSubmittalValue()
    {
        if (CurrentPrice is null)
        {
            MarkedForSubmittal = false;
            return;
        }
        if (CurrentPrice is not null)
        {
            MarkedForSubmittal = true;
            return;
        }
    }
}
