using UexCorpDataRunner.DesktopClient.Extensions;
using UexCorpDataRunner.DesktopClient.ViewModels;

namespace UexCorpDataRunner.DesktopClient.Views;

public partial class ActiveInterfaceView : ContentPage
{
    public ActiveInterfaceView(ActiveInterfaceViewModel viewModel)
	{
        BindingContext = viewModel;
		InitializeComponent();
	}

    private void ContentPage_Appearing(object sender, EventArgs e)
    {
        IWindow currentWindow = this.GetParentWindow();

        if (currentWindow?.Handler?.PlatformView is null) {
            Dispatcher.DispatchDelayed(new TimeSpan(0, 0, 0, 0, 100), () => {
                var displayBounds = Microsoft.UI.Windowing.DisplayArea.Primary.OuterBounds;
                int displayWidth = displayBounds.Width;
                int displayHeight = displayBounds.Height;

                int windowY = (displayHeight / 2) - 350;
                int windowX = (displayWidth / 2) - 175;

                currentWindow?.MoveAndResize(windowX, windowY, 350, 700);
            });
            return;
        }

        var location = currentWindow.GetWindowLocation();

        currentWindow.Resize(350, 700).Move(location.XLocation - (350 - 37), location.YLocation).ShowWindowButtons();

    }
}