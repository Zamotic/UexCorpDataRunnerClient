using System.Collections.ObjectModel;
using UexCorpDataRunner.Domain.DataRunner;
using UexCorpDataRunner.Domain.Services;
using UexCorpDataRunner.Persistence.Api.Uex;

namespace UexCorpDataRunner.Application.Services;
public class UexDataService : IUexDataService
{
    readonly IUexCorpWebApiClientAdapter _WebClientAdapter;

    public UexDataService(IUexCorpWebApiClientAdapter webClientAdapter)
    {
        _WebClientAdapter = webClientAdapter;
    }

    public async Task<IReadOnlyCollection<City>> GetAllCitiesAsync(string systemCode)
    {
        await Task.Delay(1);
        throw new NotImplementedException();
    }

    public async Task<IReadOnlyCollection<Commodity>> GetAllCommoditiesAsync()
    {
        await Task.Delay(1);
        throw new NotImplementedException();
    }

    public async Task<IReadOnlyCollection<Planet>> GetAllPlanetsAsync(string systemCode)
    {
        var collection = await _WebClientAdapter.GetPlanetsAsync(systemCode);
        return collection;
    }

    public async Task<IReadOnlyCollection<Satellite>> GetAllSatellitesAsync(string systemCode)
    {
        await Task.Delay(1);
        throw new NotImplementedException();
    }

    public async Task<IReadOnlyCollection<Domain.DataRunner.System>> GetAllSystemsAsync()
    {
        var collection = await _WebClientAdapter.GetSystemsAsync();
        return collection;
    }

    public async Task<IReadOnlyCollection<Tradeport>> GetAllTradeportsAsync(string systemCode)
    {
        await Task.Delay(1);
        throw new NotImplementedException();
    }
}
