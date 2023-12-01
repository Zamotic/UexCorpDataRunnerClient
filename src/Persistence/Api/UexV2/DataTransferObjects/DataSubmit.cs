using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace UexCorpDataRunner.Persistence.Api.UexV2.DataTransferObjects;
public class DataSubmit
{
    [JsonPropertyName("id_terminal")]
    public int TerminalId { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; } = "Commodity";

    [JsonPropertyName("is_production")]
    public bool IsProduction { get; set; }

    [JsonPropertyName("prices")]
    public DataSubmitPrice[] _DataSubmitPrices { get; set; }

    [JsonPropertyName("faction_affinity")]
    public int FactionAffinity { get; set; }

    [JsonPropertyName("details")]
    public string? Details { get; set; }

    [JsonPropertyName("game_version")]
    public string GameVersion { get; set; }

}
