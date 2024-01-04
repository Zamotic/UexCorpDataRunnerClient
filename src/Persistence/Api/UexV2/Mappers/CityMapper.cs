using UexCorpDataRunner.Domain.DataRunnerV2;
using UexCorpDataRunner.Persistence.Api.UexV2.DataTransferObjects;

namespace UexCorpDataRunner.Persistence.Api.UexV2.Mappers;
public static class CityMapper
{
    public static City ConvertFromDto(this CityDto input)
    {
        City output = new City();
        output.Id = input.Id;
        output.DateAdded = input.DateAdded;
        output.DateModified = input.DateModified;
        output.Name = input.Name;
        output.Code = input.Code;
        output.IsAvailable = input.IsAvailable;
        output.DateAdded = input.DateAdded;
        output.DateModified = input.DateModified;

        output.PlanetId = input.PlanetId;
        output.MoonId = input.MoonId;
        output.Nickname = input.Nickname;

        output.HasTradeTerminal = input.HasTradeTerminal;
        output.HasShops = input.HasShops;

        return output;
    }

    public static IReadOnlyCollection<City> ConvertFromDto(this IEnumerable<CityDto> input)
    {
        List<City> output = new List<City>();
        foreach (var item in input)
        {
            output.Add(ConvertFromDto(item));
        }

        return output.AsReadOnly();
    }

    public static CityDto ConvertToDto(this City input)
    {
        CityDto output = new CityDto();
        output.Id = input.Id;
        output.DateAdded = input.DateAdded;
        output.DateModified = input.DateModified;
        output.Name = input.Name;
        output.Code = input.Code;
        output.IsAvailable = input.IsAvailable;
        output.DateAdded = input.DateAdded;
        output.DateModified = input.DateModified;

        output.PlanetId = input.PlanetId;
        output.MoonId = input.MoonId;
        output.Nickname = input.Nickname;

        output.HasTradeTerminal = input.HasTradeTerminal;
        output.HasShops = input.HasShops;

        return output;
    }

    public static IReadOnlyCollection<CityDto> ConvertToDto(this IEnumerable<City> input)
    {
        List<CityDto> output = new List<CityDto>();
        foreach (var item in input)
        {
            output.Add(ConvertToDto(item));
        }

        return output.AsReadOnly();
    }
}
