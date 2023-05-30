using System.ComponentModel;
using System.Windows.Data;
using Microsoft.Xaml.Behaviors;
using UexCorpDataRunner.Application.DataRunner;
using UexCorpDataRunner.Domain.DataRunner;

namespace UexCorpDataRunner.Presentation.DataRunner.Sort;

public class CommoditySort : Behavior<CollectionViewSource>
{
    protected override void OnAttached()
    {
        base.OnAttached();

        AssociatedObject.SortDescriptions.Clear();
        //AssociatedObject.SortDescriptions.Add(new SortDescription(nameof(CommodityWrapper.Kind), ListSortDirection.Ascending));
        AssociatedObject.SortDescriptions.Add(new SortDescription(nameof(CommodityWrapper.Name), ListSortDirection.Ascending));
    }
}
