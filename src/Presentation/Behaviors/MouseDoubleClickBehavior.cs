using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace UexCorpDataRunner.Presentation.Behaviors;
public class MouseDoubleClickBehavior
{
    public static readonly DependencyProperty MouseDoubleClickProperty = DependencyProperty.RegisterAttached("MouseDoubleClick", typeof(ICommand),
        typeof(MouseDoubleClickBehavior), new UIPropertyMetadata(MouseDoubleClickBehavior.MouseDoubleClickFired));

    public static void SetMouseDoubleClick(DependencyObject target, ICommand value)
    {
        target.SetValue(MouseDoubleClickBehavior.MouseDoubleClickProperty, value);
    }

    private static void MouseDoubleClickFired(DependencyObject target, DependencyPropertyChangedEventArgs e)
    {
        Control? control = target as Control;
        if (control is null)
            return;

        if ((e.NewValue != null) && (e.OldValue == null))
        {
            control.MouseDoubleClick += MouseDoubleClick;
        }
        else if ((e.NewValue == null) && (e.OldValue != null))
        {
            control.MouseDoubleClick -= MouseDoubleClick;
        }
    }

    private static void MouseDoubleClick(object sender, RoutedEventArgs e)
    {
        Control? control = sender as Control;
        if (control is null)
            return;

        ICommand command = (ICommand)control.GetValue(MouseDoubleClickBehavior.MouseDoubleClickProperty);
        object[] arguments = new object[] { };
        command.Execute(arguments);
    }
}

