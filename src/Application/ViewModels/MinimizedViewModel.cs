using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using UexCorpDataRunner.Application.Common;
using UexCorpDataRunner.Application.MessengerMessages;
using UexCorpDataRunner.Business.Settings;

namespace UexCorpDataRunner.Application.ViewModels;

public class MinimizedViewModel : ViewModelBase
{
    public readonly IMessenger _Messenger;
    public readonly ISettingsService _SettingsService;

    public double _Top = 0;
    public double Top
    {
        get => _Top;
        set => SetProperty(ref _Top, value);
    }

    public double _Left = 0;
    public double Left
    {
        get => _Left;
        set => SetProperty(ref _Left, value);
    }

    public double _TransformAngle = 0;
    public double TransformAngle
    {
        get => _TransformAngle;
        set => SetProperty(ref _TransformAngle, value);
    }

    public MinimizedViewModel(IMessenger messenger, ISettingsService settingsService)
    {
        _Messenger = messenger;
        _Messenger.Register<HideUserInterfaceMessage>(this, HideUserInterfaceNotified);
        _SettingsService = settingsService;
    }

    public ICommand ShowUserInterfaceCommand => new RelayCommand(ShowUserInterfaceCommandExecute);
    private void ShowUserInterfaceCommandExecute()
    {
        IsEnabled = false;
        _Messenger.Send(new ShowUserInterfaceMessage());
    }

    private void HideUserInterfaceNotified(HideUserInterfaceMessage notification)
    {
        IsEnabled = true;

        if(_SettingsService.Settings?.CollapseOrientation.Equals("Vertical") == true)
        {
            Top = 20;
            Left = -20;
            TransformAngle = 90;
        }
        if (_SettingsService.Settings?.CollapseOrientation.Equals("Horizontal") == true)
        {
            Top = 0;
            Left = 0;
            TransformAngle = 0;
        }
    }


}
