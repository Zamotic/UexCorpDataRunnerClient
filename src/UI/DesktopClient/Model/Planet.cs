using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UexCorpDataRunner.DesktopClient.Model;
public class Planet : IPlanetaryBody
{
    public string? Name { get; set; }
    public System? System { get; set; }
}
