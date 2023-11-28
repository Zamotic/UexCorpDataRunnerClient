using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace UexCorpDataRunner.Presentation.DataRunnerV2.Filter;
public class SystemListFilter : Behavior<CollectionViewSource>
{
    protected override void OnAttached()
    {
        base.OnAttached();

        AssociatedObject.Filter += AssociatedObjectOnFilter;
    }

    private void AssociatedObjectOnFilter(object sender, FilterEventArgs e)
    {
        Domain.DataRunnerV2.StarSystem? system = e.Item as Domain.DataRunnerV2.StarSystem;
        if (system is null)
        {
            e.Accepted = false;
            return;
        }
        e.Accepted = system.IsAvailable;
    }
}
