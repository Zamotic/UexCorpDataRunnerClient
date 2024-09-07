using UexCorpDataRunner.Domain.DataRunnerV2;
using UexCorpDataRunner.Persistence.Api.UexV2.DataTransferObjects;


namespace UexCorpDataRunner.Persistence.Api.UexV2.Mappers;
public static class DataSubmitResponseMapper
{
    public static DataSubmitResponse ConvertFromDto(this UexResponseDto<DataSubmitResponseDto> input)
    {
        DataSubmitResponse output = new DataSubmitResponse();

        output.Response = true;
        if (input.Code != 200)
        {
            output.Response = false;
        }

        if (input.Data == null)
        {
            return output;
        }

        output.Username = input.Data.Username;
        output.DateAdded = input.Data.DateAdded;
        output.ReportIds.AddRange(input.Data.ReportIds);

        return output;
    }

    public static IReadOnlyCollection<DataSubmitResponse> ConvertFromDto(this IEnumerable<UexResponseDto<DataSubmitResponseDto>> input)
    {
        throw new NotImplementedException();
    }

    public static DataSubmitResponseDto ConvertToDto(this DataSubmitResponse input)
    {
        DataSubmitResponseDto output = new DataSubmitResponseDto();

        output.Username = input.Username;
        output.DateAdded = input.DateAdded;
        output.ReportIds.AddRange(input.ReportIds);

        return output;
    }

    public static IReadOnlyCollection<DataSubmitResponseDto> ConvertToDto(this IEnumerable<DataSubmitResponse> input)
    {
        throw new NotImplementedException();
    }
}
