using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UexCorpDataRunner.Domain.DataRunnerV2;
using UexCorpDataRunner.Persistence.Api.UexV2.DataTransferObjects;

namespace UexCorpDataRunner.Persistence.Api.UexV2.Mappers;
public static class DataSubmitPriceMapper
{
    public static DataSubmitPrice ConvertFromDto(this DataSubmitPriceDto input)
    {
        DataSubmitPrice output = new DataSubmitPrice();

        output.CommodityId = input.CommodityId;
        output.SellPrice = input.SellPrice;
        output.SellScu = input.SellScu;
        output.SellStatus = input.SellStatus;
        output.BuyPrice = input.BuyPrice;
        output.BuyScu = input.BuyScu;
        output.BuyStatus = input.BuyStatus;

        return output;
    }

    public static IReadOnlyCollection<DataSubmitPrice> ConvertFromDto(this IEnumerable<DataSubmitPriceDto> input)
    {
        List<DataSubmitPrice> output = new List<DataSubmitPrice>();
        foreach (var item in input)
        {
            output.Add(ConvertFromDto(item));
        }

        return output.AsReadOnly();
    }

    public static DataSubmitPriceDto ConvertToDto(this DataSubmitPrice input)
    {
        DataSubmitPriceDto output = new DataSubmitPriceDto();

        output.CommodityId = input.CommodityId;
        output.SellPrice = input.SellPrice;
        output.SellScu = input.SellScu;
        output.SellStatus = input.SellStatus;
        output.BuyPrice = input.BuyPrice;
        output.BuyScu = input.BuyScu;
        output.BuyStatus = input.BuyStatus;

        return output;
    }

    public static IReadOnlyCollection<DataSubmitPriceDto> ConvertToDto(this IEnumerable<DataSubmitPrice> input)
    {
        List<DataSubmitPriceDto> output = new List<DataSubmitPriceDto>();
        foreach (var item in input)
        {
            output.Add(ConvertToDto(item));
        }

        return output.AsReadOnly();
    }
}
