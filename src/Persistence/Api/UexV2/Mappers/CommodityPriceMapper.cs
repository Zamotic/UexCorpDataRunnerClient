using UexCorpDataRunner.Domain.DataRunnerV2;
using UexCorpDataRunner.Persistence.Api.UexV2.DataTransferObjects;

namespace UexCorpDataRunner.Persistence.Api.UexV2.Mappers;
public static class CommodityPriceMapper
{
    public static CommodityPrice ConvertFromDto(this CommodityPriceDto input)
    {
        CommodityPrice output = new CommodityPrice();
        output.Id = input.Id;
        output.DateAdded = input.DateAdded;
        output.DateModified = input.DateModified;
        output.CommodityId = input.CommodityId;
        output.StarSystemId = input.StarSystemId;
        output.PlanetId = input.PlanetId;
        output.MoonId = input.MoonId;
        output.CityId = input.CityId;
        output.OutpostId = input.OutpostId;
        output.TerminalId = input.TerminalId;
        output.BuyPrice = Convert.ToDecimal(input.BuyPrice);
        output.BuyPriceMin = Convert.ToDecimal(input.BuyPriceMin);
        //output.BuyPriceMinWeek = Convert.ToDecimal(input.BuyPriceMinWeek);
        //output.BuyPriceMinMonth = Convert.ToDecimal(input.BuyPriceMinMonth);
        output.BuyPriceMax = Convert.ToDecimal(input.BuyPriceMax);
        //output.BuyPriceMaxWeek = Convert.ToDecimal(input.BuyPriceMaxWeek);
        //output.BuyPriceMaxMonth = Convert.ToDecimal(input.BuyPriceMaxMonth);
        output.BuyPriceAvg = Convert.ToDecimal(input.BuyPriceAvg);
        //output.BuyPriceAvgWeek = Convert.ToDecimal(input.BuyPriceAvgWeek);
        //output.BuyPriceAvgMonth = Convert.ToDecimal(input.BuyPriceAvgMonth);
        output.SellPrice = Convert.ToDecimal(input.SellPrice);
        output.SellPriceMin = Convert.ToDecimal(input.SellPriceMin);
        //output.SellPriceMinWeek = Convert.ToDecimal(input.SellPriceMinWeek);
        //output.SellPriceMinMonth = Convert.ToDecimal(input.SellPriceMinMonth);
        output.SellPriceMax = Convert.ToDecimal(input.SellPriceMax);
        //output.SellPriceMaxWeek = Convert.ToDecimal(input.SellPriceMaxWeek);
        //output.SellPriceMaxMonth = Convert.ToDecimal(input.SellPriceMaxMonth);
        output.SellPriceAvg = Convert.ToDecimal(input.SellPriceAvg);
        //output.SellPriceAvgWeek = Convert.ToDecimal(input.SellPriceAvgWeek);
        //output.SellPriceAvgMonth = Convert.ToDecimal(input.SellPriceAvgMonth);

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
        output.DateAdded = input.DateAdded;
        output.DateModified = input.DateModified;
        output.CommodityId = input.CommodityId;
        output.StarSystemId = input.StarSystemId;
        output.PlanetId = input.PlanetId;
        output.MoonId = input.MoonId;
        output.CityId = input.CityId;
        output.OutpostId = input.OutpostId;
        output.TerminalId = input.TerminalId;
        output.BuyPrice = Convert.ToSingle(input.BuyPrice);
        output.BuyPriceMin = Convert.ToSingle(input.BuyPriceMin);
        //output.BuyPriceMinWeek = Convert.ToSingle(input.BuyPriceMinWeek);
        //output.BuyPriceMinMonth = Convert.ToSingle(input.BuyPriceMinMonth);
        output.BuyPriceMax = Convert.ToSingle(input.BuyPriceMax);
        //output.BuyPriceMaxWeek = Convert.ToSingle(input.BuyPriceMaxWeek);
        //output.BuyPriceMaxMonth = Convert.ToSingle(input.BuyPriceMaxMonth);
        output.BuyPriceAvg = Convert.ToSingle(input.BuyPriceAvg);
        //output.BuyPriceAvgWeek = Convert.ToSingle(input.BuyPriceAvgWeek);
        //output.BuyPriceAvgMonth = Convert.ToSingle(input.BuyPriceAvgMonth);
        output.SellPrice = Convert.ToSingle(input.SellPrice);
        output.SellPriceMin = Convert.ToSingle(input.SellPriceMin);
        //output.SellPriceMinWeek = Convert.ToSingle(input.SellPriceMinWeek);
        //output.SellPriceMinMonth = Convert.ToSingle(input.SellPriceMinMonth);
        output.SellPriceMax = Convert.ToSingle(input.SellPriceMax);
        //output.SellPriceMaxWeek = Convert.ToSingle(input.SellPriceMaxWeek);
        //output.SellPriceMaxMonth = Convert.ToSingle(input.SellPriceMaxMonth);
        output.SellPriceAvg = Convert.ToSingle(input.SellPriceAvg);
        //output.SellPriceAvgWeek = Convert.ToSingle(input.SellPriceAvgWeek);
        //output.SellPriceAvgMonth = Convert.ToSingle(input.SellPriceAvgMonth);

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
