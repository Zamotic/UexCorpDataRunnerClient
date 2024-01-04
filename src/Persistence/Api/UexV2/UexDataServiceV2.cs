using UexCorpDataRunner.Domain.DataRunnerV2;
using UexCorpDataRunner.Domain.Services;

namespace UexCorpDataRunner.Persistence.Api.UexV2;
public class UexDataServiceV2 : IUexDataServiceV2
{
    readonly IUexCorpWebApiClientAdapter _WebClientAdapter;

    public UexDataServiceV2(IUexCorpWebApiClientAdapter webClientAdapter)
    {
        _WebClientAdapter = webClientAdapter;
    }

    public virtual async Task<GameVersion> GetCurrentVersionAsync()
    {
        var version = await _WebClientAdapter.GetCurrentVersionAsync();
        return version;
    }

    public virtual async Task<IReadOnlyCollection<StarSystem>> GetAllSystemsAsync()
    {
        var collection = await _WebClientAdapter.GetSystemsAsync();
        return collection;
    }

    public virtual async Task<IReadOnlyCollection<Terminal>> GetTerminalsAsync(int starSystemId)
    {
        var collection = await _WebClientAdapter.GetTerminalsAsync(starSystemId);
        return collection;
    }

    public virtual async Task<IReadOnlyCollection<Planet>> GetAllPlanetsAsync(int starSystemId)
    {
        var collection = await _WebClientAdapter.GetPlanetsAsync(starSystemId);
        return collection;
    }

    public virtual async Task<IReadOnlyCollection<Moon>> GetAllMoonsByStarSystemIdAsync(int starSystemId)
    {
        var collection = await _WebClientAdapter.GetMoonsByStarSystemIdAsync(starSystemId);
        return collection;
    }

    public virtual async Task<IReadOnlyCollection<Moon>> GetAllMoonsByPlanetIdAsync(int planetId)
    {
        var collection = await _WebClientAdapter.GetMoonsByPlanetIdAsync(planetId);
        return collection;
    }

    public virtual async Task<IReadOnlyCollection<City>> GetAllCitiesByStarSystemIdAsync(int starSystemId)
    {
        var collection = await _WebClientAdapter.GetCitiesByStarSystemIdAsync(starSystemId);
        return collection;
    }

    public virtual async Task<IReadOnlyCollection<City>> GetAllCitiesByPlanetIdAsync(int planetId)
    {
        var collection = await _WebClientAdapter.GetCitiesByPlanetIdAsync(planetId);
        return collection;
    }

    public virtual async Task<IReadOnlyCollection<City>> GetAllCitiesByMoonIdAsync(int moonId)
    {
        var collection = await _WebClientAdapter.GetCitiesByMoonIdAsync(moonId);
        return collection;
    }

    public virtual async Task<IReadOnlyCollection<Outpost>> GetAllOutpostsByStarSystemIdAsync(int starSystemId)
    {
        var collection = await _WebClientAdapter.GetOutpostsByStarSystemIdAsync(starSystemId);
        return collection;
    }

    public virtual async Task<IReadOnlyCollection<Outpost>> GetAllOutpostsByPlanetIdAsync(int planetId)
    {
        var collection = await _WebClientAdapter.GetOutpostsByPlanetIdAsync(planetId);
        return collection;
    }

    public virtual async Task<IReadOnlyCollection<Outpost>> GetAllOutpostsByMoonIdAsync(int moonId)
    {
        var collection = await _WebClientAdapter.GetOutpostsByMoonIdAsync(moonId);
        return collection;
    }

    public virtual async Task<IReadOnlyCollection<Commodity>> GetAllCommoditiesAsync()
    {
        var collection = await _WebClientAdapter.GetCommoditiesAsync();
        return collection;
    }

    public virtual async Task<IReadOnlyCollection<CommodityPrice>> GetCommodityPricesAsync(int terminalId)
    {
        var collection = await _WebClientAdapter.GetCommodityPricesAsync(terminalId);
        return collection;
    }

    public virtual async Task<DataSubmitResponse> SubmitDataAsync(DataSubmit dataSubmit)
    {
        var response = await _WebClientAdapter.SubmitDataAsync(dataSubmit);
        return response;
    }
}
