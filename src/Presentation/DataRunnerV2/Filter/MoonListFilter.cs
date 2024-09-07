using Microsoft.Xaml.Behaviors;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using UexCorpDataRunner.Domain.DataRunnerV2;

namespace UexCorpDataRunner.Presentation.DataRunnerV2.Filter;

public class MoonListFilter : Behavior<CollectionViewSource>
{
    public static readonly DependencyProperty SelectedPlanetProperty =
    DependencyProperty.Register(nameof(SelectedPlanet), typeof(Planet), typeof(MoonListFilter), new UIPropertyMetadata(OnSelectedPlanetChangedCallBack));
    public Planet SelectedPlanet
    {
        get { return (Planet)GetValue(SelectedPlanetProperty); }
        set { SetValue(SelectedPlanetProperty, value); }
    }

    private static void OnSelectedPlanetChangedCallBack(DependencyObject sender, DependencyPropertyChangedEventArgs e)
    {
        MoonListFilter? f = sender as MoonListFilter;
        if (f != null)
        {
            f.OnSelectedPlanetChanged();
        }
    }

    protected virtual void OnSelectedPlanetChanged()
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
        Moon? moon = e.Item as Moon;
        if (moon is null)
        {
            e.Accepted = false;
            return;
        }
        if (SelectedPlanet is null)
        {
            e.Accepted = false;
            return;
        }
        if (moon.IsAvailable is false)
        {
            e.Accepted = false;
            return;
        }
        e.Accepted = moon.PlanetId.Equals(SelectedPlanet.Id);
    }
}
