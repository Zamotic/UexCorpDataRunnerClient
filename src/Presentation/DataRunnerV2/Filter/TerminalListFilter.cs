using Microsoft.Xaml.Behaviors;
using System;
using System.Windows;
using System.Windows.Data;
using UexCorpDataRunner.Domain.DataRunnerV2;

namespace UexCorpDataRunner.Presentation.DataRunnerV2.Filter;

public class TerminalListFilter : Behavior<CollectionViewSource>
{
    public static readonly DependencyProperty SelectedStarSystemProperty =
        DependencyProperty.Register(nameof(SelectedStarSystem), typeof(StarSystem), typeof(TerminalListFilter), new UIPropertyMetadata(OnSelectedChangedCallBack));
    public StarSystem SelectedStarSystem
    {
        get { return (StarSystem)GetValue(SelectedStarSystemProperty); }
        set { SetValue(SelectedStarSystemProperty, value); }
    }

    public static readonly DependencyProperty SelectedPlanetProperty =
        DependencyProperty.Register(nameof(SelectedPlanet), typeof(Planet), typeof(TerminalListFilter), new UIPropertyMetadata(OnSelectedChangedCallBack));
    public Planet SelectedPlanet
    {
        get { return (Planet)GetValue(SelectedPlanetProperty); }
        set { SetValue(SelectedPlanetProperty, value); }
    }

    public static readonly DependencyProperty SelectedMoonProperty =
        DependencyProperty.Register(nameof(SelectedMoon), typeof(Moon), typeof(TerminalListFilter), new UIPropertyMetadata(OnSelectedChangedCallBack));
    public Moon SelectedMoon
    {
        get { return (Moon)GetValue(SelectedMoonProperty); }
        set { SetValue(SelectedMoonProperty, value); }
    }

    public static readonly DependencyProperty SelectedSpaceStationProperty =
        DependencyProperty.Register(nameof(SelectedSpaceStation), typeof(SpaceStation), typeof(TerminalListFilter), new UIPropertyMetadata(OnSelectedChangedCallBack));
    public SpaceStation SelectedSpaceStation
    {
        get { return (SpaceStation)GetValue(SelectedSpaceStationProperty); }
        set { SetValue(SelectedSpaceStationProperty, value); }
    }

    public static readonly DependencyProperty SelectedOutpostProperty =
        DependencyProperty.Register(nameof(SelectedOutpost), typeof(Outpost), typeof(TerminalListFilter), new UIPropertyMetadata(OnSelectedChangedCallBack));
    public Outpost SelectedOutpost
    {
        get { return (Outpost)GetValue(SelectedOutpostProperty); }
        set { SetValue(SelectedOutpostProperty, value); }
    }

    public static readonly DependencyProperty SelectedCityProperty =
        DependencyProperty.Register(nameof(SelectedCity), typeof(City), typeof(TerminalListFilter), new UIPropertyMetadata(OnSelectedChangedCallBack));
    public City SelectedCity
    {
        get { return (City)GetValue(SelectedCityProperty); }
        set { SetValue(SelectedCityProperty, value); }
    }

    private static void OnSelectedChangedCallBack(DependencyObject sender, DependencyPropertyChangedEventArgs e)
    {
        TerminalListFilter? f = sender as TerminalListFilter;
        if (f != null)
        {
            f.OnPropertyChanged();
        }
    }

    protected virtual void OnPropertyChanged()
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
        if (ArePropertiesAllNull())
        {
            e.Accepted = false;
            return;
        }
        Terminal? terminal = e.Item as Terminal;
        if (terminal is null)
        {
            e.Accepted = false;
            return;
        }
        bool? starSystemValue = null;
        if(SelectedStarSystem is not null)
        {
            starSystemValue = terminal.StarSystemId.Equals(SelectedStarSystem.Id);
        }
        bool? planetValue = null;
        if (SelectedPlanet is not null)
        {
            planetValue = terminal.PlanetId.Equals(SelectedPlanet.Id);
        }
        bool? moonValue = null;
        if (SelectedMoon is not null)
        {
            moonValue = terminal.MoonId.Equals(SelectedMoon.Id);
        }
        bool? spaceStationValue = null;
        if (SelectedSpaceStation is not null)
        {
            spaceStationValue = terminal.SpaceStationId.Equals(SelectedSpaceStation.Id);
        }
        bool? outpostValue = null;
        if (SelectedOutpost is not null)
        {
            outpostValue = terminal.OutpostId.Equals(SelectedOutpost.Id);
        }
        bool? cityValue = null;
        if (SelectedCity is not null)
        {
            cityValue = terminal.CityId.Equals(SelectedCity.Id);
        }
        e.Accepted = (starSystemValue is null || starSystemValue is true) 
            && (planetValue is null || planetValue is true) 
            && (moonValue is null || moonValue is true) 
            && (spaceStationValue is null || spaceStationValue is true) 
            && (outpostValue is null || outpostValue is true) 
            && (cityValue is null || cityValue is true);
    }

    private bool ArePropertiesAllNull()
    {
        if (SelectedStarSystem is not null)
        {
            return false;
        }
        if (SelectedPlanet is not null)
        {
            return false;
        }
        if (SelectedMoon is not null)
        {
            return false;
        }
        if (SelectedSpaceStation is not null)
        {
            return false;
        }
        if (SelectedOutpost is not null)
        {
            return false;
        }
        if (SelectedCity is not null)
        {
            return false;
        }
        return true;
    }
}
