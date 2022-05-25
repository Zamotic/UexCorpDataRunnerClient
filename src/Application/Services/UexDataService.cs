using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UexCorpDataRunner.Application.WebClient;
using UexCorpDataRunner.Domain.Models;
using UexCorpDataRunner.Domain.Services;

namespace UexCorpDataRunner.Application.Services;
public class UexDataService : IUexDataService
{
    readonly IUexCorpWebApiClient _WebClient;

    public UexDataService(IUexCorpWebApiClient webClient)
    {
        _WebClient = webClient;
    }

    public Task<IReadOnlyList<City>> GetAllCitiesAsync(string systemCode)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<Commodity>> GetAllCommoditiesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<Planet>> GetAllPlanetsAsync(string systemCode)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<Satellite>> GetAllSatellitesAsync(string systemCode)
    {
        throw new NotImplementedException();
    }

    public async Task<IReadOnlyList<Domain.Models.System>> GetAllSystemsAsync()
    {
        var collection = await _WebClient.GetSystems();
        return new ReadOnlyCollection<Domain.Models.System>(collection);
    }

    public Task<IReadOnlyList<Tradeport>> GetAllTradeportsAsync(string systemCode)
    {
        throw new NotImplementedException();
    }
}
