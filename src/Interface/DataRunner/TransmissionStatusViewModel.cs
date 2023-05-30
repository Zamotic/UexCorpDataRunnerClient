using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using UexCorpDataRunner.Application.Common;
using UexCorpDataRunner.Domain.Services;
using UexCorpDataRunner.Interface.MessengerMessages;

namespace UexCorpDataRunner.Interface.DataRunner;
public class TransmissionStatusViewModel : ViewModelBase
{
    private readonly IMessenger _Messenger;
    private readonly ISettingsService _SettingsService;

    private bool _IsTransmissionInProgress = false;
    public bool IsTransmissionInProgress
    {
        get => _IsTransmissionInProgress;
        set => SetProperty(ref _IsTransmissionInProgress, value);
    }
    private System.Collections.Concurrent.ConcurrentQueue<string> _statusBufferQueue = new System.Collections.Concurrent.ConcurrentQueue<string>();

    private bool _IsCloseTransmissionStatusFocused = false;
    public bool IsCloseTransmissionStatusFocused
    {
        get => _IsCloseTransmissionStatusFocused;
        set => SetProperty(ref _IsCloseTransmissionStatusFocused, value);
    }    
    
    private bool _IsTransmissionStatusTextBoxFocused = false;
    public bool IsTransmissionStatusTextBoxFocused
    {
        get => _IsTransmissionStatusTextBoxFocused;
        set => SetProperty(ref _IsTransmissionStatusTextBoxFocused, value);
    }

    private string _TransmissionStatusText = string.Empty;
    public string TransmissionStatusText
    {
        get => _TransmissionStatusText;
        set => SetProperty(ref _TransmissionStatusText, value);
    }

    private string _CloseTransmissionStatusButtonText = "Ok";
    public string CloseTransmissionStatusButtonText
    {
        get => _CloseTransmissionStatusButtonText;
        set => SetProperty(ref _CloseTransmissionStatusButtonText, value);
    }

    public TransmissionStatusViewModel(IMessenger messenger, ISettingsService settingsService)
    {
        _Messenger = messenger;
        _SettingsService = settingsService;
        _Messenger.Register<ShowTransmissionStatusMessage>(this, ShowTransmissionStatusMessageHandler);
        _Messenger.Register<TransmissionStatusCompleteMessage>(this, TransmissionStatusCompleteMessageHandler);
    }

    public IRelayCommand CloseTransmissionStatusViewCommand { get => new RelayCommand(CloseTransmissionStatusViewCommandExecute, CloseTransmissionStatusViewCommandCanExecute); }
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
        _Messenger.Send(new CloseTransmissionStatusMessage());
    }

    public IRelayCommand CancelCurrentDataTransmissionCommand { get => new RelayCommand(CancelCurrentDataTransmissionCommandExecute, CancelCurrentDataTransmissionCommandCanExecute); }
    private bool CancelCurrentDataTransmissionCommandCanExecute()
    {
        if (IsTransmissionInProgress == true)
        {
            return true;
        }

        return false;
    }
    private void CancelCurrentDataTransmissionCommandExecute()
    {
        _Messenger.Send(new CancelCurrentDataTransmissionMessage());
        IsEnabled = false;
        IsTransmissionInProgress = false;
    }

    Timer? _ReadTextTimer;
    public void ShowTransmissionStatusMessageHandler(object sender, ShowTransmissionStatusMessage notification)
    {
        TransmissionStatusText = string.Empty;
        _statusBufferQueue.Clear();

        IsEnabled = true;
        IsTransmissionInProgress = true;

        TransmissionStatusText = string.Empty;

        IsEnabled = true;
        IsTransmissionInProgress = true;

        _statusBufferQueue = notification.Queue;

        _ReadTextTimer = new Timer((TimerCallback) =>
        {
            ReadStatusBufferQueue();
        }, null, TimeSpan.FromMilliseconds(100), TimeSpan.FromMilliseconds(50));
    }

    Timer? _CloseWindowTextTimer;

    public void TransmissionStatusCompleteMessageHandler(object sender, TransmissionStatusCompleteMessage notification)
    {
        IsTransmissionInProgress = false;
        _ReadTextTimer?.Change(Timeout.Infinite, 0);
        ReadStatusBufferQueue();
        TransmissionStatusText += $"\n\n{notification.ResponseMessage}";
        IsTransmissionStatusTextBoxFocused = true;
        IsCloseTransmissionStatusFocused = true;

        if(notification.AllResponsesSucceeded == false)
        {
            return;
        }

        StartCloseWindowTimer();
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

    private void StartCloseWindowTimer()
    {
        if (_SettingsService.Settings is null)
        {
            return;
        };

        if (_SettingsService.Settings.AutoCloseSummaryWindow == false)
        {
            return;
        }

        short countDownTimeCounter = _SettingsService.Settings.AutoCloseSummaryTime;

        _CloseWindowTextTimer = new Timer((TimerCallback) =>
        {
            CloseTransmissionStatusButtonText = $"Ok ({countDownTimeCounter--})";
            if (countDownTimeCounter <= -1)
            {
                _CloseWindowTextTimer?.Change(Timeout.Infinite, 0);
                _CloseWindowTextTimer?.Dispose();
                _CloseWindowTextTimer = null;
                CloseTransmissionStatusButtonText = $"Ok";
                CloseTransmissionStatusViewCommandExecute();
            }
        }, null, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1));
    }
}
