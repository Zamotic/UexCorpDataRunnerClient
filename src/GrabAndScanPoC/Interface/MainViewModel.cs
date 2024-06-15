using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GrabAndScanPoC.Imaging.ImageRetrieval;

namespace GrabAndScanPoC.Interface;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    string? selectedProcess;

    [ObservableProperty]
    List<string> processList = new List<string>();

    public IRelayCommand ViewModelLoadedCommand { get => new RelayCommand(ViewModelLoadedExecute); }
    private void ViewModelLoadedExecute()
    {
        ProcessList = ImageCaptureService.GetAllWindowHandleNames();
    }
}
