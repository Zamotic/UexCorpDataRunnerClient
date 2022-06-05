using AutoMapper;
using UexCorpDataRunner.Persistence.Api.Uex.DataTransferObjects;

namespace UexCorpDataRunner.Persistence.Api.Uex.Maps;
public class CityProfile : Profile
{
    public CityProfile()
    { 
        CreateMap<CityDto, Domain.DataRunner.City>();
    }
}
