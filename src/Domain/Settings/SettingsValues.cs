using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UexCorpDataRunner.Domain.Settings;
public class SettingsValues : ObservableObject
{
    private string theme = "Dark";
    public string Theme { get => theme; set => SetProperty(ref theme, value); }
    private string collapseLocation = "CenterRight";
    public string CollapseLocation { get => collapseLocation; set => SetProperty(ref collapseLocation, value); }
    private string collapseOrientation = "Vertical";
    public string CollapseOrientation { get => collapseOrientation; set =>  SetProperty(ref collapseOrientation, value); }
    private string userApiKey = string.Empty;
    public string UserApiKey { get => userApiKey; set =>  SetProperty(ref userApiKey, value); }
    private string userAccessCode = string.Empty;
    public string UserAccessCode { get => userAccessCode; set =>  SetProperty(ref userAccessCode, value); }
}
