using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UexCorpDataRunner.Domain.DataRunnerV2;
public class SpaceStation : AvailableNameableBaseModel
{
    public int StarSystemId { get; set; }
    public int PlanetId { get; set; }
    public int MoonId { get; set; }
    public int CityId { get; set; }
    public string? Nickname { get; set; }
}
