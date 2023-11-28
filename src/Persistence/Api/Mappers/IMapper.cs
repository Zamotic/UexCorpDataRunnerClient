using UexCorpDataRunner.Domain.DataRunnerV2;
using UexCorpDataRunner.Persistence.Api.UexV2.DataTransferObjects;

namespace UexCorpDataRunner.Persistence.Api.Mappers;

public interface IMapperV2
{
    IReadOnlyCollection<TOutput> ConvertFromDto<TInput, TOutput>(IEnumerable<IConvertibleFromDto<TInput, TOutput>> sourceCollection);
}
