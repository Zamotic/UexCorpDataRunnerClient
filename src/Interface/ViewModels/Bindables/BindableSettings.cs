using UexCorpDataRunner.Application.Common;
using UexCorpDataRunner.Domain.Settings;

namespace UexCorpDataRunner.Application.ViewModels.Bindables;
public class BindableSettings : BindableBase
{
    private SettingsValues _Settings;

    public string Theme 
    { 
        get => _Settings.Theme; 
        set => SetProperty((value) => { _Settings.Theme = value; }, value); 
    }
    public string CollapseLocation 
    { 
        get => _Settings.CollapseLocation; 
        set => SetProperty((value) => { _Settings.CollapseLocation = value; }, value);
    }
    public string CollapseOrientation
    {
        get => _Settings.CollapseOrientation;
        set => SetProperty((value) => { _Settings.CollapseOrientation = value; }, value);
    }
    public string UserApiKey
    {
        get => _Settings.UserApiKey;
        set => SetProperty((value) => { _Settings.UserApiKey = value; }, value);
    }

    private BindableSettings() { _Settings = new SettingsValues(); }
    public BindableSettings(SettingsValues settings)
    {
        _Settings = settings;
    }
}
