using System.ComponentModel;
using System.Windows.Data;
using Microsoft.Xaml.Behaviors;
using UexCorpDataRunner.Domain.DataRunner;

namespace UexCorpDataRunner.Presentation.DataRunner.Sort;

public class SatelliteSort : Behavior<CollectionViewSource>
{
    protected override void OnAttached()
    {
        base.OnAttached();

        AssociatedObject.SortDescriptions.Clear();
        AssociatedObject.SortDescriptions.Add(new SortDescription(nameof(Satellite.Name), ListSortDirection.Ascending));
    }
}
