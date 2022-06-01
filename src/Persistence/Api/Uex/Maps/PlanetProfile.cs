using AutoMapper;
using UexCorpDataRunner.Persistence.Api.Uex.DataTransferObjects;

namespace UexCorpDataRunner.Persistence.Api.Uex.Maps;
public class PlanetProfile : Profile
{
    public PlanetProfile()
    { 
        CreateMap<PlanetDto, Domain.DataRunner.Planet>();
    }
}
