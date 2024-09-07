using System.ComponentModel;
using System.Windows.Data;
using Microsoft.Xaml.Behaviors;
using UexCorpDataRunner.Application.DataRunnerV2;
using UexCorpDataRunner.Domain.DataRunnerV2;

namespace UexCorpDataRunner.Presentation.DataRunnerV2.Sort;

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
