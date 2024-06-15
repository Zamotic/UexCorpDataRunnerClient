using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GrabAndScanPoC.Imaging.ImageRetrieval;
using System.Drawing;

namespace GrabAndScanPoC.Interface;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    string? selectedProcess;

    [ObservableProperty]
    List<string> processList = new List<string>();

    [ObservableProperty]
    Image? grabbedImage;

    public IRelayCommand ViewModelLoadedCommand { get => new RelayCommand(ViewModelLoadedExecute); }
    private void ViewModelLoadedExecute()
    {
        ProcessList = ImageCaptureService.GetAllWindowHandleNames();
    }

    public IRelayCommand GrabProcessImage { get => new RelayCommand(GrabProcessImageExecute); }
    private void GrabProcessImageExecute()
    {
        if (SelectedProcess is null)
        {
            return;
        }

        GrabbedImage = ImageCaptureService.GetBitmapScreenshot(SelectedProcess);
    }

    bool threadRunning = false;

    public IRelayCommand StartAutoGrabImage { get => new RelayCommand(StartAutoGrabImageExecuteAsync); }
    private async void StartAutoGrabImageExecuteAsync()
    {
        if (SelectedProcess is null)
        {
            return;
        }

        await Task.Run(() =>
        {
            threadRunning = true;
            do
            {
                GrabbedImage = ImageCaptureService.GetBitmapScreenshot(SelectedProcess);
            } while (threadRunning);
        }).ConfigureAwait(true);     
    }

    public IRelayCommand StopAutoGrabImage { get => new RelayCommand(StopAutoGrabImageExecute); }
    private void StopAutoGrabImageExecute()
    {
        threadRunning = false;
    }
}
