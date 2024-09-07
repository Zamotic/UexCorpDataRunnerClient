using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;

namespace UexCorpDataRunner.UILibrary.Extensions;
public class MouseDoubleClickBehavior
{
    public static readonly DependencyProperty MouseDoubleClickProperty = DependencyProperty.RegisterAttached("MouseDoubleClick", typeof(ICommand),
        typeof(MouseDoubleClickBehavior), new UIPropertyMetadata(MouseDoubleClickBehavior.MouseDoubleClickFired));

    public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.RegisterAttached("CommandParameter", typeof(object), 
        typeof(MouseDoubleClickBehavior), new UIPropertyMetadata(null));

    public static void SetMouseDoubleClick(DependencyObject target, ICommand value)
    {
        target.SetValue(MouseDoubleClickBehavior.MouseDoubleClickProperty, value);
    }

    public static object GetCommandParameter(DependencyObject target)
    {
        return target.GetValue(CommandParameterProperty);
    }
    public static void SetCommandParameter(DependencyObject target, object value)
    {
        target.SetValue(CommandParameterProperty, value);
    }

    private static void MouseDoubleClickFired(DependencyObject target, DependencyPropertyChangedEventArgs e)
    {
        Control? control = target as Control;
        if (control is null)
        {
            return;
        }

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
        {
            return;
        }
        ICommand command = (ICommand)control.GetValue(MouseDoubleClickBehavior.MouseDoubleClickProperty);
        object parameter = control.GetValue(CommandParameterProperty);
        command.Execute(parameter);
    }
}

