using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace UexCorpDataRunner.AvaloniaClient.Controls;
public partial class DefaultOverlayComboBox2 : ComboBox
{
    public string DefaultOverlayString
    {
        get { return (string)GetValue(DefaultOverlayStringProperty); }
        set { SetValue(DefaultOverlayStringProperty, value); }
    }

    public static readonly StyledProperty<string> DefaultOverlayStringProperty =
        AvaloniaProperty.Register<ComboBox, string>(nameof(DefaultOverlayString), "( Select )");

    public DefaultOverlayComboBox2()
    {
        InitializeComponent();
    }
}
