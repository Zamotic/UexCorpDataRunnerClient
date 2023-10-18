using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using UexCorpDataRunner.Persistence.Api.Converters;

namespace UexCorpDataRunner.Persistence.Api.Uex.DataTransferObjects;
public class PriceReportDto
{
    [JsonPropertyName("commodity")]
    public string CommodityCode { get; set; } = string.Empty;
    [JsonPropertyName("tradeport")]
    public string TradeportCode { get; set; } = string.Empty;
    [JsonPropertyName("operation")]
    public string Operation { get; set; } = string.Empty;
    [JsonPropertyName("price")]
    public string Price { get; set; } = string.Empty;
    [JsonPropertyName("access_code")]
    public string AccessCode { get; set; } = string.Empty;
    [JsonPropertyName("version")]
    public string Version { get; set; } = string.Empty;
    [JsonPropertyName("confirm")]
    [JsonConverter(typeof(UexBooleanTypeJsonConverter))]
    public bool Confirm { get; set; } = false;
    [JsonPropertyName("production")]
    public char Production { get; set; } = 'Y';
    [JsonPropertyName("scu")]
    public string SCU { get; set; } = string.Empty;
}
