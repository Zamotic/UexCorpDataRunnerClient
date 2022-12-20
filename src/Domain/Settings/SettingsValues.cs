using CommunityToolkit.Mvvm.ComponentModel;
using UexCorpDataRunner.Domain.DataRunner;

namespace UexCorpDataRunner.Domain.Settings;
public class SettingsValues : ObservableObject
{
    private string _Theme = Globals.Settings.Dark;
    public string Theme { get => _Theme; set => SetProperty(ref _Theme, value); }

    private string _CollapseLocation = Globals.Settings.CenterRight;
    public string CollapseLocation { get => _CollapseLocation; set => SetProperty(ref _CollapseLocation, value); }

    private string _CollapseOrientation = Globals.Settings.Vertical;
    public string CollapseOrientation { get => _CollapseOrientation; set =>  SetProperty(ref _CollapseOrientation, value); }

    private string _AlwaysOnTop = Globals.Settings.Always;
    public string AlwaysOnTop { get => _AlwaysOnTop; set => SetProperty(ref _AlwaysOnTop, value); }

    private string _ShowTemporaryCommodities = Globals.Settings.HideTemporary;
    public string ShowTemporaryCommodities { get => _ShowTemporaryCommodities; set => SetProperty(ref _ShowTemporaryCommodities, value); }

    private string _UserAccessCode = string.Empty;
    public string UserAccessCode { get => _UserAccessCode; set =>  SetProperty(ref _UserAccessCode, value); }

    private string _CurrentVersion = string.Empty;
    public string CurrentVersion { get => _CurrentVersion; set => SetProperty(ref _CurrentVersion, value); }

    private bool _AutoCloseSummaryWindow = false;
    public bool AutoCloseSummaryWindow { get => _AutoCloseSummaryWindow; set => SetProperty(ref _AutoCloseSummaryWindow, value); }

    private short _AutoCloseSummaryTime = 5;
    public short AutoCloseSummaryTime { get => _AutoCloseSummaryTime; set => SetProperty(ref _AutoCloseSummaryTime, value); }

    private string _SelectedGameVersion = GameVersion.LiveValue;
    public string SelectedGameVersion { get => _SelectedGameVersion; set => SetProperty(ref _SelectedGameVersion, value); }
    public GameVersion? LoadedGameVersion { get; set; }
}
