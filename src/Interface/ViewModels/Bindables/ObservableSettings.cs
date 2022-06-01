using UexCorpDataRunner.Application.Common;
using UexCorpDataRunner.Domain.Settings;
using CommunityToolkit.Mvvm.ComponentModel;

namespace UexCorpDataRunner.Application.ViewModels.Bindables;
public class ObservableSettings : ObservableObject
{
    private SettingsValues _Settings;

    public string Theme 
    { 
        get => _Settings.Theme; 
        set => SetProperty(_Settings.Theme, value, _Settings, (s, v) => s.Theme = v);
    }
    public string CollapseLocation 
    { 
        get => _Settings.CollapseLocation; 
        set => SetProperty(_Settings.CollapseLocation, value, _Settings, (s, v) => s.CollapseLocation = v);
    }
    public string CollapseOrientation
    {
        get => _Settings.CollapseOrientation;
        set => SetProperty(_Settings.CollapseOrientation, value, _Settings, (s, v) => s.CollapseOrientation = v);
    }
    public string UserApiKey
    {
        get => _Settings.UserApiKey;
        set => SetProperty(_Settings.UserApiKey, value, _Settings, (s, v) => s.UserApiKey = v);
    }

    private ObservableSettings() => _Settings = new SettingsValues();
    public ObservableSettings(SettingsValues settings) => this._Settings = settings;
}
