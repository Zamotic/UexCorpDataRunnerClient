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
}
