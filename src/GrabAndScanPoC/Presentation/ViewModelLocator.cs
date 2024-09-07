using System.Windows;

namespace GrabAndScanPoC.Presentation;

public static class ViewModelLocator
{
    public static DependencyProperty AutoWireViewModelProperty = DependencyProperty.RegisterAttached("AutoWireViewModel", typeof(bool),
        typeof(ViewModelLocator), new PropertyMetadata(false, AutoWireViewModelChanged));

    public static System.Reflection.Assembly? InterfaceAssembly { get; }

    static ViewModelLocator()
    {
        InterfaceAssembly = System.Reflection.Assembly.GetAssembly(typeof(GrabAndScanPoC.Interface.StartupExtensions));
        if (InterfaceAssembly is null)
        {
            throw new Exception("Application Assembly could not be found");
        }
    }

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

        frameworkElement.DataContext = StartupExtensions.ServiceProvider?.GetService(viewModelType);
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
        string? viewModelName = null;

        if (viewName.EndsWith("View"))
        {
            viewModelName = viewType.FullName
                .Replace("Presentation", "Interface")
                .Replace("View", "ViewModel");
        }

        Type? viewModelType = InterfaceAssembly?.GetType(viewModelName);

        return viewModelType;
    }
}
