using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UexCorpDataRunner.Application.Common;
using UexCorpDataRunner.Interface.MessengerMessages;
using UexCorpDataRunner.Domain.Settings;
using UexCorpDataRunner.Domain.Services;

namespace UexCorpDataRunner.Interface.Settings;

public class SettingsViewModel : ViewModelBase
{
    private readonly IMessenger _Messenger;
    private readonly ISettingsService _SettingsService;

    private bool _UserDataChanged = false;

    private SettingsValues? _SettingsValues;
    public SettingsValues? SettingsValues
    {
        get => _SettingsValues;
        private set
        {
            if(_SettingsValues != null)
            {
                _SettingsValues.PropertyChanged -= _SettingsValues_PropertyChanged;
            }
            SetProperty(ref _SettingsValues, value);
            if (_SettingsValues != null)
            {
                _SettingsValues.PropertyChanged += _SettingsValues_PropertyChanged;
            }
        }
    }

    private void _SettingsValues_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if(e.PropertyName is null)
        {
            return;
        }
        if(_UserDataChanged = true)
        {
            return;
        }
        if (e.PropertyName.Equals(nameof(_SettingsValues.UserApiKey)) == true)
        {
            _UserDataChanged = true;
        }
        if (e.PropertyName.Equals(nameof(_SettingsValues.UserAccessCode)) == true)
        {
            _UserDataChanged = true;
        }
    }

    public List<string> ThemeList { get; } = new List<string>() { /*"Light",*/ "Dark" };

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
        _Messenger.Send(new CloseSettingsInterfaceMessage(_UserDataChanged));
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

        _UserDataChanged = false;
        SettingsValues = _SettingsService.Settings;
    }
}
