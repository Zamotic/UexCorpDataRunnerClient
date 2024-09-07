using UexCorpDataRunner.Domain.DataRunnerV2;
using UexCorpDataRunner.Persistence.Api.UexV2.DataTransferObjects;

namespace UexCorpDataRunner.Persistence.Api.UexV2.Mappers;
public static class MoonMapper
{
    public static Moon ConvertFromDto(this MoonDto input)
    {
        Moon output = new Moon();
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
        output.NameOrigin = input.NameOrigin;

        return output;
    }

    public static IReadOnlyCollection<Moon> ConvertFromDto(this IEnumerable<MoonDto> input)
    {
        List<Moon> output = new List<Moon>();
        foreach (var item in input)
        {
            output.Add(ConvertFromDto(item));
        }

        return output.AsReadOnly();
    }

    public static MoonDto ConvertToDto(this Moon input)
    {
        MoonDto output = new MoonDto();
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
        output.NameOrigin = input.NameOrigin;

        return output;
    }

    public static IReadOnlyCollection<MoonDto> ConvertToDto(this IEnumerable<Moon> input)
    {
        List<MoonDto> output = new List<MoonDto>();
        foreach (var item in input)
        {
            output.Add(ConvertToDto(item));
        }

        return output.AsReadOnly();
    }
}