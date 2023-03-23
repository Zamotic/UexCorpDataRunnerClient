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
    public string Code { get => _commodity.Code; }
    public string Kind { get => _commodity.Kind; }

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
            if(value % 1 == 0)
            {
                value = value / 100;
            }
            SetProperty(ref _currentPrice, value);
            ValidatePriceIsWithinTolerances();
            SetMarkForSubmittalValue();
        }
    }

    private int? _currentScu = null;
    public int? CurrentScu
    {
        get => _currentScu;
        set
        {
            SetProperty(ref _currentScu, value);
            ValidateScuIsWithinTolerances();
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

    public bool? _IsScuWithinTolerance = null;
    public bool? IsScuWithinTolerance
    {
        get => _IsScuWithinTolerance;
        set => SetProperty(ref _IsScuWithinTolerance, value);
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

    private void ValidateScuIsWithinTolerances()
    {
        if (CurrentScu is null)
        {
            IsScuWithinTolerance = null;
            return;
        }

        int minimumTolerance = 0;
        int maximumTolerance = int.MaxValue;

        if (CurrentScu.Value.IsLessThan(minimumTolerance) == true)
        {
            IsScuWithinTolerance = false;
            return;
        }
        if (CurrentScu.Value.IsGreaterThan(maximumTolerance) == true)
        {
            IsScuWithinTolerance = false;
            return;
        }

        IsScuWithinTolerance = true;
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
