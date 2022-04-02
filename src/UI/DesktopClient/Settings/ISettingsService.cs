using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UexCorpDataRunner.DesktopClient.Settings;
public interface ISettingsService
{
    Model.SettingsValues? Settings { get; }

    void LoadSettings();
    void SaveSettings();
}
