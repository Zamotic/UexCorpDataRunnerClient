using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows;

namespace UexCorpDataRunner.UILibrary.Controls;
public class AutoFilteredComboBox : ComboBox
{
    private int silenceEvents = 0;
    private System.Text.RegularExpressions.Regex regex = null;

    private new Boolean IsEditable { get; set; }
    private new Boolean IsTextSearchEnabled { get; set; }

    /// <summary>
    /// Creates a new instance of <see cref="AutoFilteredComboBox2" />.
    /// </summary>
    public AutoFilteredComboBox()
        : base()
    {
        DependencyPropertyDescriptor textProperty = DependencyPropertyDescriptor.FromProperty(
            ComboBox.TextProperty, typeof(AutoFilteredComboBox));
        textProperty.AddValueChanged(this, this.OnTextChanged);

        base.IsEditable = true;
        base.IsTextSearchEnabled = false;
    }

    #region DropDownOnFocus Dependency Property
    /// <summary>
    /// The <see cref="DependencyProperty"/> object of the <see cref="DropDownOnFocus" /> dependency property.
    /// </summary>
    public static readonly DependencyProperty DropDownOnFocusProperty =
        DependencyProperty.Register("DropDownOnFocus", typeof(bool), typeof(AutoFilteredComboBox), new UIPropertyMetadata(true));

    /// <summary>
    /// Gets or sets the way the combo box behaves when it receives focus.
    /// </summary>
    /// <value>The way the combo box behaves when it receives focus.</value>
    [System.ComponentModel.Description("The way the combo box behaves when it receives focus.")]
    [System.ComponentModel.Category("AutoFiltered ComboBox")]
    [System.ComponentModel.DefaultValue(true)]
    public bool DropDownOnFocus
    {
        get
        {
            return (bool)this.GetValue(DropDownOnFocusProperty);
        }
        set
        {
            this.SetValue(DropDownOnFocusProperty, value);
        }
    }
    #endregion

    #region | Handle selection |
    /// <summary>
    /// Called when <see cref="ComboBox.ApplyTemplate()"/> is called.
    /// </summary>
    [System.Diagnostics.DebuggerStepThrough]
    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        if (this.EditableTextBox != null)
        {
            this.EditableTextBox.SelectionChanged += this.EditableTextBox_SelectionChanged;
            //this.EditableTextBox.TextChanged += this.EditableTextBox_TextChanged;
        }
    }

    private TextBox _EditableTextBox = null;
    /// <summary>
    /// Gets the text box in charge of the editable portion of the combo box.
    /// </summary>
    protected TextBox EditableTextBox
    {
        get
        {
            if (_EditableTextBox == null)
            {
                _EditableTextBox = this.GetTemplateChild("PART_EditableTextBox") as TextBox;
            }
            return _EditableTextBox;
            //return ((TextBox)base.GetTemplateChild("PART_EditableTextBox"));
        }
    }

    private int start = 0, length = 0;

    private void EditableTextBox_SelectionChanged(object sender, RoutedEventArgs e)
    {
        if (this.IsFocused || EditableTextBox.IsFocused)
        {
            //if (this.ItemsSource != null && this.DropDownOnFocus && this.IsDropDownOpen == false)
            //{
            //	System.Diagnostics.Debug.Print("here3");
            //	this.IsDropDownOpen = true;
            //}

            TextBox tempSource = e.OriginalSource as TextBox;

            if (tempSource != null && !string.IsNullOrEmpty(tempSource.SelectedText))// && this.silenceEvents != 0)
            {
                if (tempSource.SelectionStart == 0 && tempSource.SelectionLength == 1)
                {
                    string tempText = tempSource.SelectedText;
                    tempSource.SelectedText = string.Empty;
                    e.Handled = true;
                    tempSource.Text = tempText;
                    tempSource.CaretIndex = tempText.Length;
                }
            }

            //if (this.silenceEvents == 0)
            //{
            //	this.start = ((TextBox)(e.OriginalSource)).SelectionStart;
            //	this.length = ((TextBox)(e.OriginalSource)).SelectionLength;

            //	this.RefreshFilter();
            //}
        }
    }

    //void EditableTextBox_TextChanged(object sender, TextChangedEventArgs e)
    //{
    //	this.RefreshFilter();
    //}
    #endregion

    #region | Handle focus |
    /// <summary>
    /// Invoked whenever an unhandled <see cref="UIElement.GotFocus" /> event
    /// reaches this element in its route.
    /// </summary>
    /// <param name="e">The <see cref="RoutedEventArgs" /> that contains the event data.</param>
    protected override void OnGotFocus(RoutedEventArgs e)
    {
        base.OnGotFocus(e);

        if (this.ItemsSource != null && this.DropDownOnFocus && this.IsDropDownOpen == false)
        {
            System.Diagnostics.Debug.Print("here2");
            this.IsDropDownOpen = true;
        }
    }

    /// <summary>
    /// Invoked whenever an unhandled event
    /// reaches this element in its route.
    /// </summary>
    /// <param name="e">The <see cref="KeyboardFocusChangedEventArgs" /> that contains the event data.</param>
    protected override void OnGotKeyboardFocus(KeyboardFocusChangedEventArgs e)
    {
        base.OnGotKeyboardFocus(e);

        if (this.ItemsSource != null && this.DropDownOnFocus && this.IsDropDownOpen == false)
        {
            System.Diagnostics.Debug.Print("here");
            this.IsDropDownOpen = true;
        }
    }
    #endregion

    #region | Handle filtering |
    private void RefreshFilter()
    {
        if (this.ItemsSource != null)
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(this.ItemsSource);

            // Check if the textbox contains the default text. If it does, provide no filter.
            string prefix = this.Text == "(Select Customer)" ? string.Empty : this.Text;

            // If the end of the text is selected, do not mind it.
            if (this.length > 0 && this.start + this.length == this.Text.Length)
            {
                prefix = prefix.Substring(0, this.start);
            }

            regex = new System.Text.RegularExpressions.Regex(System.Text.RegularExpressions.Regex.Escape(prefix), System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            view.Refresh();
        }
    }

    private bool FilterPredicate(object value)
    {
        // We don't like nulls.
        if (value == null)
            return false;

        // If there is no text, there's no reason to filter.
        if (this.Text.Length == 0)
            return true;

        string property = value.GetType().GetProperty(DisplayMemberPath).GetValue(value, null) as string;
        //string prefix = this.Text;

        //// If the end of the text is selected, do not mind it.
        //if (this.length > 0 && this.start + this.length == this.Text.Length)
        //{
        //	prefix = prefix.Substring(0, this.start);
        //}

        //System.Diagnostics.Debug.Print("regex = {0}, property = {1}, match = {2}", regex, property, regex.IsMatch(property));

        return regex.IsMatch(property);

        //return value.ToString()
        //	.StartsWith(prefix, !this.IsCaseSensitive, System.Globalization.CultureInfo.CurrentCulture);
    }
    #endregion

    /// <summary>
    /// Called when the source of an item in a selector changes.
    /// </summary>
    /// <param name="oldValue">Old value of the source.</param>
    /// <param name="newValue">New value of the source.</param>
    protected override void OnItemsSourceChanged(System.Collections.IEnumerable oldValue, System.Collections.IEnumerable newValue)
    {
        if (newValue != null)
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(newValue);
            view.Filter += this.FilterPredicate;
        }

        if (oldValue != null)
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(oldValue);
            view.Filter -= this.FilterPredicate;
        }

        base.OnItemsSourceChanged(oldValue, newValue);
    }

    private void OnTextChanged(object sender, EventArgs e)
    {
        if(this.IsDropDownOpen == false)
        {
            this.IsDropDownOpen = true;
        }
        if (this.silenceEvents == 0)
        {
            this.RefreshFilter();

            // Manually simulate the automatic selection that would have been
            // available if the IsTextSearchEnabled dependency property was set.
            //if (this.Text.Length > 0)
            //{
            //	foreach (object item in CollectionViewSource.GetDefaultView(this.ItemsSource))
            //	{
            //		string itemString = item.GetType().GetProperty(DisplayMemberPath).GetValue(item, null) as string;
            //		int text = itemString.Length, prefix = this.Text.Length;
            //		this.SelectedItem = item;

            //		this.silenceEvents++;

            //		EditableTextBox.Text = itemString.ToString();
            //		EditableTextBox.Select(prefix, text - prefix);
            //		this.silenceEvents--;
            //		break;
            //	}
            //}
        }
    }

    /// <summary>
    /// Confirm or cancel the selection when Tab, Enter, or Escape are hit. 
    /// Open the DropDown when the Down Arrow is hit.
    /// </summary>
    /// <param name="e">Key Event Args.</param>
    /// <remarks>
    /// The 'KeyDown' event is not raised for Arrows, Tab and Enter keys.
    /// It is swallowed by the DropDown if it's open.
    /// So use the Preview instead.
    /// </remarks>
    protected override void OnPreviewKeyDown(System.Windows.Input.KeyEventArgs e)
    {
        ICollectionView view = CollectionViewSource.GetDefaultView(this.ItemsSource);
        if (e.Key == Key.Tab || e.Key == Key.Enter)
        {
            if (!string.IsNullOrWhiteSpace(EditableTextBox.Text))
            {
                // Explicit Selection -> Close ItemsPanel
                if (!view.IsEmpty)
                {
                    view.MoveCurrentToFirst();
                    this.SelectedItem = view.CurrentItem;
                }
                else
                {
                    // Escape -> Close DropDown and redisplay Filter
                    this.IsDropDownOpen = false;
                    this.SelectedItem = null;
                    this.Text = EditableTextBox.Text = string.Empty;
                }
                this.IsDropDownOpen = false;
            }
        }
        else if (e.Key == Key.Escape)
        {
            // Escape -> Close DropDown and redisplay Filter
            this.SelectedItem = null;
            this.Text = EditableTextBox.Text = string.Empty;
        }
        //else if (e.Key == Key.Up || e.Key == Key.Down)
        //{
        //	ICollectionView view = CollectionViewSource.GetDefaultView(this.ItemsSource);
        //	if (e.Key == Key.Up)
        //	{
        //		view.MoveCurrentToPrevious();
        //	}
        //	else
        //	{
        //		view.MoveCurrentToNext();
        //	}

        //	e.Handled = true;
        //}
        else
        {
            base.OnPreviewKeyDown(e);
        }
    }
}


