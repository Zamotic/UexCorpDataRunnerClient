using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using UexCorpDataRunner.Persistence.Api.Converters;

namespace UexCorpDataRunner.Persistence.Api.UexV2.DataTransferObjects;
public class PlanetDto : ExtendedBaseDto
{
    [JsonPropertyName("id_star_system")]
    public int StarSystemId { get; set; }

    [JsonPropertyName("name_origin")]
    public string? NameOrigin { get; set; }

    [JsonPropertyName("is_lagrange")]
    [JsonConverter(typeof(UexBooleanTypeJsonConverter))]
    public bool IsLagrange { get; set; }
}
