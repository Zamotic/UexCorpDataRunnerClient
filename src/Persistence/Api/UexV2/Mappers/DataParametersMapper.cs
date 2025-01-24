using UexCorpDataRunner.Domain.DataRunnerV2;
using UexCorpDataRunner.Persistence.Api.UexV2.DataTransferObjects;

namespace UexCorpDataRunner.Persistence.Api.UexV2.Mappers;

public static class DataParametersMapper
{
    public static DataParameters ConvertFromDto(this DataParametersDto dataParametersDto)
    {
        DataParameters output = new DataParameters();
        DataParameters.DataParametersGlobal global = new DataParameters.DataParametersGlobal();

        global.IsAcceptingReports = dataParametersDto.Global.IsAcceptingReports;
        global.GameVersion = dataParametersDto.Global.GameVersion;
        global.GameVersionPtu = dataParametersDto.Global.GameVersionPtu;
        global.EvaluationPeriodDays = dataParametersDto.Global.EvaluationPeriodDays;

        output.Global = global;

        return output;
    }

    public static DataParametersDto ConvertToDto(this DataParameters dataParameters)
    {
        DataParametersDto output = new DataParametersDto();
        DataParametersDto.DataParametersGlobalsDto global = new();

        global.IsAcceptingReports = dataParameters.Global.IsAcceptingReports;
        global.GameVersion = dataParameters.Global.GameVersion;
        global.GameVersionPtu = dataParameters.Global.GameVersionPtu;
        global.EvaluationPeriodDays = dataParameters.Global.EvaluationPeriodDays;

        output.Global = global;

        return output;
    }
}