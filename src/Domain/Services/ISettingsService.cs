using UexCorpDataRunner.Domain.Settings;

namespace UexCorpDataRunner.Domain.Services;
public interface ISettingsService
{
    SettingsValues? Settings { get; }

    void LoadSettings();
    void SaveSettings();
}
