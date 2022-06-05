using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UexCorpDataRunner.Application.Common;
using UexCorpDataRunner.Application.MessengerMessages;
using UexCorpDataRunner.Domain.Settings;
using UexCorpDataRunner.Domain.Services;

namespace UexCorpDataRunner.Interface.Settings;

public class SettingsViewModel : ViewModelBase
{
    private readonly IMessenger _Messenger;
    private readonly ISettingsService _SettingsService;

    private SettingsValues? _SettingsValues;
    public SettingsValues? SettingsValues
    {
        get => _SettingsValues;
        private set => SetProperty(ref _SettingsValues, value);
    }

    public List<string> ThemeList { get; } = new List<string>() { "Light", "Dark" };

    public List<string> CollapseLocationList { get; } = new List<string>() { "TopLeft", "TopCenter", "TopRight", "CenterLeft", "CenterRight", "BottomLeft", "BottomCenter", "BottomRight" };

    public List<string> CollapseOrientationList { get; } = new List<string>() { "Horizontal", "Vertical" };

    public SettingsViewModel(IMessenger messenger, ISettingsService settingsService)
    {
        _Messenger = messenger;
        _Messenger.Register<ShowSettingsInterfaceMessage>(this, ShowSettingsInterfaceMessageHandler);

        _SettingsService = settingsService;
    }

    public ICommand CloseSettingsInterfaceCommand => new RelayCommand(CloseSettingsInterfaceCommandExecute);
    private void CloseSettingsInterfaceCommandExecute()
    {
        _SettingsService.SaveSettings();
        IsEnabled = false;
        _Messenger.Send(new CloseSettingsInterfaceMessage());
    }

    //public void ShowSettingsInterfaceNotified(ShowSettingsInterfaceMessage notification)
    public void ShowSettingsInterfaceMessageHandler(object sender, ShowSettingsInterfaceMessage notification)
    {
        IsEnabled = true;
        if (_SettingsService is null)
        {
            return;
        }

        if (_SettingsService.Settings is null)
        {
            return;
        }

        SettingsValues = _SettingsService.Settings;
    }
}
