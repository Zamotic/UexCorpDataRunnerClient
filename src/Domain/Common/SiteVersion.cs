using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UexCorpDataRunner.Domain.Common;
public class SiteVersion
{
    public const string Version1Value = "UEX 1.0";
    public const string Version2Value = "UEX 2.0";

    public string Version1 { get; set; } = string.Empty;
    public string Version2 { get; set; } = string.Empty;
}
