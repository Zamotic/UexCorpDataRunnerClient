using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UexCorpDataRunner.Domain.Settings;
public class SettingsValues
{
    public string Theme { get; set; } = "Dark";
    public string CollapseLocation { get; set; } = "CenterRight";
    public string CollapseOrientation { get; set; } = "Vertical";
    public string UserApiKey { get; set; } = string.Empty;
    public string UserAccessCode { get; set; } = string.Empty;
}
