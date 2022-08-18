using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UexCorpDataRunner.Domain.Settings;
public class SettingsValues : ObservableObject
{
    private string theme = Globals.Settings.Dark;
    public string Theme { get => theme; set => SetProperty(ref theme, value); }

    private string collapseLocation = Globals.Settings.CenterRight;
    public string CollapseLocation { get => collapseLocation; set => SetProperty(ref collapseLocation, value); }

    private string collapseOrientation = Globals.Settings.Vertical;
    public string CollapseOrientation { get => collapseOrientation; set =>  SetProperty(ref collapseOrientation, value); }

    private string alwaysOnTop = Globals.Settings.Always;
    public string AlwaysOnTop { get => alwaysOnTop; set => SetProperty(ref alwaysOnTop, value); }

    private string showTemporaryCommodities = Globals.Settings.HideTemporary;
    public string ShowTemporaryCommodities { get => showTemporaryCommodities; set => SetProperty(ref showTemporaryCommodities, value); }

    private string userAccessCode = string.Empty;
    public string UserAccessCode { get => userAccessCode; set =>  SetProperty(ref userAccessCode, value); }

    private string currentVersion = string.Empty;
    public string CurrentVersion { get => currentVersion; set => SetProperty(ref currentVersion, value); }
}
