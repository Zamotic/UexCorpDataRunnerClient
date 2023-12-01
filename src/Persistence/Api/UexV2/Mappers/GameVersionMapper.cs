using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UexCorpDataRunner.Persistence.Api.UexV2.DataTransferObjects;
using UexCorpDataRunner.Domain.DataRunnerV2;

namespace UexCorpDataRunner.Persistence.Api.UexV2.Mappers;
public static class GameVersionMapper
{
    public static GameVersion ConvertFromDto(this GameVersionDto input)
    {
        GameVersion gameVersion = new GameVersion();
        gameVersion.Live = input.Live;
        gameVersion.Ptu = input.Ptu;

        return gameVersion;
    }

    public static GameVersionDto ConvertToDto(this GameVersion input)
    {
        GameVersionDto gameVersionDto = new GameVersionDto();
        gameVersionDto.Live = input.Live;
        gameVersionDto.Ptu = input.Ptu;

        return gameVersionDto;
    }

}
