using Microsoft.Xaml.Behaviors;
using System.Windows.Data;
using UexCorpDataRunner.Application.DataRunner;
using UexCorpDataRunner.Domain.DataRunner;

namespace UexCorpDataRunner.Presentation.DataRunner.Filter;

public class BuyableCommodityListFilter : Behavior<CollectionViewSource>
{
    protected override void OnAttached()
    {
        base.OnAttached();

        AssociatedObject.Filter += AssociatedObjectOnFilter;
    }

    private void AssociatedObjectOnFilter(object sender, FilterEventArgs e)
    {
        CommodityWrapper? commodity = e.Item as CommodityWrapper;
        if (commodity is null)
        {
            e.Accepted = false;
            return;
        }
        e.Accepted = commodity.Operation == OperationType.Buy;
    }
}
