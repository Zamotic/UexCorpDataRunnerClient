using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Data;
using UexCorpDataRunner.Domain.DataRunner;

namespace UexCorpDataRunner.Presentation.DataRunner.Filter;

public class SatelliteListFilter : Behavior<CollectionViewSource>
{
    public static readonly DependencyProperty SelectedPlanetProperty =
    DependencyProperty.Register(nameof(SelectedPlanet), typeof(Planet), typeof(SatelliteListFilter), new UIPropertyMetadata(OnSelectedPlanetChangedCallBack));
    public Planet SelectedPlanet
    {
        get { return (Planet)GetValue(SelectedPlanetProperty); }
        set { SetValue(SelectedPlanetProperty, value); }
    }

    private static void OnSelectedPlanetChangedCallBack(DependencyObject sender, DependencyPropertyChangedEventArgs e)
    {
        SatelliteListFilter? f = sender as SatelliteListFilter;
        if (f != null)
        {
            f.OnSelectedPlanetChanged();
        }
    }

    protected virtual void OnSelectedPlanetChanged()
    {
        // Grab related data.
        // Raises INotifyPropertyChanged.PropertyChanged
        //if (AssociatedObject.View.IsEmpty == false)
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
        if (SelectedPlanet is null)
        {
            e.Accepted = false;
            return;
        }
        Satellite? satellite = e.Item as Satellite;
        if (satellite is null)
        {
            e.Accepted = false;
            return;
        }
        if (satellite.Planet is null)
        {
            e.Accepted = false;
            return;
        }
        if (satellite.IsAvailable is false)
        {
            e.Accepted = false;
            return;
        }
        e.Accepted = satellite.Planet.Equals(SelectedPlanet.Code);
    }
}
