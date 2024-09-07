using CommunityToolkit.Mvvm.Messaging;
using UexCorpDataRunner.Application.Common;
using UexCorpDataRunner.Domain.Services;
using UexCorpDataRunner.Domain.Common;
using UexCorpDataRunner.Interface.MessengerMessages;

namespace UexCorpDataRunner.Interface;
public class MainViewModel : ViewModelBase
{
    private readonly IMessenger _Messenger;
    private readonly ISettingsService _SettingsService;

    private int _SelectedTabControlIndex = 1;
    public int SelectedTabControlIndex { get => _SelectedTabControlIndex; set => SetProperty(ref _SelectedTabControlIndex, value); }

    public MainViewModel(IMessenger messenger, ISettingsService settingsService)
    {
        _Messenger = messenger;
        _Messenger.Register<CloseSettingsInterfaceMessage>(this, CloseSettingsInterfaceMessageHandler);

        _SettingsService = settingsService;
        SetTabControlIndex();
    }

    public void CloseSettingsInterfaceMessageHandler(object sender, CloseSettingsInterfaceMessage notification)
    {
        if (_SettingsService?.Settings is null)
        {
            return;
        }

        SetTabControlIndex();
    }

    private void SetTabControlIndex()
    {
        if (_SettingsService?.Settings?.SelectedSiteVersion is null)
        {
            SelectedTabControlIndex = 0;
            return;
        }

        if (_SettingsService.Settings.SelectedSiteVersion == SiteVersion.Version1Value)
        {
            SelectedTabControlIndex = 0;
            return;
        }

        SelectedTabControlIndex = 1;
    }
}
