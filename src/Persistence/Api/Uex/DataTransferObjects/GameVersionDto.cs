using System.Text.Json.Serialization;
using UexCorpDataRunner.Persistence.Api.Converters;

namespace UexCorpDataRunner.Persistence.Api.Uex.DataTransferObjects;
public class GameVersionDto
{
    [JsonPropertyName("live")]
    public string Live { get; set; } = string.Empty;
    [JsonPropertyName("ptu")]
    public string? Ptu { get; set; }
}
