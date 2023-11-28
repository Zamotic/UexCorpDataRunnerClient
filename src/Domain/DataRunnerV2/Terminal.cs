using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace UexCorpDataRunner.Domain.DataRunnerV2;
public class Terminal
{
    public int StarSystemId { get; set; }

    public string? StarSystemName { get; set; }

    public int PlanetId { get; set; }

    public string? PlanetName { get; set; }

    public int MoonId { get; set; }

    public string? MoonName { get; set; }

    public int SpaceStationId { get; set; }

    public string? SpaceStationName { get; set; }

    public int OutpostId { get; set; }

    public string? OutpostName { get; set; }

    public int CityId { get; set; }

    public string? CityName { get; set; }

    public string? Name { get; set; }

    public string? Nickname { get; set; }

    public string? Code { get; set; }

    public string? Type { get; set; }
}
