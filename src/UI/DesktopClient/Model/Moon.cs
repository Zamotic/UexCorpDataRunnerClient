using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UexCorpDataRunner.DesktopClient.Model;
public class Moon : IPlanetaryBody
{
    public string? Name { get; private set; }
    public Planet? Planet { get; private set; }
}
