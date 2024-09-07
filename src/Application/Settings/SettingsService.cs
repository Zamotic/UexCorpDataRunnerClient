using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UexCorpDataRunner.Domain.Common;
using UexCorpDataRunner.Domain.Services;
using UexCorpDataRunner.Domain.Settings;

namespace UexCorpDataRunner.Application.Settings;

public class SettingsService : ISettingsService
{
    private static SettingsValues? _Settings;

    private const string ApplicationDataLocalPath = "UEXCorpDataRunnerClient";
    private const string SettingsFileName = "settings.json";
    private readonly string SettingsFileFolderPath;
    private readonly string SettingsFilePath; 

    public SettingsValues? Settings => _Settings;

    public SettingsService()
    {
        var settingFolderPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);
        SettingsFileFolderPath = System.IO.Path.Combine(settingFolderPath, ApplicationDataLocalPath);
        SettingsFilePath = System.IO.Path.Combine(SettingsFileFolderPath, SettingsFileName);
    }

    public void LoadSettings()
    {
        if(System.IO.File.Exists(SettingsFilePath) == false)
        {
            CreateNewSettingsFile();
            return;
        }

        string fileContents = System.IO.File.ReadAllText(SettingsFilePath);
        if (string.IsNullOrEmpty(fileContents) == true)
        {
            CreateNewSettingsFile();
            return;
        }

        var loadedSettingsFile = System.Text.Json.JsonSerializer.Deserialize<SettingsFile>(fileContents);
        if (loadedSettingsFile is null)
        {
            CreateNewSettingsFile();
            return;
        }

        if(loadedSettingsFile.Settings!.SelectedSiteVersion == SiteVersion.Version1Value)
        {
            loadedSettingsFile.Settings.SelectedSiteVersion = SiteVersion.Version2Value;
        }
        loadedSettingsFile.Settings.KonamiCode = string.Empty;

        _Settings = loadedSettingsFile.Settings;
    }

    public void SaveSettings()
    {
        if (_Settings is null)
        {
            _Settings = new SettingsValues();
        }

        SettingsFile settingsFileToSave = new SettingsFile() { Settings = _Settings };
        string jsonSettingsString = System.Text.Json.JsonSerializer.Serialize<SettingsFile>(settingsFileToSave);

        System.IO.File.WriteAllText(SettingsFilePath, jsonSettingsString);
    }

    private void CreateNewSettingsFile()
    {
        _Settings = new SettingsValues();
        
        if (System.IO.Directory.Exists(SettingsFileFolderPath) == false)
        {
            System.IO.Directory.CreateDirectory(SettingsFileFolderPath);
        }

        SaveSettings();
    }
}
