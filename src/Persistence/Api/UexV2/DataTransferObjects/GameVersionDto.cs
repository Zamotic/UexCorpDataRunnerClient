using System.Text.Json.Serialization;

namespace UexCorpDataRunner.Persistence.Api.UexV2.DataTransferObjects;
public class GameVersionDto : IConvertibleFromDto<GameVersionDto, Domain.DataRunnerV2.GameVersion>
{
    [JsonPropertyName("live")]
    public string Live { get; set; } = string.Empty;
    [JsonPropertyName("ptu")]
    public string? Ptu { get; set; }

    public Domain.DataRunnerV2.GameVersion ConvertFromDto()
    {
        Domain.DataRunnerV2.GameVersion gameVersion = new Domain.DataRunnerV2.GameVersion();
        gameVersion.Live = this.Live;
        gameVersion.Ptu = this.Ptu;

        return gameVersion;
    }
}
