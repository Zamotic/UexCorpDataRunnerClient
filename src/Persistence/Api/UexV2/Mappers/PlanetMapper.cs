using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UexCorpDataRunner.Domain.DataRunnerV2;
using UexCorpDataRunner.Persistence.Api.UexV2.DataTransferObjects;

namespace UexCorpDataRunner.Persistence.Api.UexV2.Mappers;
public static class PlanetMapper
{
    public static Planet ConvertFromDto(this PlanetDto input)
    {
        Planet output = new Planet();
        output.Id = input.Id;
        output.DateAdded = input.DateAdded;
        output.DateModified = input.DateModified;
        output.Name = input.Name;
        output.Code = input.Code;
        output.IsAvailable = input.IsAvailable;
        output.DateAdded = input.DateAdded;
        output.DateModified = input.DateModified;

        output.StarSystemId = input.StarSystemId;
        output.NameOrigin = input.NameOrigin;
        output.IsLagrange = input.IsLagrange;

        return output;
    }

    public static IReadOnlyCollection<Planet> ConvertFromDto(this IEnumerable<PlanetDto> input)
    {
        List<Planet> output = new List<Planet>();
        foreach (var item in input)
        {
            output.Add(item.ConvertFromDto());
        }

        return output.AsReadOnly();
    }

    public static PlanetDto ConvertToDto(this Planet input)
    {
        PlanetDto output = new PlanetDto();
        output.Id = input.Id;
        output.DateAdded = input.DateAdded;
        output.DateModified = input.DateModified;
        output.Name = input.Name;
        output.Code = input.Code;
        output.IsAvailable = input.IsAvailable;
        output.DateAdded = input.DateAdded;
        output.DateModified = input.DateModified;

        output.StarSystemId = input.StarSystemId;
        output.NameOrigin = input.NameOrigin;
        output.IsLagrange = input.IsLagrange;

        return output;
    }

    public static IReadOnlyCollection<PlanetDto> ConvertToDto(this IEnumerable<Planet> input)
    {
        List<PlanetDto> output = new List<PlanetDto>();
        foreach (var item in input)
        {
            output.Add(item.ConvertToDto());
        }

        return output.AsReadOnly();
    }
}
