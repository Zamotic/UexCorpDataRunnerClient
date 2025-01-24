using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using UexCorpDataRunner.Persistence.Api.Converters;

namespace UexCorpDataRunner.Persistence.Api.UexV2.DataTransferObjects;

public class DataParametersDto
{
    public class DataParametersGlobalsDto
    {
        [JsonPropertyName("is_accepting_reports")]
        [JsonConverter(typeof(UexBooleanTypeJsonConverter))]
        public bool IsAcceptingReports { get; set; }

        [JsonPropertyName("game_version")]
        public string? GameVersion { get; set; }

        [JsonPropertyName("game_version_ptu")]
        public string? GameVersionPtu { get; set; }

        [JsonPropertyName("evaluation_period_days")]
        public int EvaluationPeriodDays { get; set; }

    }

    [JsonPropertyName("global")]
    public DataParametersGlobalsDto Global { get; set; } = new();

}
