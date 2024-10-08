﻿using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using UexCorpDataRunner.Application.Common;
using UexCorpDataRunner.Domain.Common;
using UexCorpDataRunner.Domain.DataRunner;
using UexCorpDataRunner.Domain.Services;
using UexCorpDataRunner.Domain.Settings;
using UexCorpDataRunner.Interface.MessengerMessages;

namespace UexCorpDataRunner.Interface.Settings;

public class SettingsViewModel : ViewModelBase
{
    private readonly IMessenger _Messenger;
    private readonly ISettingsService _SettingsService;
    private readonly IUexDataServiceV2 _DataService;

    private bool _UserAccessCodeChanged = false;
    private bool _UserSecretKeyChanged = false;
    private bool _AlwaysOnTopChanged = false;
    private bool _ShowTemporaryCommodityChanged = false;
    private bool _SelectedGameVersionChanged = false;
    private bool _SelectedSiteVersionChanged = false;

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
        if(_SettingsValues is null)
        {
            return;
        }
        if (e.PropertyName.Equals(nameof(_SettingsValues.UserAccessCode)) == true)
        {
            _UserAccessCodeChanged = true;
        }
        if (e.PropertyName.Equals(nameof(_SettingsValues.AlwaysOnTop)) == true)
        {
            _AlwaysOnTopChanged = true;
        }
        if (e.PropertyName.Equals(nameof(_SettingsValues.ShowTemporaryCommodities)) == true)
        {
            _ShowTemporaryCommodityChanged = true;
        }
        if (e.PropertyName.Equals(nameof(_SettingsValues.Theme)) == true)
        {
            _Messenger.Send(new ThemeChangedMessage(_SettingsValues.Theme));
        }
        if (e.PropertyName.Equals(nameof(_SettingsValues.SelectedGameVersion)) == true)
        {
            _SelectedGameVersionChanged = true;
        }
        if (e.PropertyName.Equals(nameof(_SettingsValues.SelectedSiteVersion)) == true)
        {
            _SelectedSiteVersionChanged = true;
        }
        if (e.PropertyName.Equals(nameof(_SettingsValues.UserSecretKey)) == true)
        {
            _UserSecretKeyChanged = true;
        }
    }

    public List<string> ThemeList { get; } = new List<string>() 
    { 
        Domain.Globals.Settings.Light, 
        Domain.Globals.Settings.Dark 
    };

    public List<string> CollapseLocationList { get; } = new List<string>() 
    { 
        Domain.Globals.Settings.TopLeft, 
        Domain.Globals.Settings.TopCenter, 
        Domain.Globals.Settings.TopRight, 
        Domain.Globals.Settings.CenterLeft, 
        Domain.Globals.Settings.CenterRight,
        Domain.Globals.Settings.BottomLeft,
        Domain.Globals.Settings.BottomCenter,
        Domain.Globals.Settings.BottomRight
    };

    public List<string> CollapseOrientationList { get; } = new List<string>() 
    {
        Domain.Globals.Settings.Horizontal, 
        Domain.Globals.Settings.Vertical
    };

    public List<string> AlwaysOnTopList { get; } = new List<string>() 
    { 
        Domain.Globals.Settings.Always, 
        Domain.Globals.Settings.Minimized, 
        Domain.Globals.Settings.Never 
    };

    public List<string> ShowTemporaryCommodityList { get; } = new List<string>()
    {
        Domain.Globals.Settings.ShowAll,
        Domain.Globals.Settings.HideTemporary
    };

    public List<short> AutoCloseDelayList { get; } = new List<short>()
    {
        1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20
    };
    public List<string> GameVersionList { get; } = new List<string>()
    {
        GameVersion.LiveValue,
        GameVersion.PtuValue
    };
    public List<string> SiteVersionList { get; } = new List<string>()
    {
        //SiteVersion.Version1Value,
        SiteVersion.Version2Value
    };
    public List<string> SearchStyleList { get; } = new List<string>()
    {
        SearchStyle.DrillDownValue,
        SearchStyle.SearchFilterValue
    };

    public string? Version { get => Domain.Globals.Settings.Version; }

    //private const string KonamiCodeString = "UUDDLRLRBA";
    //public bool WasKonamiCodeActivated
    //{
    //    get
    //    {
    //        if (_SettingsService?.Settings?.KonamiCode.Equals(KonamiCodeString) == true)
    //        {
    //            return true;
    //        }
    //        return false;
    //    }
    //    set
    //    {
    //        if (_SettingsService?.Settings is null)
    //        {
    //            return;
    //        }
    //        if (value == true)
    //        {

    //            _SettingsService.Settings.KonamiCode = KonamiCodeString;
    //            OnPropertyChanged(nameof(WasKonamiCodeActivated));
    //            return;
    //        }
    //        _SettingsService.Settings.KonamiCode = string.Empty;
    //    }
    //}

    public SettingsViewModel(IMessenger messenger, ISettingsService settingsService, IUexDataServiceV2 dataService)
    {
        _Messenger = messenger;
        _Messenger.Register<ShowSettingsInterfaceMessage>(this, ShowSettingsInterfaceMessageHandler);

        _SettingsService = settingsService;
        _DataService = dataService;

        if(_SettingsService.Settings?.Theme != null)
        {
            _Messenger.Send(new ThemeChangedMessage(_SettingsService.Settings.Theme));
        }

        CheckIfVersionChangedSinceLastLoad();
    }

    private void CheckIfVersionChangedSinceLastLoad()
    {
        if (_SettingsService?.Settings is null)
        {
            return;
        }

        if (Domain.Globals.Settings.Version is null)
        {
            return;
        }

        if (_Messenger is null)
        {
            return;
        }

        if (_SettingsService.Settings?.CurrentVersion.Equals(Domain.Globals.Settings.Version) == true)
        {
            return;
        }

#pragma warning disable CS8602 // Dereference of a possibly null reference.
        _SettingsService.Settings.CurrentVersion = Domain.Globals.Settings.Version;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        _SettingsService.SaveSettings();
        _Messenger.Send(new ShowReleaseNotesMessage());
        
    }

    bool _IsViewModelLoaded = false;
    public IRelayCommand ViewModelLoadedCommand => new RelayCommand<object>(async (sender) => await ViewModelLoadedCommandExecuteAsync(sender));
    public async Task ViewModelLoadedCommandExecuteAsync(object? sender)
    {
        if(_IsViewModelLoaded is true)
        {
            return;
        }
        if (_SettingsService.Settings is null)
        {
            throw new Exception("Setting Service : Settings cannot be null");
        }

        _SettingsService.Settings.LoadedGameVersion = await _DataService.GetCurrentVersionAsync();

        _IsViewModelLoaded = true;
    }

    public IRelayCommand HyperlinkCommand => new RelayCommand<object>(HyperlinkCommandExecute);
    private void HyperlinkCommandExecute(object? linkUrl)
    {
        if(linkUrl is null)
        {
            return;
        }

        string? url = linkUrl as string;
        if (string.IsNullOrEmpty(url) == true)
        {
            return;
        }
        var psi = new System.Diagnostics.ProcessStartInfo
        {
            UseShellExecute = true,
            FileName = url
        };
        System.Diagnostics.Process.Start(psi);
    }

    public IRelayCommand CloseSettingsInterfaceCommand => new RelayCommand(CloseSettingsInterfaceCommandExecute);
    private void CloseSettingsInterfaceCommandExecute()
    {
        _SettingsService.SaveSettings();
        IsEnabled = false;
        _Messenger.Send(new CloseSettingsInterfaceMessage(_UserAccessCodeChanged, _AlwaysOnTopChanged, _ShowTemporaryCommodityChanged, _UserSecretKeyChanged));
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

        _UserAccessCodeChanged = false;
        _AlwaysOnTopChanged = false;
        _ShowTemporaryCommodityChanged = false;

        SettingsValues = _SettingsService.Settings;
    }
}
