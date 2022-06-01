using UexCorpDataRunner.Application.Common;
using UexCorpDataRunner.Domain.DataRunner;
using CommunityToolkit.Mvvm.ComponentModel;

namespace UexCorpDataRunner.Application.ViewModels.Bindables;
public class ObservableCommodity : ObservableObject
{
    private Commodity _Commodity;

    public string? Name { get => _Commodity.Name; }
    public string? Code { get => _Commodity.Code; }
    public string? Kind { get => _Commodity.Kind; }
    public decimal BuyPrice
    {
        get => _Commodity.BuyPrice;
        set => SetProperty(_Commodity.BuyPrice, value, _Commodity, (s, v) => s.BuyPrice = v);
    }
    public decimal SellPrice
    {
        get => _Commodity.SellPrice;
        set => SetProperty(_Commodity.SellPrice, value, _Commodity, (s, v) => s.SellPrice = v);
    }
    public DateTime DateAdded { get => _Commodity.DateAdded; }
    public DateTime DateModified { get => _Commodity.DateModified; }

    private ObservableCommodity() => _Commodity = new Commodity();
    public ObservableCommodity(Commodity commodity) => _Commodity = commodity;
}
