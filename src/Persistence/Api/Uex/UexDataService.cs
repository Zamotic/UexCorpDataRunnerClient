using System.Collections.ObjectModel;
using UexCorpDataRunner.Domain.DataRunner;
using UexCorpDataRunner.Domain.Services;
using UexCorpDataRunner.Persistence.Api.Uex;

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

    public async Task<IReadOnlyList<Domain.DataRunner.System>> GetAllSystemsAsync()
    {
        var collection = await _WebClient.GetSystems();
        return new ReadOnlyCollection<Domain.DataRunner.System>(collection);
    }

    public Task<IReadOnlyList<Tradeport>> GetAllTradeportsAsync(string systemCode)
    {
        throw new NotImplementedException();
    }
}
