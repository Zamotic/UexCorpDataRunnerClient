using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UexCorpDataRunner.Domain.DataRunnerV2;
using UexCorpDataRunner.Persistence.Api.UexV2.DataTransferObjects;

namespace UexCorpDataRunner.Persistence.Api.Mappers;
public class V2DtoMapper : IMapperV2
{
    public IReadOnlyCollection<TOutput> ConvertFromDto<TInput, TOutput>(IEnumerable<IConvertibleFromDto<TInput, TOutput>> sourceCollection)
    {
        List<TOutput> returnCollection = new List<TOutput>();
        foreach (var source in sourceCollection)
        {
            TOutput output = source.ConvertFromDto();
            returnCollection.Add(output);
        }

        return returnCollection.AsReadOnly();
    }
}
