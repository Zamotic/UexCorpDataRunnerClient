using UexCorpDataRunner.Domain.DataRunnerV2;
using UexCorpDataRunner.Persistence.Api.UexV2.DataTransferObjects;

namespace UexCorpDataRunner.Persistence.Api.UexV2.Mappers;
public static class DataSubmitMapper
{
    public static DataSubmit ConvertFromDto(this DataSubmitDto input)
    {
        DataSubmit output = new DataSubmit();

        output.TerminalId = input.TerminalId;
        output.Type = input.Type;
        output.IsProduction = input.IsProduction;
        output.FactionAffinity = input.FactionAffinity;
        output.Details = input.Details;
        output.GameVersion = input.GameVersion;

        output.DataSubmitPrices.AddRange(input.DataSubmitPrices.ConvertFromDto());

        return output;
    }

    public static IReadOnlyCollection<DataSubmit> ConvertFromDto(this IEnumerable<DataSubmitDto> input)
    {
        List<DataSubmit> output = new List<DataSubmit>();
        foreach (var item in input)
        {
            output.Add(ConvertFromDto(item));
        }

        return output.AsReadOnly();
    }

    public static DataSubmitDto ConvertToDto(this DataSubmit input)
    {
        DataSubmitDto output = new DataSubmitDto();

        output.TerminalId = input.TerminalId;
        output.Type = input.Type;
        output.IsProduction = input.IsProduction;
        output.FactionAffinity = input.FactionAffinity;
        output.Details = input.Details;
        output.GameVersion = input.GameVersion;

        output.DataSubmitPrices.AddRange(input.DataSubmitPrices.ConvertToDto());

        return output;
    }

    public static IReadOnlyCollection<DataSubmitDto> ConvertToDto(this IEnumerable<DataSubmit> input)
    {
        List<DataSubmitDto> output = new List<DataSubmitDto>();
        foreach (var item in input)
        {
            output.Add(ConvertToDto(item));
        }

        return output.AsReadOnly();
    }
}
