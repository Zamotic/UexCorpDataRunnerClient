using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Data;
using UexCorpDataRunner.Domain.DataRunner;

namespace UexCorpDataRunner.Presentation.DataRunner.Filter;

public class TradeportListFilter : Behavior<CollectionViewSource>
{
    public static readonly DependencyProperty SelectedPlanetProperty =
    DependencyProperty.Register(nameof(SelectedPlanet), typeof(Planet), typeof(TradeportListFilter), new UIPropertyMetadata(OnSelectedPlanetChangedCallBack));
    public Planet SelectedPlanet
    {
        get { return (Planet)GetValue(SelectedPlanetProperty); }
        set { SetValue(SelectedPlanetProperty, value); }
    }

    private static void OnSelectedPlanetChangedCallBack(DependencyObject sender, DependencyPropertyChangedEventArgs e)
    {
        TradeportListFilter? f = sender as TradeportListFilter;
        if (f != null)
        {
            f.OnPropertyChanged();
        }
    }

    public static readonly DependencyProperty SelectedSatelliteProperty =
    DependencyProperty.Register(nameof(SelectedSatellite), typeof(Satellite), typeof(TradeportListFilter), new UIPropertyMetadata(OnSelectedSatelliteChangedCallBack));
    public Satellite SelectedSatellite
    {
        get { return (Satellite)GetValue(SelectedSatelliteProperty); }
        set { SetValue(SelectedSatelliteProperty, value); }
    }

    private static void OnSelectedSatelliteChangedCallBack(DependencyObject sender, DependencyPropertyChangedEventArgs e)
    {
        TradeportListFilter? f = sender as TradeportListFilter;
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
        if (SelectedPlanet is null)
        {
            e.Accepted = false;
            return;
        }
        Tradeport? tradeport = e.Item as Tradeport;
        if (tradeport is null)
        {
            e.Accepted = false;
            return;
        }
        if (SelectedSatellite is null)
        {
            e.Accepted = ((tradeport.Planet?.Equals(SelectedPlanet.Code) == true) && (string.IsNullOrWhiteSpace(tradeport.Satellite) == true));
            return;
        }
        if (tradeport.Satellite is null)
        {
            e.Accepted = false;
            return;
        }
        e.Accepted = tradeport.Satellite.Equals(SelectedSatellite.Code);
    }
}
