using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UexCorpDataRunner.DesktopClient.Core;
using UexCorpDataRunner.DesktopClient.Notifications;
using UexCorpDataRunner.DesktopClient.Settings;
using UexCorpDataRunner.DesktopClient.ViewModels.Settings;

namespace UexCorpDataRunner.DesktopClient.ViewModels;

public class SettingsViewModel : ViewModelBase
{
    private readonly IMessenger _Messenger;
    private readonly ISettingsService _SettingsService;

    private BindableSettings? _BindableSettings;
    public BindableSettings? BindableSettings 
    { 
        get => _BindableSettings; 
        private set => SetProperty(ref _BindableSettings, value); 
    }

    public List<string> ThemeList { get; } = new List<string>() { "Light", "Dark" };

    public List<string> CollapseLocationList { get; } = new List<string>() { "TopLeft", "TopCenter", "TopRight", "CenterLeft", "CenterRight", "BottomLeft", "BottomCenter", "BottomRight" };

    public List<string> CollapseOrientationList { get; } = new List<string>() { "Horizontal", "Vertical" };

    public SettingsViewModel(IMessenger messenger, ISettingsService settingsService)
    {
        _Messenger = messenger;
        _Messenger.Register<ShowSettingsInterfaceNotification>(this, ShowSettingsInterfaceNotified);

        _SettingsService = settingsService;
    }

    public ICommand CloseSettingsInterfaceCommand => new RelayCommand(CloseSettingsInterfaceCommandExecute);
    private void CloseSettingsInterfaceCommandExecute()
    {
        _SettingsService.SaveSettings();
        IsEnabled = false;
        _Messenger.Send(new CloseSettingsInterfaceNotification());
    }

    public void ShowSettingsInterfaceNotified(ShowSettingsInterfaceNotification notification)
    {
        IsEnabled = true;
        if(_SettingsService is null)
        {
            return;
        }

        if(_SettingsService.Settings is null)
        {
            return;
        }

        BindableSettings = new BindableSettings(_SettingsService.Settings);
    }
}
