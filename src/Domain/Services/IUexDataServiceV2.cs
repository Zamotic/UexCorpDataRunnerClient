using UexCorpDataRunner.Domain.DataRunnerV2;

namespace UexCorpDataRunner.Domain.Services;
public interface IUexDataServiceV2
{
    Task<GameVersion> GetCurrentVersionAsync();
    Task<IReadOnlyCollection<StarSystem>> GetAllSystemsAsync();
    Task<IReadOnlyCollection<Terminal>> GetTerminalsAsync(int starSystemId);
    Task<IReadOnlyCollection<Planet>> GetAllPlanetsAsync(int starSystemId);
    Task<IReadOnlyCollection<Moon>> GetAllMoonsByStarSystemIdAsync(int starSystemId);
    Task<IReadOnlyCollection<Moon>> GetAllMoonsByPlanetIdAsync(int planetId);
    Task<IReadOnlyCollection<City>> GetAllCitiesByStarSystemIdAsync(int starSystemId);
    Task<IReadOnlyCollection<City>> GetAllCitiesByPlanetIdAsync(int planetId);
    Task<IReadOnlyCollection<City>> GetAllCitiesByMoonIdAsync(int moonId);
    Task<IReadOnlyCollection<Outpost>> GetAllOutpostsByStarSystemIdAsync(int starSystemId);
    Task<IReadOnlyCollection<Outpost>> GetAllOutpostsByPlanetIdAsync(int planetId);
    Task<IReadOnlyCollection<Outpost>> GetAllOutpostsByMoonIdAsync(int moonId);
    Task<IReadOnlyCollection<Commodity>> GetAllCommoditiesAsync();
    Task<IReadOnlyCollection<CommodityPrice>> GetCommodityPricesAsync(int terminalId);
    Task<DataSubmitResponse> SubmitDataAsync(DataSubmit dataSubmit);
}
