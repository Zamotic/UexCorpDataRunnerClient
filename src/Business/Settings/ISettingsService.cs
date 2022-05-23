using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UexCorpDataRunner.Domain.Models.Settings;

namespace UexCorpDataRunner.Business.Settings;
public interface ISettingsService
{
    SettingsValues? Settings { get; }

    void LoadSettings();
    void SaveSettings();
}
