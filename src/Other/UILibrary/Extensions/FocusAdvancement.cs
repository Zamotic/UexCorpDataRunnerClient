﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace UexCorpDataRunner.UILibrary.Extensions;
public static class FocusAdvancement
{
    public static bool GetAdvancesByEnterKey(DependencyObject obj)
    {
        return (bool)obj.GetValue(AdvancesByEnterKeyProperty);
    }

    public static void SetAdvancesByEnterKey(DependencyObject obj, bool value)
    {
        obj.SetValue(AdvancesByEnterKeyProperty, value);
    }

    public static readonly DependencyProperty AdvancesByEnterKeyProperty =
        DependencyProperty.RegisterAttached("AdvancesByEnterKey", typeof(bool), typeof(FocusAdvancement),
        new UIPropertyMetadata(OnAdvancesByEnterKeyPropertyChanged));

    static void OnAdvancesByEnterKeyPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var element = d as UIElement;
        if (element == null) return;

        if ((bool)e.NewValue) element.KeyDown += Keydown;
        else element.KeyDown -= Keydown;
    }

    static void Keydown(object sender, KeyEventArgs e)
    {
        if (!e.Key.Equals(Key.Enter)) return;

        var element = sender as UIElement;
        if (element != null) element.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
    }
}
