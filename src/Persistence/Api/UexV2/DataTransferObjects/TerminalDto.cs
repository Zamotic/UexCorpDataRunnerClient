using System.Text.Json.Serialization;
using UexCorpDataRunner.Persistence.Api.Converters;

namespace UexCorpDataRunner.Persistence.Api.UexV2.DataTransferObjects;
public class TerminalDto : ExtendedBaseDto
{
    [JsonPropertyName("id_star_system")]
    public int StarSystemId { get; set; }

    [JsonPropertyName("star_system_name")]
    public string? StarSystemName { get; set; }

    [JsonPropertyName("id_planet")]
    public int PlanetId { get; set; }

    [JsonPropertyName("planet_name")]
    public string? PlanetName { get; set; }

    [JsonPropertyName("id_moon")]
    public int MoonId { get; set; }

    [JsonPropertyName("moon_name")]
    public string? MoonName { get; set; }

    [JsonPropertyName("id_space_station")]
    public int SpaceStationId { get; set; }

    [JsonPropertyName("space_station_name")]
    public string? SpaceStationName { get; set; }

    [JsonPropertyName("id_outpost")]
    public int OutpostId { get; set; }

    [JsonPropertyName("outpost_name")]
    public string? OutpostName { get; set; }

    [JsonPropertyName("id_city")]
    public int CityId { get; set; }

    [JsonPropertyName("city_name")]
    public string? CityName { get; set; }

    [JsonPropertyName("id_faction")]
    public int FactionId { get; set; }

    [JsonPropertyName("nickname")]
    public string? Nickname { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("screenshot")]
    public string? Screenshot { get; set; }

    [JsonPropertyName("is_affinity_influenceable")]
    [JsonConverter(typeof(UexBooleanTypeJsonConverter))]
    public bool IsAffinityInfluenceable { get; set; }

    [JsonPropertyName("has_container_transfer")]
    [JsonConverter(typeof(UexBooleanTypeJsonConverter))]
    public bool HasContainerTransfer { get; set; }
}
