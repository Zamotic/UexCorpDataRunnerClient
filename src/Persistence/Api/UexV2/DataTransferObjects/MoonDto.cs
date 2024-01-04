using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace UexCorpDataRunner.Persistence.Api.UexV2.DataTransferObjects;
public class MoonDto : ExtendedBaseDto
{
    [JsonPropertyName("id_planet")]
    public int PlanetId { get; set; }

    [JsonPropertyName("name_origin")]
    public string? NameOrigin { get; set; }
}
