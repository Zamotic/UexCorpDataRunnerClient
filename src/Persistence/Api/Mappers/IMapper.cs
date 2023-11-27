using UexCorpDataRunner.Domain.DataRunnerV2;
using UexCorpDataRunner.Persistence.Api.UexV2.DataTransferObjects;

namespace UexCorpDataRunner.Persistence.Api.Mappers;

public interface IMapperV2
{
    StarSystem ConvertFromDto(StarSystemDto source);
    IReadOnlyCollection<StarSystem> ConvertFromDto(IEnumerable<StarSystemDto> sourceCollection);
}
