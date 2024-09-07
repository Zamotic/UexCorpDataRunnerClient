using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UexCorpDataRunner.Persistence.Api.UexV2.DataTransferObjects;
public interface IConvertibleFromDto<TInput, TOutput>
{
    TOutput ConvertFromDto();
}