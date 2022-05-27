using AutoMapper;
using UexCorpDataRunner.Persistence.Api.Uex.DataTransferObjects;

namespace UexCorpDataRunner.Persistence.Api.Uex.Maps;
public class SystemProfile : Profile
{
    public SystemProfile()
    {
        CreateMap<SystemDto, Domain.DataRunner.System>();
    }
}
