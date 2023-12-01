using UexCorpDataRunner.Domain.DataRunnerV2;
using UexCorpDataRunner.Persistence.Api.UexV2.DataTransferObjects;

namespace UexCorpDataRunner.Persistence.Api.UexV2.Mappers;
public static class TerminalMapper
{
    public static Terminal ConvertFromDto(this TerminalDto input)
    {
        Terminal output = new Terminal();
        output.StarSystemId = input.StarSystemId;
        output.StarSystemName = input.StarSystemName;
        output.PlanetId = input.PlanetId;
        output.PlanetName = input.PlanetName;
        output.MoonId = input.MoonId;
        output.MoonName = input.MoonName;
        output.SpaceStationId = input.SpaceStationId;
        output.SpaceStationName = input.SpaceStationName;
        output.OutpostId = input.OutpostId;
        output.OutpostName = input.OutpostName;
        output.CityId = input.CityId;
        output.CityName = input.CityName;
        output.Name = input.Name;
        output.Nickname = input.Nickname;
        output.Code = input.Code;
        output.Type = input.Type;
        return output;
    }

    public static IReadOnlyCollection<Terminal> ConvertFromDto(this IEnumerable<TerminalDto> sourceCollection)
    {
        List<Terminal> returnCollection = new List<Terminal>();
        foreach (var source in sourceCollection)
        {
            Terminal output = ConvertFromDto(source);
            returnCollection.Add(output);
        }

        return returnCollection.AsReadOnly();
    }

    public static TerminalDto ConvertToDto(this Terminal input)
    {
        TerminalDto output = new TerminalDto();
        output.StarSystemId = input.StarSystemId;
        output.StarSystemName = input.StarSystemName;
        output.PlanetId = input.PlanetId;
        output.PlanetName = input.PlanetName;
        output.MoonId = input.MoonId;
        output.MoonName = input.MoonName;
        output.SpaceStationId = input.SpaceStationId;
        output.SpaceStationName = input.SpaceStationName;
        output.OutpostId = input.OutpostId;
        output.OutpostName = input.OutpostName;
        output.CityId = input.CityId;
        output.CityName = input.CityName;
        output.Name = input.Name;
        output.Nickname = input.Nickname;
        output.Code = input.Code;
        output.Type = input.Type;

        return output;
    }

    public static IReadOnlyCollection<TerminalDto> ConvertToDto(this IEnumerable<Terminal> sourceCollection)
    {
        List<TerminalDto> returnCollection = new List<TerminalDto>();
        foreach (var source in sourceCollection)
        {
            TerminalDto output = ConvertToDto(source);
            returnCollection.Add(output);
        }

        return returnCollection.AsReadOnly();
    }
}
