using UexCorpDataRunner.Application.Common;
using UexCorpDataRunner.Domain.Models.Settings;

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

    private BindableSettings() { _Settings = new SettingsValues(); }
    public BindableSettings(SettingsValues settings)
    {
        _Settings = settings;
    }
}
