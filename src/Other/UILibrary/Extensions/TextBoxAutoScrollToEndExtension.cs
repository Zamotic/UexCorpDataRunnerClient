using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace UexCorpDataRunner.UILibrary.Extensions;
public class TextBoxAutoScrollToEndExtension : DependencyObject
{
	public static readonly DependencyProperty IsEnabledProperty = DependencyProperty.RegisterAttached("IsEnabled", typeof(bool),
	typeof(TextBoxAutoScrollToEndExtension), new UIPropertyMetadata(false, IsEnabledChanged));

	[AttachedPropertyBrowsableForType(typeof(TextBox))]
	public static bool GetIsEnabled(DependencyObject obj)
	{
		return (bool)obj.GetValue(IsEnabledProperty);
	}
	[AttachedPropertyBrowsableForType(typeof(TextBox))]
	public static void SetIsEnabled(DependencyObject obj, bool value)
	{
		obj.SetValue(IsEnabledProperty, value);
	}

	static void IsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		var textBox = d as TextBox;
		if (textBox == null)
			return;
		if ((bool)e.NewValue)
		{
			textBox.Unloaded += textBox_Unloaded;
			textBox.Loaded += textBox_Loaded;
		}
	}

	static void textBox_Loaded(object sender, RoutedEventArgs e)
	{
		var textBox = sender as TextBox;
		if (textBox == null)
			return;

		textBox.TextChanged += TextChanged;
	}

	private static void textBox_Unloaded(object sender, RoutedEventArgs e)
	{
		var textBox = sender as TextBox;
		if (textBox == null)
			return;

		textBox.TextChanged -= TextChanged;
	}

	//private static void TextChangedFired(DependencyObject target, DependencyPropertyChangedEventArgs e)
	//{
	//	TextBox? control = target as TextBox;
	//	if (control != null)
	//	{
	//		if ((e.NewValue != null) && (e.OldValue == null))
	//		{
	//			control.TextChanged += TextChanged;
	//		}
	//		else if ((e.NewValue == null) && (e.OldValue != null))
	//		{
	//			control.TextChanged -= TextChanged;
	//		}
	//	}
	//}

	private static void TextChanged(object sender, RoutedEventArgs e)
	{
		TextBox? textBox = sender as TextBox;
		if (textBox != null)
		{
			textBox.ScrollToEnd();
		}
	}
}
