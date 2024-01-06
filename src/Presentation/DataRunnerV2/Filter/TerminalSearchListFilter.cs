using Microsoft.Xaml.Behaviors;
using System;
using System.Windows;
using System.Windows.Data;
using UexCorpDataRunner.Domain.DataRunnerV2;

namespace UexCorpDataRunner.Presentation.DataRunnerV2.Filter;

public class TerminalSearchListFilter : Behavior<CollectionViewSource>
{
    public static readonly DependencyProperty SelectedStarSystemProperty =
    DependencyProperty.Register(nameof(SelectedStarSystem), typeof(StarSystem), typeof(TerminalSearchListFilter), new UIPropertyMetadata(OnSelectedStarSystemChangedCallBack));
    public StarSystem SelectedStarSystem
    {
        get { return (StarSystem)GetValue(SelectedStarSystemProperty); }
        set { SetValue(SelectedStarSystemProperty, value); }
    }

    private static void OnSelectedStarSystemChangedCallBack(DependencyObject sender, DependencyPropertyChangedEventArgs e)
    {
        TerminalSearchListFilter? f = sender as TerminalSearchListFilter;
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
        Terminal? terminal = e.Item as Terminal;
        if (terminal is null)
        {
            e.Accepted = false;
            return;
        }
        if(terminal.StarSystemId.Equals(SelectedStarSystem.Id) == false)
        {
            e.Accepted = false;
            return;
        }
        e.Accepted = true;
    }
}
