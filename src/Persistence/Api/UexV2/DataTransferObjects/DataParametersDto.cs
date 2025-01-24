using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace UexCorpDataRunner.Persistence.Api.UexV2.DataTransferObjects;

public class DataParametersDto
{
    [JsonPropertyName("global")]
    public DataParametersGlobalsDto Global { get; set; } = new();
}
