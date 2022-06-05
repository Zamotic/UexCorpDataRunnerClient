using AutoMapper;
using UexCorpDataRunner.Persistence.Api.Uex.DataTransferObjects;
using UexCorpDataRunner.Persistence.Api.Uex.Maps.Resolvers;

namespace UexCorpDataRunner.Persistence.Api.Uex.Maps;
public class TradeportProfile : Profile
{
    public TradeportProfile()
    {
        CreateMap<TradeportDto, Domain.DataRunner.Tradeport>();
        CreateMap<TradeListingDto, Domain.DataRunner.TradeListing>()
            .ForMember(t => t.Operation, opt => opt.MapFrom<OperationResolver>());
    }
}
