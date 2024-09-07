using System.Text.Json.Serialization;
using UexCorpDataRunner.Persistence.Api.Converters;

namespace UexCorpDataRunner.Persistence.Api.Uex.DataTransferObjects;
public class StarSystemDto
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
    [JsonConverter(typeof(UexDateTimeOffsetTypeJsonConverter))]
    public DateTimeOffset DateAdded { get; set; }
    [JsonPropertyName("date_modified")]
    [JsonConverter(typeof(UexDateTimeOffsetTypeJsonConverter))]
    public DateTimeOffset DateModified { get; set; }
}
