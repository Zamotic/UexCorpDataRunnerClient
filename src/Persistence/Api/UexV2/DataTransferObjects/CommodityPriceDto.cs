using System.Text.Json.Serialization;
using UexCorpDataRunner.Persistence.Api.Converters;

namespace UexCorpDataRunner.Persistence.Api.UexV2.DataTransferObjects;
public class CommodityPriceDto : BaseDto
{
    [JsonPropertyName("id_commodity")]
    public int CommodityId { get; set; }

    [JsonPropertyName("id_star_system")]
    public int StarSystemId { get; set; }

    [JsonPropertyName("id_planet")]
    public int PlanetId { get; set; }

    [JsonPropertyName("id_moon")]
    public int MoonId { get; set; }

    [JsonPropertyName("id_city")]
    public int CityId { get; set; }

    [JsonPropertyName("id_outpost")]
    public int OutpostId { get; set; }

    [JsonPropertyName("id_terminal")]
    public int TerminalId { get; set; }

    [JsonPropertyName("price_buy")]
    [JsonConverter(typeof(UexFloatTypeJsonConverter))]
    public float BuyPrice { get; set; }

    [JsonPropertyName("price_buy_min")]
    [JsonConverter(typeof(UexFloatTypeJsonConverter))]
    public float BuyPriceMin { get; set; }

    [JsonPropertyName("price_buy_min_week")]
    [JsonConverter(typeof(UexFloatTypeJsonConverter))]
    public float BuyPriceMinWeek { get; set; }

    [JsonPropertyName("price_buy_min_month")]
    [JsonConverter(typeof(UexFloatTypeJsonConverter))]
    public float BuyPriceMinMonth { get; set; }

    [JsonPropertyName("price_buy_max")]
    [JsonConverter(typeof(UexFloatTypeJsonConverter))]
    public float BuyPriceMax { get; set; }

    [JsonPropertyName("price_buy_max_week")]
    [JsonConverter(typeof(UexFloatTypeJsonConverter))]
    public float BuyPriceMaxWeek { get; set; }

    [JsonPropertyName("price_buy_max_month")]
    [JsonConverter(typeof(UexFloatTypeJsonConverter))]
    public float BuyPriceMaxMonth { get; set; }

    [JsonPropertyName("price_buy_avg")]
    [JsonConverter(typeof(UexFloatTypeJsonConverter))]
    public float BuyPriceAvg { get; set; }

    [JsonPropertyName("price_buy_avg_week")]
    [JsonConverter(typeof(UexFloatTypeJsonConverter))]
    public float BuyPriceAvgWeek { get; set; }

    [JsonPropertyName("price_buy_avg_month")]
    [JsonConverter(typeof(UexFloatTypeJsonConverter))]
    public float BuyPriceAvgMonth { get; set; }

    [JsonPropertyName("price_sell")]
    [JsonConverter(typeof(UexFloatTypeJsonConverter))]
    public float SellPrice { get; set; }

    [JsonPropertyName("price_sell_min")]
    [JsonConverter(typeof(UexFloatTypeJsonConverter))]
    public float SellPriceMin { get; set; }

    [JsonPropertyName("price_sell_min_week")]
    [JsonConverter(typeof(UexFloatTypeJsonConverter))]
    public float SellPriceMinWeek { get; set; }

    [JsonPropertyName("price_sell_min_month")]
    [JsonConverter(typeof(UexFloatTypeJsonConverter))]
    public float SellPriceMinMonth { get; set; }

    [JsonPropertyName("price_sell_max")]
    [JsonConverter(typeof(UexFloatTypeJsonConverter))]
    public float SellPriceMax { get; set; }

    [JsonPropertyName("price_sell_max_week")]
    [JsonConverter(typeof(UexFloatTypeJsonConverter))]
    public float SellPriceMaxWeek { get; set; }

    [JsonPropertyName("price_sell_max_month")]
    [JsonConverter(typeof(UexFloatTypeJsonConverter))]
    public float SellPriceMaxMonth { get; set; }

    [JsonPropertyName("price_sell_avg")]
    [JsonConverter(typeof(UexFloatTypeJsonConverter))]
    public float SellPriceAvg { get; set; }

    [JsonPropertyName("price_sell_avg_week")]
    [JsonConverter(typeof(UexFloatTypeJsonConverter))]
    public float SellPriceAvgWeek { get; set; }

    [JsonPropertyName("price_sell_avg_month")]
    [JsonConverter(typeof(UexFloatTypeJsonConverter))]
    public float SellPriceAvgMonth { get; set; }

    [JsonPropertyName("scu_buy")]
    public int ScuBuy { get; set; }

    [JsonPropertyName("scu_buy_min")]
    public int ScuBuyMin { get; set; }

    [JsonPropertyName("scu_buy_min_week")]
    public int ScuBuyMinWeek { get; set; }

    [JsonPropertyName("scu_buy_min_month")]
    public int ScuBuyMinMonth { get; set; }

    [JsonPropertyName("scu_buy_max")]
    public int ScuBuyMax { get; set; }

    [JsonPropertyName("scu_buy_max_week")]
    public int ScuBuyMaxWeek { get; set; }

    [JsonPropertyName("scu_buy_max_month")]
    public int ScuBuyMaxMonth { get; set; }

    [JsonPropertyName("scu_buy_avg")]
    public int ScuBuyAvg { get; set; }

    [JsonPropertyName("scu_buy_avg_week")]
    public int ScuBuyAvgWeek { get; set; }

    [JsonPropertyName("scu_buy_avg_month")]
    public int ScuBuyAvgMonth { get; set; }

    [JsonPropertyName("scu_sell")]
    public int ScuSell { get; set; }

    [JsonPropertyName("scu_sell_min")]
    public int ScuSellMin { get; set; }

    [JsonPropertyName("scu_sell_min_week")]
    public int ScuSellMinWeek { get; set; }

    [JsonPropertyName("scu_sell_min_month")]
    public int ScuSellMinMonth { get; set; }

    [JsonPropertyName("scu_sell_max")]
    public int ScuSellMax { get; set; }

    [JsonPropertyName("scu_sell_max_week")]
    public int ScuSellMaxWeek { get; set; }

    [JsonPropertyName("scu_sell_max_month")]
    public int ScuSellMaxMonth { get; set; }

    [JsonPropertyName("scu_sell_avg")]
    public int ScuSellAvg { get; set; }

    [JsonPropertyName("scu_sell_avg_week")]
    public int ScuSellAvgWeek { get; set; }

    [JsonPropertyName("scu_sell_avg_month")]
    public int ScuSellAvgMonth { get; set; }

    [JsonPropertyName("game_version")]
    public string? GameVersion {  get; set; }

    [JsonPropertyName("commodity_name")]
    public string? CommodityName { get; set; }

    [JsonPropertyName("star_system_name")]
    public string? StarSystemName {  get; set; }

    [JsonPropertyName("planet_name")]
    public string? PlanetName {  get; set; }

    [JsonPropertyName("moon_name")]
    public string? MoonName {  get; set; }

    [JsonPropertyName("space_station_name")]
    public string? SpaceStationName {  get; set; }

    [JsonPropertyName("outpost_name")]
    public string? OutpostName {  get; set; }

    [JsonPropertyName("city_name")]
    public string? CityName {  get; set; }

}
