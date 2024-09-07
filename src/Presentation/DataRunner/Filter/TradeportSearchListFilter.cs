using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Data;
using UexCorpDataRunner.Domain.DataRunner;

namespace UexCorpDataRunner.Presentation.DataRunner.Filter;

public class TradeportSearchListFilter : Behavior<CollectionViewSource>
{
    public static readonly DependencyProperty SelectedStarSystemProperty =
    DependencyProperty.Register(nameof(SelectedStarSystem), typeof(StarSystem), typeof(TradeportSearchListFilter), new UIPropertyMetadata(OnSelectedStarSystemChangedCallBack));
    public StarSystem SelectedStarSystem
    {
        get { return (StarSystem)GetValue(SelectedStarSystemProperty); }
        set { SetValue(SelectedStarSystemProperty, value); }
    }

    private static void OnSelectedStarSystemChangedCallBack(DependencyObject sender, DependencyPropertyChangedEventArgs e)
    {
        TradeportSearchListFilter? f = sender as TradeportSearchListFilter;
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
        if (SelectedStarSystem is null)
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
        if (tradeport.System is null)
        {
            e.Accepted = false;
            return;
        }
        e.Accepted = tradeport.System.Equals(SelectedStarSystem.Code);
    }
}
