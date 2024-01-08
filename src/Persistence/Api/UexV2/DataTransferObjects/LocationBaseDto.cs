using System.Text.Json.Serialization;
using UexCorpDataRunner.Persistence.Api.Converters;

namespace UexCorpDataRunner.Persistence.Api.UexV2.DataTransferObjects;
public class LocationBaseDto : ExtendedBaseDto
{
    [JsonPropertyName("id_star_system")]
    public int StarSystemId { get; set; }

    [JsonPropertyName("id_faction")]
    public int FactionId { get; set; }

    [JsonPropertyName("nickname")]
    public string? Nickname{ get; set; }

    //[JsonPropertyName("is_monitored")]
    //[JsonConverter(typeof(UexBooleanTypeJsonConverter))]
    //public bool IsMonitored { get; set; }

    //[JsonPropertyName("is_armistice")]
    //[JsonConverter(typeof(UexBooleanTypeJsonConverter))]
    //public bool IsArmistice { get; set; }

    //[JsonPropertyName("is_landable")]
    //[JsonConverter(typeof(UexBooleanTypeJsonConverter))]
    //public bool IsLandable { get; set; }

    //[JsonPropertyName("is_decomissioned")]
    //[JsonConverter(typeof(UexBooleanTypeJsonConverter))]
    //public bool IsDecomissioned { get; set; }

    //[JsonPropertyName("has_quantum_marker")]
    //[JsonConverter(typeof(UexBooleanTypeJsonConverter))]
    //public bool HasQuantumMarker { get; set; }

    [JsonPropertyName("has_trade_terminal")]
    [JsonConverter(typeof(UexBooleanTypeJsonConverter))]
    public bool HasTradeTerminal { get; set; }

    //[JsonPropertyName("has_habitation")]
    //[JsonConverter(typeof(UexBooleanTypeJsonConverter))]
    //public bool HasHabitation { get; set; }

    //[JsonPropertyName("has_refinery")]
    //[JsonConverter(typeof(UexBooleanTypeJsonConverter))]
    //public bool HasRefinery { get; set; }

    //[JsonPropertyName("has_cargo_center")]
    //[JsonConverter(typeof(UexBooleanTypeJsonConverter))]
    //public bool HasCargoCenter { get; set; }

    //[JsonPropertyName("has_clinic")]
    //[JsonConverter(typeof(UexBooleanTypeJsonConverter))]
    //public bool HasClinic { get; set; }

    //[JsonPropertyName("has_food")]
    //[JsonConverter(typeof(UexBooleanTypeJsonConverter))]
    //public bool HasFood { get; set; }

    [JsonPropertyName("has_shops")]
    [JsonConverter(typeof(UexBooleanTypeJsonConverter))]
    public bool HasShops { get; set; }

    //[JsonPropertyName("has_refuel")]
    //[JsonConverter(typeof(UexBooleanTypeJsonConverter))]
    //public bool HasRefuel { get; set; }

    //[JsonPropertyName("has_repair")]
    //[JsonConverter(typeof(UexBooleanTypeJsonConverter))]
    //public bool HasRepair { get; set; }

    //[JsonPropertyName("has_gravity")]
    //[JsonConverter(typeof(UexBooleanTypeJsonConverter))]
    //public bool HasGravity { get; set; }
}
