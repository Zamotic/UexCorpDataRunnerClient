using AutoMapper;
using UexCorpDataRunner.Persistence.Api.Uex.DataTransferObjects;

namespace UexCorpDataRunner.Persistence.Api.Uex.Maps;
public class CommodityProfile : Profile
{
    public CommodityProfile()
    { 
        CreateMap<CommodityDto, Domain.DataRunner.Commodity>();
    }
}
