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

    public decimal BuyPrice { get; set; }

    public decimal BuyPriceMin { get; set; }

    //public decimal BuyPriceMinWeek { get; set; }

    //public decimal BuyPriceMinMonth { get; set; }

    public decimal BuyPriceMax { get; set; }

    //public decimal BuyPriceMaxWeek { get; set; }

    //public decimal BuyPriceMaxMonth { get; set; }

    public decimal BuyPriceAvg { get; set; }

    //public decimal BuyPriceAvgWeek { get; set; }

    //public decimal BuyPriceAvgMonth { get; set; }

    public decimal SellPrice { get; set; }

    public decimal SellPriceMin { get; set; }

    //public decimal SellPriceMinWeek { get; set; }

    //public decimal SellPriceMinMonth { get; set; }

    public decimal SellPriceMax { get; set; }

    //public decimal SellPriceMaxWeek { get; set; }

    //public decimal SellPriceMaxMonth { get; set; }

    public decimal SellPriceAvg { get; set; }

    //public decimal SellPriceAvgWeek { get; set; }

    //public decimal SellPriceAvgMonth { get; set; }

    public int ScuBuy { get; set; }

    public int ScuBuyMin { get; set; }

    //public int ScuBuyMinWeek { get; set; }

    //public int ScuBuyMinMonth { get; set; }

    public int ScuBuyMax { get; set; }

    //public int ScuBuyMaxWeek { get; set; }

    //public int ScuBuyMaxMonth { get; set; }

    public int ScuBuyAvg { get; set; }

    //public int ScuBuyAvgWeek { get; set; }

    //public int ScuBuyAvgMonth { get; set; }

    public int ScuSell { get; set; }

    public int ScuSellMin { get; set; }

    //public int ScuSellMinWeek { get; set; }

    //public int ScuSellMinMonth { get; set; }

    public int ScuSellMax { get; set; }

    //public int ScuSellMaxWeek { get; set; }

    //public int ScuSellMaxMonth { get; set; }

    public int ScuSellAvg { get; set; }

    //public int ScuSellAvgWeek { get; set; }

    //public int ScuSellAvgMonth { get; set; }

    public string? GameVersion { get; set; }

    public string? CommodityName { get; set; }

    public string? StarSystemName { get; set; }

    public string? PlanetName { get; set; }

    public string? MoonName { get; set; }

    public string? SpaceStationName { get; set; }

    public string? OutpostName { get; set; }

    public string? CityName { get; set; }
}
