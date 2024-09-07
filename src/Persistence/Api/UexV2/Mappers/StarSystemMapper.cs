using UexCorpDataRunner.Domain.DataRunnerV2;
using UexCorpDataRunner.Persistence.Api.UexV2.DataTransferObjects;

namespace UexCorpDataRunner.Persistence.Api.UexV2.Mappers;
public static class StarSystemMapper
{
    public static StarSystem ConvertFromDto(this StarSystemDto input)
    {
        StarSystem output = new StarSystem();
        output.Id = input.Id;
        output.DateAdded = input.DateAdded;
        output.DateModified = input.DateModified;
        output.Name = input.Name;
        output.Code = input.Code;
        output.IsAvailable = input.IsAvailable;
        output.IsVisible = input.IsVisible;
        output.IsDefault = input.IsDefault;
        output.DateAdded = input.DateAdded;
        output.DateModified = input.DateModified;

        return output;
    }

    public static IReadOnlyCollection<StarSystem> ConvertFromDto(this IEnumerable<StarSystemDto> input)
    {
        List<StarSystem> output = new List<StarSystem>();
        foreach (var item in input)
        {
            output.Add(ConvertFromDto(item));
        }

        return output.AsReadOnly();
    }

    public static StarSystemDto ConvertToDto(this StarSystem input)
    {
        StarSystemDto output = new StarSystemDto();
        output.Id = input.Id;
        output.DateAdded = input.DateAdded;
        output.DateModified = input.DateModified;
        output.Name = input.Name;
        output.Code = input.Code;
        output.IsAvailable = input.IsAvailable;
        output.IsVisible = input.IsVisible;
        output.IsDefault = input.IsDefault;
        output.DateAdded = input.DateAdded;
        output.DateModified = input.DateModified;

        return output;
    }

    public static IReadOnlyCollection<StarSystemDto> ConvertToDto(this IEnumerable<StarSystem> input)
    {
        List<StarSystemDto> output = new List<StarSystemDto>();
        foreach (var item in input)
        {
            output.Add(ConvertToDto(item));
        }

        return output.AsReadOnly();
    }

}
