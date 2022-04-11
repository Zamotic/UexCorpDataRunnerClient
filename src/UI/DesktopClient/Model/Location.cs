using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UexCorpDataRunner.DesktopClient.Model;
public  class Location
{
    public string? Name { get; protected internal set; }
    public IPlanetaryBody? PlanetaryBody { get; protected internal set; }
}
