using AutoMapper;
using UexCorpDataRunner.Persistence.Api.Uex.DataTransferObjects;

namespace UexCorpDataRunner.Persistence.Api.Uex.Maps;
public class VersionProfile : Profile
{
    public VersionProfile()
    {
        CreateMap<VersionDto, Domain.DataRunner.Version>();
    }
}
