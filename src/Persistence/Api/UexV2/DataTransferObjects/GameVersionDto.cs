using System.Text.Json.Serialization;

namespace UexCorpDataRunner.Persistence.Api.UexV2.DataTransferObjects;
public class GameVersionDto
{
    [JsonPropertyName("live")]
    public string Live { get; set; } = string.Empty;
    [JsonPropertyName("ptu")]
    public string? Ptu { get; set; }
}
