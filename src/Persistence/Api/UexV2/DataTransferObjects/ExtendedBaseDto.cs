using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using UexCorpDataRunner.Persistence.Api.Converters;

namespace UexCorpDataRunner.Persistence.Api.UexV2.DataTransferObjects;
public class ExtendedBaseDto : BaseDto
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("code")]
    public string? Code { get; set; }

    [JsonPropertyName("is_available")]
    [JsonConverter(typeof(UexBooleanTypeJsonConverter))]
    public bool IsAvailable { get; set; }

    [JsonPropertyName("is_visible")]
    [JsonConverter(typeof(UexBooleanTypeJsonConverter))]
    public bool IsVisible { get; set; }

    [JsonPropertyName("default")]
    [JsonConverter(typeof(UexBooleanTypeJsonConverter))]
    public bool IsDefault { get; set; }
}
