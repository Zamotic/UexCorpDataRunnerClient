using System.Text.Json.Serialization;
using UexCorpDataRunner.Persistence.Api.Uex.DataTransferObjects.Converters;

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
    [JsonPropertyName("date_added")]
    [JsonConverter(typeof(UexDateTimeOffsetTypeJsonConverter))]
    public DateTimeOffset DateAdded { get; set; }
    [JsonPropertyName("date_modified")]
    [JsonConverter(typeof(UexDateTimeOffsetTypeJsonConverter))]
    public DateTimeOffset DateModified { get; set; }
}