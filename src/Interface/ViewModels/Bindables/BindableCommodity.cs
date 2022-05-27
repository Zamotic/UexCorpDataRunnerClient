using UexCorpDataRunner.Application.Common;
using UexCorpDataRunner.Domain.DataRunner;

namespace UexCorpDataRunner.Application.ViewModels.Bindables;
public class BindableCommodity : BindableBase
{
    private Commodity _Commodity;

    public string? Name { get => _Commodity.Name; }
    public string? Code { get => _Commodity.Code; }
    public string? Kind { get => _Commodity.Kind; }
    public decimal BuyPrice
    {
        get => _Commodity.BuyPrice;
        set => SetProperty((value) => { _Commodity.BuyPrice = value; }, value);
    }
    public decimal SellPrice
    {
        get => _Commodity.SellPrice;
        set => SetProperty((value) => { _Commodity.SellPrice = value; }, value);
    }
    public DateTime DateAdded { get => _Commodity.DateAdded; }
    public DateTime DateModified { get => _Commodity.DateModified; }

    private BindableCommodity() { _Commodity = new Commodity(); }
    public BindableCommodity(Commodity commodity)
    {
        _Commodity = commodity;
    }
}
