using System.Text.Json.Serialization;
using UexCorpDataRunner.Persistence.Api.Converters;

namespace UexCorpDataRunner.Persistence.Api.UexV2.DataTransferObjects;
public class CommodityDto : ExtendedBaseDto
{
    [JsonPropertyName("is_parent")]
    public int ParentId { get; set; }

    [JsonPropertyName("slug")]
    public string? Slug { get; set; }

    [JsonPropertyName("kind")]
    public string? Kind { get; set; }

    [JsonPropertyName("price_buy")]
    [JsonConverter(typeof(UexFloatTypeJsonConverter))]
    public float BuyPrice { get; set; }

    [JsonPropertyName("price_sell")]
    [JsonConverter(typeof(UexFloatTypeJsonConverter))]
    public float SellPrice { get; set; }

    [JsonPropertyName("is_raw")]
    [JsonConverter(typeof(UexBooleanTypeJsonConverter))]
    public bool IsRaw { get; set; }

    [JsonPropertyName("is_harvestable")]
    [JsonConverter(typeof(UexBooleanTypeJsonConverter))]
    public bool IsHarvestable { get; set; }

    [JsonPropertyName("is_buyable")]
    [JsonConverter(typeof(UexBooleanTypeJsonConverter))]
    public bool IsBuyable { get; set; }

    [JsonPropertyName("is_sellable")]
    [JsonConverter(typeof(UexBooleanTypeJsonConverter))]
    public bool IsSellable { get; set; }

    [JsonPropertyName("is_temporary")]
    [JsonConverter(typeof(UexBooleanTypeJsonConverter))]
    public bool IsTemporary { get; set; }

    [JsonPropertyName("is_illegal")]
    [JsonConverter(typeof(UexBooleanTypeJsonConverter))]
    public bool IsIllegal { get; set; }

    new private bool IsDefault { get; set; }
}
