using System.Text.Json.Serialization;
using UexCorpDataRunner.Persistence.Api.Uex.DataTransferObjects.Converters;

namespace UexCorpDataRunner.Persistence.Api.Uex.DataTransferObjects;
public class VersionDto
{
    [JsonPropertyName("live")]
    public string? Live { get; set; }
    [JsonPropertyName("ptu")]
    public string? Ptu { get; set; }
}
