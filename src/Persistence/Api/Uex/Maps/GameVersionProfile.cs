using AutoMapper;
using UexCorpDataRunner.Persistence.Api.Uex.DataTransferObjects;

namespace UexCorpDataRunner.Persistence.Api.Uex.Maps;
public class GameVersionProfile : Profile
{
    public GameVersionProfile()
    {
        CreateMap<GameVersionDto, Domain.DataRunner.GameVersion>();
    }
}
