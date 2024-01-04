using UexCorpDataRunner.Domain.DataRunnerV2;
using UexCorpDataRunner.Persistence.Api.UexV2.DataTransferObjects;

namespace UexCorpDataRunner.Persistence.Api.UexV2.Mappers;
public static class OutpostMapper
{
    public static Outpost ConvertFromDto(this OutpostDto input)
    {
        Outpost output = new Outpost();
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

    public static IReadOnlyCollection<Outpost> ConvertFromDto(this IEnumerable<OutpostDto> input)
    {
        List<Outpost> output = new List<Outpost>();
        foreach (var item in input)
        {
            output.Add(ConvertFromDto(item));
        }

        return output.AsReadOnly();
    }

    public static OutpostDto ConvertToDto(this Outpost input)
    {
        OutpostDto output = new OutpostDto();
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

    public static IReadOnlyCollection<OutpostDto> ConvertToDto(this IEnumerable<Outpost> input)
    {
        List<OutpostDto> output = new List<OutpostDto>();
        foreach (var item in input)
        {
            output.Add(ConvertToDto(item));
        }

        return output.AsReadOnly();
    }
}
