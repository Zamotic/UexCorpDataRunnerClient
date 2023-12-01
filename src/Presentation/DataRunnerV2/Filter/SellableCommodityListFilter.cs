using Microsoft.Xaml.Behaviors;
using System.Windows.Data;
using UexCorpDataRunner.Application.DataRunnerV2;
using UexCorpDataRunner.Domain.DataRunnerV2;

namespace UexCorpDataRunner.Presentation.DataRunnerV2.Filter;

public class SellableCommodityListFilter : Behavior<CollectionViewSource>
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
        e.Accepted = commodity.Operation == OperationType.Sell;
    }
}
