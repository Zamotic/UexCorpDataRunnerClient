using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Windows.Input;
using UexCorpDataRunner.Application.Common;
using UexCorpDataRunner.Interface.MessengerMessages;

namespace UexCorpDataRunner.Interface.DataRunner;
public class TransmissionStatusViewModel : ViewModelBase
{
    IMessenger _messenger;

    private bool _isTransmissionInProgress = false;
    public bool IsTransmissionInProgress
    {
        get => _isTransmissionInProgress;
        set => SetProperty(ref _isTransmissionInProgress, value);
    }
    private System.Collections.Concurrent.ConcurrentQueue<string> _statusBufferQueue = new System.Collections.Concurrent.ConcurrentQueue<string>();

    private string _transmissionStatusText = string.Empty;
    public string TransmissionStatusText
    {
        get => _transmissionStatusText;
        set => SetProperty(ref _transmissionStatusText, value);
    }

    public TransmissionStatusViewModel(IMessenger messenger)
    {
        _messenger = messenger;
        _messenger.Register<ShowTransmissionStatusMessage>(this, ShowTransmissionStatusMessageHandler);
        _messenger.Register<TransmissionStatusCompleteMessage>(this, TransmissionStatusCompleteMessageHandler);
    }

    public ICommand CloseTransmissionStatusViewCommand { get => new RelayCommand(CloseTransmissionStatusViewCommandExecute, CloseTransmissionStatusViewCommandCanExecute); }
    private bool CloseTransmissionStatusViewCommandCanExecute()
    {
        if(IsTransmissionInProgress == true)
        {
            return false;
        }

        return true;
    }
    private void CloseTransmissionStatusViewCommandExecute()
    {
        IsEnabled = false;
        _messenger.Send(new CloseTransmissionStatusMessage());
    }

    public ICommand CancelCurrentDataTransmissionCommand { get => new RelayCommand(CancelCurrentDataTransmissionCommandExecute, CancelCurrentDataTransmissionCommandCanExecute); }
    private bool CancelCurrentDataTransmissionCommandCanExecute()
    {
        if (_isTransmissionInProgress == true)
        {
            return true;
        }

        return false;
    }
    private void CancelCurrentDataTransmissionCommandExecute()
    {
        _messenger.Send(new CancelCurrentDataTransmissionMessage());
        IsEnabled = false;
        IsTransmissionInProgress = false;
    }

    Timer? _readTextTimer;
    public void ShowTransmissionStatusMessageHandler(object sender, ShowTransmissionStatusMessage notification)
    {
        TransmissionStatusText = string.Empty;

        IsEnabled = true;
        IsTransmissionInProgress = true;

        _statusBufferQueue = notification.Queue;

        _readTextTimer = new Timer((TimerCallback) =>
        {
            ReadStatusBufferQueue();
        }, null, TimeSpan.FromMilliseconds(50), TimeSpan.FromMilliseconds(50));
    }

    public void TransmissionStatusCompleteMessageHandler(object sender, TransmissionStatusCompleteMessage notification)
    {
        IsTransmissionInProgress = false;
        _readTextTimer?.Change(Timeout.Infinite, 0);
        ReadStatusBufferQueue();
        TransmissionStatusText += $"\n\n{notification.ResponseMessage}";
    }

    public void ReadStatusBufferQueue()
    {
        while(true)
        {
            if (_statusBufferQueue.TryDequeue(out string? message) == true)
            {
                if (string.IsNullOrWhiteSpace(message) == false)
                {
                    TransmissionStatusText += message;
                }
                continue;
            }
            break;
        }
    }
}
