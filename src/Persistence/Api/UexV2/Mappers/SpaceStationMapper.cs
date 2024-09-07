using UexCorpDataRunner.Domain.DataRunnerV2;
using UexCorpDataRunner.Persistence.Api.UexV2.DataTransferObjects;

namespace UexCorpDataRunner.Persistence.Api.UexV2.Mappers;
public static class SpaceStationMapper
{
    public static SpaceStation ConvertFromDto(this SpaceStationDto input)
    {
        SpaceStation output = new SpaceStation();
        output.Id = input.Id;
        output.DateAdded = input.DateAdded;
        output.DateModified = input.DateModified;
        output.Name = input.Name;
        output.Code = input.Code;
        output.IsAvailable = input.IsAvailable;
        output.DateAdded = input.DateAdded;
        output.DateModified = input.DateModified;

        output.StarSystemId = input.StarSystemId;
        output.PlanetId = input.PlanetId;
        output.MoonId = input.MoonId;
        output.CityId = input.CityId;

        return output;
    }

    public static IReadOnlyCollection<SpaceStation> ConvertFromDto(this IEnumerable<SpaceStationDto> input)
    {
        List<SpaceStation> output = new List<SpaceStation>();
        foreach (var item in input)
        {
            output.Add(item.ConvertFromDto());
        }

        return output.AsReadOnly();
    }

    public static SpaceStationDto ConvertToDto(this SpaceStation input)
    {
        SpaceStationDto output = new SpaceStationDto();
        output.Id = input.Id;
        output.DateAdded = input.DateAdded;
        output.DateModified = input.DateModified;
        output.Name = input.Name;
        output.Code = input.Code;
        output.IsAvailable = input.IsAvailable;
        output.DateAdded = input.DateAdded;
        output.DateModified = input.DateModified;

        output.StarSystemId = input.StarSystemId;
        output.PlanetId = input.PlanetId;
        output.MoonId = input.MoonId;
        output.CityId = input.CityId;

        return output;
    }

    public static IReadOnlyCollection<SpaceStationDto> ConvertToDto(this IEnumerable<SpaceStation> input)
    {
        List<SpaceStationDto> output = new List<SpaceStationDto>();
        foreach (var item in input)
        {
            output.Add(item.ConvertToDto());
        }

        return output.AsReadOnly();
    }
}
