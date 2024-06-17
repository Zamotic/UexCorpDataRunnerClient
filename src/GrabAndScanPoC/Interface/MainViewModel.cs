using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using GrabAndScanPoC.Common;
using GrabAndScanPoC.Core.Messengers;
using GrabAndScanPoC.Imaging.ImageRetrieval;
using GrabAndScanPoC.Imaging.TextRetrieval;
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

    [ObservableProperty]
    string? imageExtractText;

    public ITextExtractor _textExtractor;
    public IMessenger _messenger;

    public MainViewModel(IMessenger messenger, ITextExtractor textExtractor)
    {
        _messenger = messenger;
        _textExtractor = textExtractor;
    }

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

    public IRelayCommand ExtractText { get => new RelayCommand(ExtractTextExecute); }
    private void ExtractTextExecute()
    {
        if(GrabbedImage is null)
        {
            _messenger.Send(new MessageBoxMessage(new MessageBox("No image to extract text from.", "No Image Selected", MessageBoxImage.Error, MessageBoxButton.Ok)));
            return;
        }

        ImageExtractText = _textExtractor.ExtractTextFromImage(GrabbedImage!);
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
