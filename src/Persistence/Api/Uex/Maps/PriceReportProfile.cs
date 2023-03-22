using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UexCorpDataRunner.Domain.DataRunner;
using UexCorpDataRunner.Persistence.Api.Uex.DataTransferObjects;

namespace UexCorpDataRunner.Persistence.Api.Uex.Maps;
public class PriceReportProfile : Profile
{
    public PriceReportProfile()
    {
        CreateMap<Domain.DataRunner.PriceReport, PriceReportDto>()
            .ForMember(dest => dest.Production, opt => opt.Ignore());
        CreateMap<UexResponseDto<string>, Domain.DataRunner.PriceReportResponse>()
            .ForMember(dest => dest.Response, opt => opt.MapFrom(src => src.Code.Equals(200) ? true : false))
            .ForMember(dest => dest.StatusMessage, opt => opt.MapFrom(src => src.Status));
        CreateMap<UexResponseDto<ICollection<string>>, Domain.DataRunner.PriceReportsResponse>()
            .ConvertUsing((s, _, context) =>
            {
                return new Domain.DataRunner.PriceReportsResponse()
                {
                    Response = s.Code.Equals(200) ? true : false,
                    StatusMessage = string.IsNullOrEmpty(s.Status) ? string.Empty : s.Status,
                    ReturnedDataList = (s?.Data is null) ? new List<string>() : s.Data!.Select(x => x).ToList()
                };
            });
    }
}
