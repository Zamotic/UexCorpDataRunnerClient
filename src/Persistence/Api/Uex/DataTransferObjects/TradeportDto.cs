using System.Text.Json.Serialization;
using UexCorpDataRunner.Persistence.Api.Uex.DataTransferObjects.Converters;

namespace UexCorpDataRunner.Persistence.Api.Uex.DataTransferObjects;
public class TradeportDto
{
    [JsonPropertyName("system")]
    public string? System { get; set; }
    [JsonPropertyName("planet")]
    public string? Planet { get; set; }
    [JsonPropertyName("satellite")]
    public string? Satellite { get; set; }
    [JsonPropertyName("city")]
    public string? City { get; set; }
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    [JsonPropertyName("name_short")]
    public string? NameShort { get; set; }
    [JsonPropertyName("code")]
    public string? Code { get; set; }
    [JsonPropertyName("visible")]
    [JsonConverter(typeof(UexBooleanTypeJsonConverter))]
    public bool IsVisible { get; set; }
    [JsonPropertyName("armistice")]
    [JsonConverter(typeof(UexBooleanTypeJsonConverter))]
    public bool IsArmisticeZone { get; set; }
    [JsonPropertyName("trade")]
    [JsonConverter(typeof(UexBooleanTypeJsonConverter))]
    public bool HasTrade { get; set; }
    [JsonPropertyName("outlaw")]
    [JsonConverter(typeof(UexBooleanTypeJsonConverter))]
    public bool WelcomesOutlaws { get; set; }
    [JsonPropertyName("refinery")]
    [JsonConverter(typeof(UexBooleanTypeJsonConverter))]
    public bool HasRefinery { get; set; }
    [JsonPropertyName("shops")]
    [JsonConverter(typeof(UexBooleanTypeJsonConverter))]
    public bool HasShops { get; set; }
    [JsonPropertyName("restricted")]
    [JsonConverter(typeof(UexBooleanTypeJsonConverter))]
    public bool IsRestrictedArea { get; set; }
    [JsonPropertyName("minable")]
    [JsonConverter(typeof(UexBooleanTypeJsonConverter))]
    public bool HasMinables { get; set; }
    [JsonPropertyName("date_added")]
    [JsonConverter(typeof(UexDateTimeOffsetTypeJsonConverter))]
    public DateTimeOffset DateAdded { get; set; }
    [JsonPropertyName("date_modified")]
    [JsonConverter(typeof(UexDateTimeOffsetTypeJsonConverter))]
    public DateTimeOffset DateModified { get; set; }
    [JsonPropertyName("prices")]
    public Dictionary<string, TradeListingDto>? Prices { get; set; }
}