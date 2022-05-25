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
/// Interaction logic for DefaultButtonControl.xaml
/// </summary>
public partial class DefaultButtonControl : UserControl
{
    #region		SaveButton
    public ICommand SaveButtonCommand
    {
        get { return (ICommand)GetValue(SaveButtonCommandProperty); }
        set { SetValue(SaveButtonCommandProperty, value); }
    }

    // Using a DependencyProperty as the backing store for SaveButtonCommand.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty SaveButtonCommandProperty =
        DependencyProperty.Register("SaveButtonCommand", typeof(ICommand), typeof(DefaultButtonControl));

    public string SaveButtonText
    {
        get { return (string)GetValue(SaveButtonTextProperty); }
        set { SetValue(SaveButtonTextProperty, value); }
    }

    // Using a DependencyProperty as the backing store for SaveButtonText.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty SaveButtonTextProperty =
        DependencyProperty.Register("SaveButtonText", typeof(string), typeof(DefaultButtonControl), new UIPropertyMetadata());
    #endregion	SaveButton

    #region		CancelButton
    public ICommand CancelButtonCommand
    {
        get { return (ICommand)GetValue(CancelButtonCommandProperty); }
        set { SetValue(CancelButtonCommandProperty, value); }
    }

    // Using a DependencyProperty as the backing store for CancelButtonCommand.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty CancelButtonCommandProperty =
        DependencyProperty.Register("CancelButtonCommand", typeof(ICommand), typeof(DefaultButtonControl), new UIPropertyMetadata());

    public string CancelButtonText
    {
        get { return (string)GetValue(CancelButtonTextProperty); }
        set { SetValue(CancelButtonTextProperty, value); }
    }

    // Using a DependencyProperty as the backing store for CancelButtonText.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty CancelButtonTextProperty =
        DependencyProperty.Register("CancelButtonText", typeof(string), typeof(DefaultButtonControl), new UIPropertyMetadata());

    public bool CancelButtonIsVisible
    {
        get { return (bool)GetValue(CancelButtonIsVisibleProperty); }
        set { SetValue(CancelButtonIsVisibleProperty, value); }
    }

    // Using a DependencyProperty as the backing store for CancelButtonIsVisible.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty CancelButtonIsVisibleProperty =
        DependencyProperty.Register("CancelButtonIsVisible", typeof(bool), typeof(DefaultButtonControl), new UIPropertyMetadata());
    #endregion		CancelButton

    #region		ThirdButton
    public ICommand ThirdButtonCommand
    {
        get { return (ICommand)GetValue(ThirdButtonCommandProperty); }
        set { SetValue(ThirdButtonCommandProperty, value); }
    }

    // Using a DependencyProperty as the backing store for ThirdButtonCommand.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty ThirdButtonCommandProperty =
        DependencyProperty.Register("ThirdButtonCommand", typeof(ICommand), typeof(DefaultButtonControl), new UIPropertyMetadata());

    public string ThirdButtonText
    {
        get { return (string)GetValue(ThirdButtonTextProperty); }
        set { SetValue(ThirdButtonTextProperty, value); }
    }

    // Using a DependencyProperty as the backing store for ThirdButtonText.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty ThirdButtonTextProperty =
        DependencyProperty.Register("ThirdButtonText", typeof(string), typeof(DefaultButtonControl), new UIPropertyMetadata());

    public bool ThirdButtonIsVisible
    {
        get { return (bool)GetValue(ThirdButtonIsVisibleProperty); }
        set { SetValue(ThirdButtonIsVisibleProperty, value); }
    }

    // Using a DependencyProperty as the backing store for ThirdButtonIsVisible.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty ThirdButtonIsVisibleProperty =
        DependencyProperty.Register("ThirdButtonIsVisible", typeof(bool), typeof(DefaultButtonControl), new UIPropertyMetadata());
    #endregion		ThirdButton

    #region		General
    public int ButtonWidth
    {
        get { return (int)GetValue(ButtonWidthProperty); }
        set { SetValue(ButtonWidthProperty, value); }
    }

    // Using a DependencyProperty as the backing store for ButtonWidth.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty ButtonWidthProperty =
        DependencyProperty.Register("ButtonWidth", typeof(int), typeof(DefaultButtonControl), new UIPropertyMetadata(75));

    public Thickness ButtonMargin
    {
        get { return (Thickness)GetValue(ButtonMarginProperty); }
        set { SetValue(ButtonMarginProperty, value); }
    }

    // Using a DependencyProperty as the backing store for ButtonMargin.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty ButtonMarginProperty =
        DependencyProperty.Register("ButtonMargin", typeof(Thickness), typeof(DefaultButtonControl), new UIPropertyMetadata(new Thickness(1, 0, 1, 0)));
    #endregion		General

    public DefaultButtonControl()
    {
        InitializeComponent();
        //LayoutRoot.DataContext = this;
    }
}