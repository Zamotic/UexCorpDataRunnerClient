using Microsoft.Xaml.Behaviors;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using UexCorpDataRunner.Domain.DataRunnerV2;

namespace UexCorpDataRunner.Presentation.DataRunnerV2.Filter;

public class SpaceStationListFilter : Behavior<CollectionViewSource>
{
    public static readonly DependencyProperty SelectedPlanetProperty =
    DependencyProperty.Register(nameof(SelectedPlanet), typeof(Planet), typeof(SpaceStationListFilter), new UIPropertyMetadata(OnSelectedPlanetChangedCallBack));
    public Planet SelectedPlanet
    {
        get { return (Planet)GetValue(SelectedPlanetProperty); }
        set { SetValue(SelectedPlanetProperty, value); }
    }

    public static readonly DependencyProperty SelectedMoonProperty =
    DependencyProperty.Register(nameof(SelectedMoon), typeof(Moon), typeof(SpaceStationListFilter), new UIPropertyMetadata(OnSelectedMoonChangedCallBack));
    public Moon SelectedMoon
    {
        get { return (Moon)GetValue(SelectedMoonProperty); }
        set { SetValue(SelectedMoonProperty, value); }
    }

    private static void OnSelectedPlanetChangedCallBack(DependencyObject sender, DependencyPropertyChangedEventArgs e)
    {
        SpaceStationListFilter? f = sender as SpaceStationListFilter;
        if (f != null)
        {
            f.OnSelectedChanged();
        }
    }

    private static void OnSelectedMoonChangedCallBack(DependencyObject sender, DependencyPropertyChangedEventArgs e)
    {
        SpaceStationListFilter? f = sender as SpaceStationListFilter;
        if (f != null)
        {
            f.OnSelectedChanged();
        }
    }

    protected virtual void OnSelectedChanged()
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
        SpaceStation? spaceStation = e.Item as SpaceStation;
        if (spaceStation is null)
        {
            e.Accepted = false;
            return;
        }
        if (SelectedPlanet is null && SelectedMoon is null)
        {
            e.Accepted = false;
            return;
        }
        if (spaceStation.IsAvailable is false)
        {
            e.Accepted = false;
            return;
        }
        if (SelectedPlanet is null) 
        { 
            e.Accepted = spaceStation.MoonId.Equals(SelectedMoon.Id);
            return;
        }
        if (SelectedMoon is null)
        {
            e.Accepted = spaceStation.PlanetId.Equals(SelectedPlanet.Id);
            return;
        }
        e.Accepted = (spaceStation.PlanetId.Equals(SelectedPlanet.Id) && spaceStation.MoonId.Equals(SelectedMoon.Id));
    }
}
