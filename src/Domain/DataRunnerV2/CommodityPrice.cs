using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace UexCorpDataRunner.Domain.DataRunnerV2;
public class CommodityPrice : BaseModel
{
    public int CommodityId { get; set; }

    public int StarSystemId { get; set; }

    public int PlanetId { get; set; }

    public int MoonId { get; set; }

    public int CityId { get; set; }

    public int OutpostId { get; set; }

    public int TerminalId { get; set; }

    public float PriceBuy { get; set; }

    public float PriceBuyMin { get; set; }

    public float PriceBuyMinWeek { get; set; }

    public float PriceBuyMinMonth { get; set; }

    public float PriceBuyMax { get; set; }

    public float PriceBuyMaxWeek { get; set; }

    public float PriceBuyMaxMonth { get; set; }

    public float PriceBuyAvg { get; set; }

    public float PriceBuyAvgWeek { get; set; }

    public float PriceBuyAvgMonth { get; set; }

    public float PriceSell { get; set; }

    public float PriceSellMin { get; set; }

    public float PriceSellMinWeek { get; set; }

    public float PriceSellMinMonth { get; set; }

    public float PriceSellMax { get; set; }

    public float PriceSellMaxWeek { get; set; }

    public float PriceSellMaxMonth { get; set; }

    public float PriceSellAvg { get; set; }

    public float PriceSellAvgWeek { get; set; }

    public float PriceSellAvgMonth { get; set; }

    public int ScuBuy { get; set; }

    public int ScuBuyMin { get; set; }

    public int ScuBuyMinWeek { get; set; }

    public int ScuBuyMinMonth { get; set; }

    public int ScuBuyMax { get; set; }

    public int ScuBuyMaxWeek { get; set; }

    public int ScuBuyMaxMonth { get; set; }

    public int ScuBuyAvg { get; set; }

    public int ScuBuyAvgWeek { get; set; }

    public int ScuBuyAvgMonth { get; set; }

    public int ScuSell { get; set; }

    public int ScuSellMin { get; set; }

    public int ScuSellMinWeek { get; set; }

    public int ScuSellMinMonth { get; set; }

    public int ScuSellMax { get; set; }

    public int ScuSellMaxWeek { get; set; }

    public int ScuSellMaxMonth { get; set; }

    public int ScuSellAvg { get; set; }

    public int ScuSellAvgWeek { get; set; }

    public int ScuSellAvgMonth { get; set; }

    public string? GameVersion { get; set; }

    public string? CommodityName { get; set; }

    public string? StarSystemName { get; set; }

    public string? PlanetName { get; set; }

    public string? MoonName { get; set; }

    public string? SpaceStationName { get; set; }

    public string? OutpostName { get; set; }

    public string? CityName { get; set; }
}
