using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using UexCorpDataRunner.Persistence.Api.Converters;

namespace UexCorpDataRunner.Persistence.Api.Uex.DataTransferObjects;
public class TradeListingDto
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    [JsonPropertyName("kind")]
    public string? Kind { get; set; }
    [JsonPropertyName("operation")]
    public string? Operation { get; set; }
    [JsonPropertyName("price_buy")]
    public decimal PriceBuy { get; set; }
    [JsonPropertyName("price_sell")]
    public decimal PriceSell { get; set; }
    [JsonPropertyName("date_update")]
    [JsonConverter(typeof(UexDateTimeOffsetTypeJsonConverter))]
    public DateTimeOffset DateUpdate { get; set; }
    [JsonPropertyName("is_updated")]
    public bool IsUpdated { get; set; }
}
