using UexCorpDataRunner.Domain.DataRunnerV2;
using UexCorpDataRunner.Persistence.Api.UexV2.DataTransferObjects;


namespace UexCorpDataRunner.Persistence.Api.UexV2.Mappers;
public static class DataSubmitResponseMapper
{
    public static DataSubmitResponse ConvertFromDto(this UexResponseDto<ICollection<string>> input)
    {
        DataSubmitResponse output = new DataSubmitResponse();

        if (input.Code == 200)
        {
            output.Response = true;
        }
        output.StatusMessage = input.Status;

        return output;
    }    
}
