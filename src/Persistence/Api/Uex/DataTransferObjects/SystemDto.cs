using System.Text.Json.Serialization;
using UexCorpDataRunner.Persistence.Api.Uex.DataTransferObjects.Converters;

namespace UexCorpDataRunner.Persistence.Api.Uex.DataTransferObjects;
public class SystemDto
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    [JsonPropertyName("code")]
    public string? Code { get; set; }
    [JsonPropertyName("available")]
    [JsonConverter(typeof(UexBooleanTypeJsonConverter))]
    public bool IsAvailable { get; set; }
    [JsonPropertyName("default")]
    [JsonConverter(typeof(UexBooleanTypeJsonConverter))]
    public bool IsDefault { get; set; }
    [JsonPropertyName("date_added")]
    [JsonConverter(typeof(UexDateTimeTypeJsonConverter))]
    public DateTime DateAdded { get; set; }
    [JsonPropertyName("date_modified")]
    [JsonConverter(typeof(UexDateTimeTypeJsonConverter))]
    public DateTime DateModified { get; set; }
}
