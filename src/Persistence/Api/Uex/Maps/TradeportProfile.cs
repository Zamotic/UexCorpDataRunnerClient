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
            .ForMember(dest => dest.Code, opt => opt.Ignore());
        CreateMap<KeyValuePair<string, TradeListingDto>, Domain.DataRunner.TradeListing>()
            .ConstructUsing((src, context) => 
            {
                var tradeListing = context.Mapper.Map<TradeListingDto, Domain.DataRunner.TradeListing>(src.Value);
                tradeListing.Code = src.Key;
                return tradeListing;
            })
            .ForAllMembers(opt => opt.Ignore());
    }
}
