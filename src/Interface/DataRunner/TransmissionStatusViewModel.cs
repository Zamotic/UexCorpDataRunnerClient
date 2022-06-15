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
    }

#if DEBUG
    Timer _testButtonTimer;
    Timer _testTextTimer;
#endif
    Timer _readTextTimer;
    //public void ShowSettingsInterfaceNotified(ShowSettingsInterfaceMessage notification)
    public void ShowTransmissionStatusMessageHandler(object sender, ShowTransmissionStatusMessage notification)
    {
        TransmissionStatusText = string.Empty;
        _statusBufferQueue.Clear();

        IsEnabled = true;
        IsTransmissionInProgress = true;

#if DEBUG
        _testButtonTimer = new Timer((TimerCallback) => 
        { 
            IsTransmissionInProgress = false;
            _testButtonTimer.Change(Timeout.Infinite, 0);
            _testTextTimer.Change(Timeout.Infinite, 0);
        }, null, TimeSpan.FromMilliseconds(5000), TimeSpan.FromMilliseconds(250));

        _testTextTimer = new Timer((TimerCallback) =>
        {
            _statusBufferQueue.Enqueue("Submitting Commodity 'PRFO'...SUCCESS!\n");
        }, null, TimeSpan.FromMilliseconds(500), TimeSpan.FromMilliseconds(100));
#endif
        _readTextTimer = new Timer((TimerCallback) =>
        {
            if(_statusBufferQueue.TryDequeue(out string? message) == true)
            {
                if(string.IsNullOrWhiteSpace(message) == false)
                {
                    TransmissionStatusText += message;
                }
            }
        }, null, TimeSpan.FromMilliseconds(50), TimeSpan.FromMilliseconds(50));
    }
}
