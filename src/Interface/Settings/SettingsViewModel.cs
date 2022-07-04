﻿using CommunityToolkit.Mvvm.Input;
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

    private bool _UserAccessCodeChanged = false;
    private bool _AlwaysOnTopChanged = false;
    private bool _ShowTemporaryCommodityChanged = false;

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
    }

    public List<string> ThemeList { get; } = new List<string>() 
    { 
        /*Domain.Globals.Settings.Light,*/ 
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

    public string? Version { get => Domain.Globals.Settings.Version; }

    public SettingsViewModel(IMessenger messenger, ISettingsService settingsService)
    {
        _Messenger = messenger;
        _Messenger.Register<ShowSettingsInterfaceMessage>(this, ShowSettingsInterfaceMessageHandler);

        _SettingsService = settingsService;
    }

    public ICommand HyperlinkCommand => new RelayCommand<object>(HyperlinkCommandExecute);
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

    public ICommand CloseSettingsInterfaceCommand => new RelayCommand(CloseSettingsInterfaceCommandExecute);
    private void CloseSettingsInterfaceCommandExecute()
    {
        _SettingsService.SaveSettings();
        IsEnabled = false;
        _Messenger.Send(new CloseSettingsInterfaceMessage(_UserAccessCodeChanged, _AlwaysOnTopChanged, _ShowTemporaryCommodityChanged));
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
