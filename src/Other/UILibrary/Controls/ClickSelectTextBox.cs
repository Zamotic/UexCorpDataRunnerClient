using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace UexCorpDataRunner.UILibrary.Controls;
public class ClickSelectTextBox : TextBox
{
    public ClickSelectTextBox()
    {
        AddHandler(PreviewMouseLeftButtonDownEvent,
          new MouseButtonEventHandler(SelectivelyIgnoreMouseButton), true);
        AddHandler(GotKeyboardFocusEvent,
          new RoutedEventHandler(SelectAllText), true);
        AddHandler(MouseDoubleClickEvent,
          new RoutedEventHandler(SelectAllText), true);
        AddHandler(KeyDownEvent,
          new KeyEventHandler(HandleKeyDown), true);
    }

    private static void SelectivelyIgnoreMouseButton(object sender,
                                                     MouseButtonEventArgs e)
    {
        // Find the TextBox
        DependencyObject? parent = e.OriginalSource as UIElement;
        while (parent != null && !(parent is TextBox))
            parent = VisualTreeHelper.GetParent(parent);

        if (parent != null)
        {
            var textBox = (TextBox)parent;
            if (!textBox.IsKeyboardFocusWithin)
            {
                // If the text box is not yet focussed, give it the focus and
                // stop further processing of this click event.
                textBox.Focus();
                e.Handled = true;
            }
        }
    }

    private static void SelectAllText(object sender, RoutedEventArgs e)
    {
        var textBox = e.OriginalSource as TextBox;
        if (textBox != null)
            textBox.SelectAll();
    }

    private static void HandleKeyDown(object sender, KeyEventArgs e)
    {
        var textBox = e.OriginalSource as TextBox;

        if (textBox is null)
        {
            return;
        }

        if(e.Key == Key.Escape)
        {
            textBox.Text = null;
            return;
        }

        if(e.Key == Key.Enter)
        {
            MoveFocusToNextIfTrue(textBox, e.IsDown);
            return;
        }

        if (e.Key == Key.Return)
        {
            MoveFocusToNextIfTrue(textBox, e.IsDown);
            return;
        }
    }

    private static void MoveFocusToNextIfTrue(TextBox textBox, bool value)
    {
        if(value == false)
        {
            return;
        }

        textBox.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
    }
}
