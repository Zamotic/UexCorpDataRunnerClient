using UexCorpDataRunner.Persistence.Api.UexV2.DataTransferObjects;
using UexCorpDataRunner.Domain.DataRunnerV2;

namespace UexCorpDataRunner.Persistence.Api.UexV2.Mappers;
public static class CommodityMapper
{
    public static Commodity ConvertFromDto(this CommodityDto input)
    {
        Commodity output = new Commodity();
        output.Id = input.Id;
        output.Name = input.Name;
        output.Code = input.Code;
        output.IsAvailable = input.IsAvailable;
        output.IsVisible = input.IsVisible;
        output.DateAdded = input.DateAdded;
        output.DateModified = input.DateModified;
        output.Name = input.Name;
        output.Code = input.Code;
        output.ParentId = input.ParentId;
        output.Slug = input.Slug;
        output.Kind = input.Kind;
        output.BuyPrice = Convert.ToDecimal(input.BuyPrice);
        output.SellPrice = Convert.ToDecimal(input.SellPrice);
        output.IsRaw = input.IsRaw;
        output.IsHarvestable = input.IsHarvestable;
        output.IsBuyable = input.IsBuyable;
        output.IsSellable = input.IsSellable;
        output.IsTemporary = input.IsTemporary;
        output.IsIllegal = input.IsIllegal;

        return output;
    }

    public static IReadOnlyCollection<Commodity> ConvertFromDto(this IEnumerable<CommodityDto> input)
    {
        List<Commodity> output = new List<Commodity>();
        foreach (var item in input)
        {
            output.Add(ConvertFromDto(item));
        }

        return output.AsReadOnly();
    }

    public static CommodityDto ConvertToDto(this Commodity input)
    {
        CommodityDto output = new CommodityDto();
        output.Id = input.Id;
        output.Name = input.Name;
        output.Code = input.Code;
        output.IsAvailable = input.IsAvailable;
        output.IsVisible = input.IsVisible;
        output.DateAdded = input.DateAdded;
        output.DateModified = input.DateModified;
        output.Name = input.Name;
        output.Code = input.Code;
        output.ParentId = input.ParentId;
        output.Slug = input.Slug;
        output.Kind = input.Kind;
        output.BuyPrice = Convert.ToSingle(input.BuyPrice);
        output.SellPrice = Convert.ToSingle(input.SellPrice);
        output.IsRaw = input.IsRaw;
        output.IsHarvestable = input.IsHarvestable;
        output.IsBuyable = input.IsBuyable;
        output.IsSellable = input.IsSellable;
        output.IsTemporary = input.IsTemporary;
        output.IsIllegal = input.IsIllegal;

        return output;
    }   

    public static IReadOnlyCollection<CommodityDto> ConvertToDto(this IEnumerable<Commodity> input)
    {
        List<CommodityDto> output = new List<CommodityDto>();
        foreach (var item in input)
        {
            output.Add(ConvertToDto(item));
        }

        return output.AsReadOnly();
    }
}
