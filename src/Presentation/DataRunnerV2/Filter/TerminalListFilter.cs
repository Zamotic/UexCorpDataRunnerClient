using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Data;
using UexCorpDataRunner.Domain.DataRunnerV2;

namespace UexCorpDataRunner.Presentation.DataRunnerV2.Filter;

public class TerminalListFilter : Behavior<CollectionViewSource>
{
    public static readonly DependencyProperty SelectedStarSystemProperty =
    DependencyProperty.Register(nameof(SelectedStarSystem), typeof(StarSystem), typeof(TerminalListFilter), new UIPropertyMetadata(OnSelectedStarSystemChangedCallBack));
    public StarSystem SelectedStarSystem
    {
        get { return (StarSystem)GetValue(SelectedStarSystemProperty); }
        set { SetValue(SelectedStarSystemProperty, value); }
    }

    private static void OnSelectedStarSystemChangedCallBack(DependencyObject sender, DependencyPropertyChangedEventArgs e)
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
        if (SelectedStarSystem is null)
        {
            e.Accepted = false;
            return;
        }
        Terminal? termial = e.Item as Terminal;
        if (termial is null)
        {
            e.Accepted = false;
            return;
        }
        e.Accepted = termial.StarSystemId.Equals(SelectedStarSystem.Id);
    }
}
