using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;

namespace UexCorpDataRunner.UILibrary.Extensions;
public static class VisualTreeHelpers
{
    public static T GetVisualParent<T>(object childObject) where T : Visual
    {
        DependencyObject child = childObject as DependencyObject;
        while ((child != null) && !(child is T))
        {
            child = VisualTreeHelper.GetParent(child);
        }
        return child as T;
    }
}
