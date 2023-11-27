using UexCorpDataRunner.Domain.DataRunnerV2;
using UexCorpDataRunner.Persistence.Api.UexV2.DataTransferObjects;

namespace UexCorpDataRunner.Persistence.Api.Mappers;

public interface IMapperV2
{
    GameVersion ConvertFromDto(GameVersionDto source);
    StarSystem ConvertFromDto(StarSystemDto source);
    IReadOnlyCollection<StarSystem> ConvertFromDto(IEnumerable<StarSystemDto> sourceCollection);
}
