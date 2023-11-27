using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UexCorpDataRunner.Domain.DataRunnerV2;
using UexCorpDataRunner.Persistence.Api.UexV2.DataTransferObjects;

namespace UexCorpDataRunner.Persistence.Api.Mappers;
public class V2DtoMapper : IMapperV2
{
    public GameVersion ConvertFromDto(GameVersionDto source)
    {
        GameVersion gameVersion = new GameVersion
        {
            Live = source.Live,
            Ptu = source.Ptu
        };

        return gameVersion;
    }

    public StarSystem ConvertFromDto(StarSystemDto source)
    {
        StarSystem starSystem = new StarSystem();
        starSystem.Id = source.Id;
        starSystem.Name = source.Name!;
        starSystem.Code = source.Code!;
        starSystem.IsAvailable = source.IsAvailable;
        starSystem.IsVisible = source.IsVisible;
        starSystem.IsDefault = source.IsDefault;
        starSystem.DateAdded = source.DateAdded;
        starSystem.DateModified = source.DateModified;

        return starSystem;
    }

    public IReadOnlyCollection<StarSystem> ConvertFromDto(IEnumerable<StarSystemDto> sourceCollection)
    { 
        List<StarSystem> starSystems = new List<StarSystem>();
        foreach (var source in sourceCollection)
        {
            StarSystem starSystem = ConvertFromDto(source);
            starSystems.Add(starSystem);
        }

        return starSystems.AsReadOnly();
    }
}
