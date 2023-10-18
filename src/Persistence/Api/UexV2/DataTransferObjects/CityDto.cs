using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using UexCorpDataRunner.Persistence.Api.Converters;

namespace UexCorpDataRunner.Persistence.Api.UexV2.DataTransferObjects;
public class CityDto : LocationBaseDto
{
    [JsonPropertyName("id_star_system")]
    public int StarSystemId { get; set; }

    [JsonPropertyName("id_planet")]
    public int PlanetId { get; set; }

    [JsonPropertyName("id_moon")]
    public int MoonId { get; set; }
}
