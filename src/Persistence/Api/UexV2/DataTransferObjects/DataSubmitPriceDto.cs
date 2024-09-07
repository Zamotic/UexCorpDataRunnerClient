using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace UexCorpDataRunner.Persistence.Api.UexV2.DataTransferObjects;
public class DataSubmitPriceDto
{
    [JsonPropertyName("id_commodity")]
    public int CommodityId { get; set; }

    [JsonPropertyName("price_sell")]
    public float SellPrice { get; set; }

    [JsonPropertyName("scu_sell")]
    public int SellScu { get; set; }

    [JsonPropertyName("status_sell")]
    public int SellStatus { get; set; }

    [JsonPropertyName("price_buy")]
    public float BuyPrice { get; set; }

    [JsonPropertyName("scu_buy")]
    public int BuyScu { get; set; }

    [JsonPropertyName("status_buy")]
    public int BuyStatus { get; set; }
}
