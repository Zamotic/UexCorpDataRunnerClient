using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using UexCorpDataRunner.Domain.DataRunnerV2;
using UexCorpDataRunner.Extensions;

namespace UexCorpDataRunner.Application.DataRunnerV2;
public class CommodityWrapper : ObservableObject
{
    private const decimal TolerancePercentage = .1m;
    private readonly Commodity _commodity;
    private readonly CommodityPrice _commodityPrice;

    public string Name { get => _commodity.Name!; }
    public string Code { get => _commodity.Code!; }
    public int Id { get => _commodity.Id!; }

    public OperationType Operation { get; private set; }

    public decimal ListedPrice
    {
        get
        {
            return _commodityPrice.SellPrice;
        }
    }

    private decimal? _currentPrice = null;
    public decimal? CurrentPrice
    {
        get => _currentPrice;
        set
        {
            //if(value % 1 == 0)
            //{
            //    value = value;
            //}
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
            if (CurrentPrice is null)
            {
                value = false;
            }
            SetProperty(ref _MarkedForSubmittal, value);
        }
    }

    public CommodityWrapper(Commodity commodity, CommodityPrice commodityPrice)
    {
        _commodity = commodity;
        _commodityPrice = commodityPrice;
        SetOperationType();
    }

    private void ValidatePriceIsWithinTolerances()
    {
        if (CurrentPrice is null)
        {
            IsPriceWithinTolerance = null;
            return;
        }

        decimal minimumTolerance = MinPrice - (MinPrice * TolerancePercentage);
        decimal maximumTolerance = MaxPrice + (MaxPrice * TolerancePercentage);

        if (CurrentPrice.Value.IsLessThan(minimumTolerance) == true)
        {
            IsPriceWithinTolerance = false;
            return;
        }
        if (CurrentPrice.Value.IsGreaterThan(maximumTolerance) == true)
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

    private void SetOperationType()
    {
        if (_commodity.IsBuyable == true)
        {
            if(_commodityPrice.BuyPrice > 0)
            {
                Operation = OperationType.Buy;
                return;
            }
            Operation = OperationType.Sell;
        }
        if (_commodity.IsSellable == true)
        {
            if (_commodityPrice.SellPrice > 0)
            {
                Operation = OperationType.Sell;
                return;
            }
        }
        Operation = OperationType.Unknown;
    }
}
