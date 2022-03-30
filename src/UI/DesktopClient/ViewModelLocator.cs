using System;
using System.Diagnostics;
using System.Windows;

namespace UexCorpDataRunner.DesktopClient;

public static class ViewModelLocator
{
    public static DependencyProperty AutoWireViewModelProperty = DependencyProperty.RegisterAttached("AutoWireViewModel", typeof(bool),
        typeof(ViewModelLocator), new PropertyMetadata(false, AutoWireViewModelChanged));

    public static bool GetAutoWireViewModel(UIElement element)
    {
        return (bool)element.GetValue(AutoWireViewModelProperty);
    }

    public static void SetAutoWireViewModel(UIElement element, bool value)
    {
        element.SetValue(AutoWireViewModelProperty, value);
    }

    private static void AutoWireViewModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(d) == true)
        {
            return;
        }

        if ((bool)e.NewValue)
            Bind(d);
    }

    private static void Bind(DependencyObject view)
    {
        if (view is null)
        {
            throw new ArgumentNullException(nameof(view));
        }

        FrameworkElement? frameworkElement = view as FrameworkElement;
        if (frameworkElement is null)
        {
            throw new ArgumentException("View was not of type FrameworkElement");
        }

        var viewModelType = FindViewModel(frameworkElement.GetType());

        if (viewModelType is null)
        {
            throw new Exception($"View model for {frameworkElement.Name} could not be found.");
        }

        frameworkElement.DataContext = App.ServiceProvider.GetService(viewModelType);
    }

    private static Type? FindViewModel(Type viewType)
    {
        if (viewType is null)
        {
            throw new ArgumentNullException(nameof(viewType));
        }

        if (string.IsNullOrWhiteSpace(viewType.FullName) == true)
        {
            throw new Exception("Parameter viewType has no value for FullName");
        }

        string viewName = viewType.FullName;

        if (viewName.EndsWith("View")) {
            viewName = viewType.FullName
                .Replace("View", "ViewModel")
                .Replace("Views", "ViewModels");
        }

        Type? viewModelType = Type.GetType(viewName);
        return viewModelType;
    }
}
