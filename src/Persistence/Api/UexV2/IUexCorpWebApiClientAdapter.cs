using UexCorpDataRunner.Domain.DataRunnerV2;

namespace UexCorpDataRunner.Persistence.Api.UexV2;
public interface IUexCorpWebApiClientAdapter
{
    Task<GameVersion> GetCurrentVersionAsync();
    Task<IReadOnlyCollection<StarSystem>> GetSystemsAsync();
    Task<IReadOnlyCollection<Terminal>> GetTerminalsAsync(int starSystemId);
    Task<IReadOnlyCollection<Planet>> GetPlanetsAsync(int starSystemId);
    Task<IReadOnlyCollection<Moon>> GetMoonsByStarSystemIdAsync(int starSystemId);
    Task<IReadOnlyCollection<Moon>> GetMoonsByPlanetIdAsync(int planetId);
    Task<IReadOnlyCollection<City>> GetCitiesByStarSystemIdAsync(int starSystemId);
    Task<IReadOnlyCollection<City>> GetCitiesByPlanetIdAsync(int planetId);
    Task<IReadOnlyCollection<City>> GetCitiesByMoonIdAsync(int moonId);
    Task<IReadOnlyCollection<Outpost>> GetOutpostsByStarSystemIdAsync(int starSystemId);
    Task<IReadOnlyCollection<Outpost>> GetOutpostsByPlanetIdAsync(int planetId);
    Task<IReadOnlyCollection<Outpost>> GetOutpostsByMoonIdAsync(int moonId);
    Task<IReadOnlyCollection<Commodity>> GetCommoditiesAsync();
    Task<IReadOnlyCollection<CommodityPrice>> GetCommodityPricesAsync(int terminalId);
    Task<DataSubmitResponse> SubmitDataAsync(DataSubmit dataSubmit);
}
