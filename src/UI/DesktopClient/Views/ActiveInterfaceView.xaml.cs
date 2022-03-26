using UexCorpDataRunner.DesktopClient.ViewModels;

namespace UexCorpDataRunner.DesktopClient.Views;

public partial class ActiveInterfaceView : ContentPage
{
	public ActiveInterfaceView(ActiveInterfaceViewModel viewModel)
	{
		BindingContext = viewModel;
		InitializeComponent();
	}
}