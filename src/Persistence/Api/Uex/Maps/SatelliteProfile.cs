using AutoMapper;
using UexCorpDataRunner.Persistence.Api.Uex.DataTransferObjects;

namespace UexCorpDataRunner.Persistence.Api.Uex.Maps;
public class SatelliteProfile : Profile
{
    public SatelliteProfile()
    { 
        CreateMap<SatelliteDto, Domain.DataRunner.Satellite>();
    }
}
