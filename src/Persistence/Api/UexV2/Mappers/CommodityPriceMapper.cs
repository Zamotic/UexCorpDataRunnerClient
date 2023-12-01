using UexCorpDataRunner.Domain.DataRunnerV2;
using UexCorpDataRunner.Persistence.Api.UexV2.DataTransferObjects;

namespace UexCorpDataRunner.Persistence.Api.UexV2.Mappers;
public static class CommodityPriceMapper
{
    public static CommodityPrice ConvertFromDto(this CommodityPriceDto input)
    {
        CommodityPrice output = new CommodityPrice();
        output.Id = input.Id;
        output.CommodityId = input.CommodityId;
        output.StarSystemId = input.StarSystemId;
        output.PlanetId = input.PlanetId;
        output.MoonId = input.MoonId;
        output.CityId = input.CityId;
        output.OutpostId = input.OutpostId;
        output.TerminalId = input.TerminalId;
        output.PriceBuy = input.PriceBuy;
        output.PriceBuyMin = input.PriceBuyMin;
        output.PriceBuyMinWeek = input.PriceBuyMinWeek;
        output.PriceBuyMinMonth = input.PriceBuyMinMonth;
        output.PriceBuyMax = input.PriceBuyMax;
        output.PriceBuyMaxWeek = input.PriceBuyMaxWeek;
        output.PriceBuyMaxMonth = input.PriceBuyMaxMonth;
        output.PriceBuyAvg = input.PriceBuyAvg;
        output.PriceBuyAvgWeek = input.PriceBuyAvgWeek;
        output.PriceBuyAvgMonth = input.PriceBuyAvgMonth;
        output.PriceSell = input.PriceSell;
        output.PriceSellMin = input.PriceSellMin;
        output.PriceSellMinWeek = input.PriceSellMinWeek;
        output.PriceSellMinMonth = input.PriceSellMinMonth;
        output.PriceSellMax = input.PriceSellMax;
        output.PriceSellMaxWeek = input.PriceSellMaxWeek;
        output.PriceSellMaxMonth = input.PriceSellMaxMonth;
        output.PriceSellAvg = input.PriceSellAvg;
        output.PriceSellAvgWeek = input.PriceSellAvgWeek;
        output.PriceSellAvgMonth = input.PriceSellAvgMonth;

        return output;
    }

    public static IReadOnlyCollection<CommodityPrice> ConvertFromDto(this IEnumerable<CommodityPriceDto> sourceCollection)
    {
        List<CommodityPrice> returnCollection = new List<CommodityPrice>();
        foreach (var source in sourceCollection)
        {
            CommodityPrice output = ConvertFromDto(source);
            returnCollection.Add(output);
        }

        return returnCollection.AsReadOnly();
    }

    public static CommodityPriceDto ConvertToDto(this CommodityPrice input)
    {
        CommodityPriceDto output = new CommodityPriceDto();
        output.Id = input.Id;
        output.CommodityId = input.CommodityId;
        output.StarSystemId = input.StarSystemId;
        output.PlanetId = input.PlanetId;
        output.MoonId = input.MoonId;
        output.CityId = input.CityId;
        output.OutpostId = input.OutpostId;
        output.TerminalId = input.TerminalId;
        output.PriceBuy = input.PriceBuy;
        output.PriceBuyMin = input.PriceBuyMin;
        output.PriceBuyMinWeek = input.PriceBuyMinWeek;
        output.PriceBuyMinMonth = input.PriceBuyMinMonth;
        output.PriceBuyMax = input.PriceBuyMax;
        output.PriceBuyMaxWeek = input.PriceBuyMaxWeek;
        output.PriceBuyMaxMonth = input.PriceBuyMaxMonth;
        output.PriceBuyAvg = input.PriceBuyAvg;
        output.PriceBuyAvgWeek = input.PriceBuyAvgWeek;
        output.PriceBuyAvgMonth = input.PriceBuyAvgMonth;
        output.PriceSell = input.PriceSell;
        output.PriceSellMin = input.PriceSellMin;
        output.PriceSellMinWeek = input.PriceSellMinWeek;
        output.PriceSellMinMonth = input.PriceSellMinMonth;
        output.PriceSellMax = input.PriceSellMax;
        output.PriceSellMaxWeek = input.PriceSellMaxWeek;
        output.PriceSellMaxMonth = input.PriceSellMaxMonth;
        output.PriceSellAvg = input.PriceSellAvg;
        output.PriceSellAvgWeek = input.PriceSellAvgWeek;
        output.PriceSellAvgMonth = input.PriceSellAvgMonth;

        return output;
    }
    public static IReadOnlyCollection<CommodityPriceDto> ConvertToDto(this IEnumerable<CommodityPrice> sourceCollection)
    {
        List<CommodityPriceDto> returnCollection = new List<CommodityPriceDto>();
        foreach (var source in sourceCollection)
        {
            CommodityPriceDto output = ConvertToDto(source);
            returnCollection.Add(output);
        }

        return returnCollection.AsReadOnly();
    }

}
