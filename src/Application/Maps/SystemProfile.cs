using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using UexCorpDataRunner.Application.DataTransferObjects;
using UexCorpDataRunner.Domain.Models;

namespace UexCorpDataRunner.DesktopClient.WebClient.Maps;
public class SystemProfile : Profile
{
    public SystemProfile()
    {
        CreateMap<SystemDto, Domain.Models.System>();
    }
}
