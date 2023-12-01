using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace UexCorpDataRunner.UILibrary.Extensions;
public class ComboBoxItemTemplateSelector : DataTemplateSelector
{
    public DataTemplate DropDownTemplate
    {
        get;
        set;
    }
    public DataTemplate SelectedTemplate
    {
        get;
        set;
    }
    public override DataTemplate SelectTemplate(object item, DependencyObject container)
    {
        ComboBoxItem comboBoxItem = VisualTreeHelpers.GetVisualParent<ComboBoxItem>(container);
        if (comboBoxItem != null)
        {
            return DropDownTemplate;
        }
        return SelectedTemplate;
    }
}
