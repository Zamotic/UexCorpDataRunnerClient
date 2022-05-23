using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UexCorpDataRunner.Domain.Models.Settings;
public class SettingsValues
{
    public string Theme { get; set; } = "Light";
    public string CollapseLocation { get; set; } = "CenterRight";
    public string CollapseOrientation { get; set; } = "Vertical";
}
