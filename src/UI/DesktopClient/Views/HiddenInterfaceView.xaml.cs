using UexCorpDataRunner.DesktopClient.ViewModels;

namespace UexCorpDataRunner.DesktopClient.Views;

public partial class HiddenInterfaceView : ContentPage
{
	public HiddenInterfaceView(HiddenInterfaceViewModel viewModel)
	{
		BindingContext = viewModel;
		InitializeComponent();
	}
}