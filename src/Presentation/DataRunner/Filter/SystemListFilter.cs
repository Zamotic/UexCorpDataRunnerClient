using Microsoft.Xaml.Behaviors;
using System.Windows.Data;

namespace UexCorpDataRunner.Presentation.DataRunner.Filter;

public class SystemListFilter : Behavior<CollectionViewSource>
{
    protected override void OnAttached()
    {
        base.OnAttached();

        AssociatedObject.Filter += AssociatedObjectOnFilter;
    }

    private void AssociatedObjectOnFilter(object sender, FilterEventArgs e)
    {
        Domain.DataRunner.StarSystem? system = e.Item as Domain.DataRunner.StarSystem;
        if (system is null)
        {
            e.Accepted = false;
            return;
        }
        e.Accepted = system.IsAvailable;
    }
}
