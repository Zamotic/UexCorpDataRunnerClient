﻿using AutoMapper;
using UexCorpDataRunner.Persistence.Api.Uex.DataTransferObjects;

namespace UexCorpDataRunner.Persistence.Api.Uex.Maps;
public class TradeportProfile : Profile
{
    public TradeportProfile()
    { 
        CreateMap<TradeportDto, Domain.DataRunner.Tradeport>();
    }
}
