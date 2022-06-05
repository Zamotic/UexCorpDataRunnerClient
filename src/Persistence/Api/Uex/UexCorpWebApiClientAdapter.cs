using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UexCorpDataRunner.Application.WebClient;
using UexCorpDataRunner.Persistence.Api.Uex.DataTransferObjects;

namespace UexCorpDataRunner.Persistence.Api.Uex;
public class UexCorpWebApiClientAdapter : IUexCorpWebApiClientAdapter
{
    private readonly IUexCorpWebApiClient _WebClient;
    private readonly IMapper _Mapper;

    public UexCorpWebApiClientAdapter(IUexCorpWebApiClient webClient, IMapper mapper)
    {
        _WebClient = webClient;
        _Mapper = mapper;
    }

    public async Task<IReadOnlyCollection<Domain.DataRunner.System>> GetSystemsAsync()
    {
        var systemDtos = await _WebClient.GetSystemsAsync();

        var systems = _Mapper.Map<List<Domain.DataRunner.System>>(systemDtos);
        return systems;
    }

    public async Task<IReadOnlyCollection<Domain.DataRunner.Planet>> GetPlanetsAsync(string systemCode)
    {
        var planetDtos = await _WebClient.GetPlanetsAsync(systemCode);

        var planets = _Mapper.Map<List<Domain.DataRunner.Planet>>(planetDtos);
        return planets;
    }

    public async Task<IReadOnlyCollection<Domain.DataRunner.Satellite>> GetSatellitesAsync(string systemCode)
    {
        var satelliteDtos = await _WebClient.GetSatellitesAsync(systemCode);

        var satellites = _Mapper.Map<List<Domain.DataRunner.Satellite>>(satelliteDtos);
        return satellites;
    }

    public async Task<IReadOnlyCollection<Domain.DataRunner.City>> GetCitiesAsync(string systemCode)
    {
        var citiesDtos = await _WebClient.GetCitiesAsync(systemCode);

        var cities = _Mapper.Map<List<Domain.DataRunner.City>>(citiesDtos);
        return cities;
    }

    public async Task<IReadOnlyCollection<Domain.DataRunner.Tradeport>> GetTradeportsAsync(string systemCode)
    {
        var tradeportsDtos = await _WebClient.GetTradeportsAsync(systemCode);

        var tradeports = _Mapper.Map<List<Domain.DataRunner.Tradeport>>(tradeportsDtos);
        return tradeports;
    }

    public async Task<IReadOnlyCollection<Domain.DataRunner.Commodity>> GetCommoditiesAsync()
    {
        var commoditiesDtos = await _WebClient.GetCommoditiesAsync();

        var commodities = _Mapper.Map<List<Domain.DataRunner.Commodity>>(commoditiesDtos);
        return commodities;
    }
}
