using AutoMapper;
using UexCorpDataRunner.Persistence.Api.Uex.DataTransferObjects;

namespace UexCorpDataRunner.Persistence.Api.Uex.Maps;
public class StarSystemProfile : Profile
{
    public StarSystemProfile()
    {
        CreateMap<StarSystemDto, Domain.DataRunner.StarSystem>();
    }
}
