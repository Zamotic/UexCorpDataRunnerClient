using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UexCorpDataRunner.Persistence.Api.Uex.DataTransferObjects;

namespace UexCorpDataRunner.Persistence.Api.Uex.Maps;
public class PriceReportProfile : Profile
{
    public PriceReportProfile()
    {
        CreateMap<Domain.DataRunner.PriceReport, PriceReportDto>();
        CreateMap<UexResponseDto<int>, Domain.DataRunner.PriceReportResponse>()
            .ForMember(dest => dest.Response, opt => opt.MapFrom(src => src.Code.Equals(200) ? true : false))
            .ForMember(dest => dest.StatusMessage, opt => opt.MapFrom(src => src.Status));
    }
}
