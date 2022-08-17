using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UexCorpDataRunner.UILibrary.Controls;

/// <summary>
/// Interaction logic for DefaultOverlayComboBox.xaml
/// </summary>
public partial class DefaultOverlayComboBox2 : ComboBox
{
	public string DefaultOverlayString
	{
		get { return (string)GetValue(DefaultOverlayStringProperty); }
		set { SetValue(DefaultOverlayStringProperty, value); }
	}

	// Using a DependencyProperty as the backing store for DefaultOverlayString.  This enables animation, styling, binding, etc...
	public static readonly DependencyProperty DefaultOverlayStringProperty =
		DependencyProperty.Register("DefaultOverlayString", typeof(string), typeof(DefaultOverlayComboBox2), new PropertyMetadata("( Select )"));


	public DefaultOverlayComboBox2()
    {
        InitializeComponent();
    }
}

