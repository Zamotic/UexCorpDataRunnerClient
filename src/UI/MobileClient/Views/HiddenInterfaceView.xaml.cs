using UexCorpDataRunner.DesktopClient.Extensions;
using UexCorpDataRunner.DesktopClient.ViewModels;

namespace UexCorpDataRunner.DesktopClient.Views;

public partial class HiddenInterfaceView : ContentPage
{
    public HiddenInterfaceView(HiddenInterfaceViewModel viewModel)
    {
        BindingContext = viewModel;
        InitializeComponent();
    }

    private void ContentPage_Appearing(object sender, EventArgs e)
    {
        IWindow currentWindow = this.GetParentWindow();

        var location = currentWindow.GetWindowLocation();

        currentWindow.HideWindowButtons();
        currentWindow.Resize(37,77).Move(location.XLocation + (350 - 37), location.YLocation);
    }
}