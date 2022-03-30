using UexCorpDataRunner.DesktopClient.ViewModels;

namespace UexCorpDataRunner.DesktopClient.Views;

public partial class MainPage : ContentPage
{
    public MainPage(MainPageViewModel mainPageViewModel)
    {
        BindingContext = mainPageViewModel;
        InitializeComponent();
    }
}
