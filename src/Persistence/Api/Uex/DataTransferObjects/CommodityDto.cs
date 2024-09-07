using System.Text.Json.Serialization;
using UexCorpDataRunner.Persistence.Api.Converters;

namespace UexCorpDataRunner.Persistence.Api.Uex.DataTransferObjects;
public class CommodityDto
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    [JsonPropertyName("code")]
    public string? Code { get; set; }
    [JsonPropertyName("kind")]
    public string? Kind { get; set; }
    [JsonPropertyName("trade_price_buy")]
    public decimal? BuyPrice { get; set; }
    [JsonPropertyName("trade_price_sell")]
    public decimal? SellPrice { get; set; }
    [JsonPropertyName("tradable")]
    [JsonConverter(typeof(UexBooleanTypeJsonConverter))]
    public bool Tradeable { get; set; }
    [JsonPropertyName("buyable")]
    [JsonConverter(typeof(UexBooleanTypeJsonConverter))]
    public bool Buyable { get; set; }
    [JsonPropertyName("sellable")]
    [JsonConverter(typeof(UexBooleanTypeJsonConverter))]
    public bool Sellable { get; set; }
    //[JsonPropertyName("minable")]
    //[JsonConverter(typeof(UexBooleanTypeJsonConverter))]
    //public bool Minable { get; set; }
    //[JsonPropertyName("harvestable")]
    //[JsonConverter(typeof(UexBooleanTypeJsonConverter))]
    //public bool Harvestable { get; set; }
    [JsonPropertyName("temporary")]
    [JsonConverter(typeof(UexBooleanTypeJsonConverter))]
    public bool Temporary { get; set; }
    [JsonPropertyName("restricted")]
    [JsonConverter(typeof(UexBooleanTypeJsonConverter))]
    public bool Restricted { get; set; }
    //[JsonPropertyName("raw")]
    //[JsonConverter(typeof(UexBooleanTypeJsonConverter))]
    //public bool Raw { get; set; }
    [JsonPropertyName("illegal")]
    [JsonConverter(typeof(UexBooleanTypeJsonConverter))]
    public bool Illegal { get; set; }
    [JsonPropertyName("available")]
    [JsonConverter(typeof(UexBooleanTypeJsonConverter))]
    public bool Available { get; set; }
    [JsonPropertyName("date_added")]
    [JsonConverter(typeof(UexDateTimeOffsetTypeJsonConverter))]
    public DateTimeOffset DateAdded { get; set; }
    [JsonPropertyName("date_modified")]
    [JsonConverter(typeof(UexDateTimeOffsetTypeJsonConverter))]
    public DateTimeOffset DateModified { get; set; }
}