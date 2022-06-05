using UexCorpDataRunner.Persistence.Api.Uex.DataTransferObjects;

namespace UexCorpDataRunner.Persistence.Api.Uex;
public interface IUexCorpWebApiClient
{
    Task<ICollection<SystemDto>> GetSystemsAsync();
    Task<ICollection<PlanetDto>> GetPlanetsAsync(string systemCode);
    Task<ICollection<SatelliteDto>> GetSatellitesAsync(string systemCode);
    Task<ICollection<CityDto>> GetCitiesAsync(string systemCode);
    Task<ICollection<TradeportDto>> GetTradeportsAsync(string systemCode);
    Task<ICollection<CommodityDto>> GetCommoditiesAsync();
}