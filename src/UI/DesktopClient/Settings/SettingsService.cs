using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UexCorpDataRunner.DesktopClient.Settings;

public class SettingsService : ISettingsService
{
    private static Model.SettingsValues? _Settings;

    private const string ApplicationDataLocalPath = "UEXCorpDataRunnerClient";
    private const string SettingsFileName = "settings.json";
    private readonly string SettingsFileFolderPath;
    private readonly string SettingsFilePath;

    public Model.SettingsValues? Settings => _Settings;

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

        var loadedSettingsFile = System.Text.Json.JsonSerializer.Deserialize<Model.SettingsFile>(fileContents);
        if (loadedSettingsFile is null)
        {
            CreateNewSettingsFile();
            return;
        }

        _Settings = loadedSettingsFile.Settings;
    }

    public void SaveSettings()
    {
        if (_Settings is null)
        {
            _Settings = new Model.SettingsValues();
        }

        Model.SettingsFile settingsFileToSave = new Model.SettingsFile() { Settings = _Settings };
        string jsonSettingsString = System.Text.Json.JsonSerializer.Serialize<Model.SettingsFile>(settingsFileToSave);

        System.IO.File.WriteAllText(SettingsFilePath, jsonSettingsString);
    }

    private void CreateNewSettingsFile()
    {
        _Settings = new Model.SettingsValues();
        
        if (System.IO.Directory.Exists(SettingsFileFolderPath) == false)
        {
            System.IO.Directory.CreateDirectory(SettingsFileFolderPath);
        }

        SaveSettings();
    }
}
