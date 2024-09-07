using Microsoft.Xaml.Behaviors;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using UexCorpDataRunner.Domain.DataRunnerV2;

namespace UexCorpDataRunner.Presentation.DataRunnerV2.Filter;

public class PlanetListFilter : Behavior<CollectionViewSource>
{
    public static readonly DependencyProperty SelectedStarSystemProperty =
    DependencyProperty.Register(nameof(SelectedSystem), typeof(StarSystem), typeof(PlanetListFilter), new UIPropertyMetadata(OnSelectedSystemChangedCallBack));
    public StarSystem SelectedSystem
    {
        get { return (StarSystem)GetValue(SelectedStarSystemProperty); }
        set { SetValue(SelectedStarSystemProperty, value); }
    }

    private static void OnSelectedSystemChangedCallBack(DependencyObject sender, DependencyPropertyChangedEventArgs e)
    {
        PlanetListFilter? f = sender as PlanetListFilter;
        if (f != null)
        {
            f.OnSelectedSystemChanged();
        }
    }

    protected virtual void OnSelectedSystemChanged()
    {
        // Grab related data.
        // Raises INotifyPropertyChanged.PropertyChanged
        //if(AssociatedObject.View.IsEmpty == false)
        //{
            AssociatedObject.View.Refresh();
        //}
    }

    protected override void OnAttached()
    {
        base.OnAttached();

        AssociatedObject.Filter += AssociatedObjectOnFilter;
    }

    private void AssociatedObjectOnFilter(object sender, FilterEventArgs e)
    {
        Planet? planet = e.Item as Planet;
        if (planet is null)
        {
            e.Accepted = false;
            return;
        }
        if (SelectedSystem is null)
        {
            e.Accepted = false;
            return;
        }
        if (planet.IsAvailable is false)
        {
            e.Accepted = false;
            return;
        }
        e.Accepted = planet.StarSystemId.Equals(SelectedSystem.Id);
    }
}
