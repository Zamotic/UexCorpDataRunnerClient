using UexCorpDataRunner.Persistence.Api.UexV2.DataTransferObjects;

namespace UexCorpDataRunner.Persistence.Api.UexV2;
public interface IUexCorpWebApiClient
{
    Task<GameVersionDto> GetCurrentVersionAsync();
    Task<ICollection<StarSystemDto>> GetSystemsAsync();
    Task<ICollection<PlanetDto>> GetPlanetsAsync(int starSystemId);
    Task<ICollection<MoonDto>> GetMoonsByStarSystemIdAsync(int starSystemId);
    Task<ICollection<MoonDto>> GetMoonsByPlanetIdAsync(int planetId);
    Task<ICollection<CommodityDto>> GetCommoditiesAsync();
    Task<ICollection<CommodityPriceDto>> GetCommodityPricesAsync(int terminalId);
    Task<ICollection<CommodityPriceDto>> GetCommodityPricesByCommodityIdAsync(int commodityId);
    Task<ICollection<TerminalDto>> GetTerminalsAsync(int starSystemId);
    Task<ICollection<CityDto>> GetCitiesByStarSystemIdAsync(int starSystemId);
    Task<ICollection<CityDto>> GetCitiesByPlanetIdAsync(int planetId);
    Task<ICollection<CityDto>> GetCitiesByMoonIdAsync(int moonId);
    Task<ICollection<OutpostDto>> GetOutpostsByStarSystemIdAsync(int starSystemId);
    Task<ICollection<OutpostDto>> GetOutpostsByPlanetIdAsync(int planetId);
    Task<ICollection<OutpostDto>> GetOutpostsByMoonIdAsync(int moonId);

    //Task<TradeportDto> GetTradeportAsync(string tradeportCode);
    //Task<UexResponseDto<string>> SubmitPriceReportAsync(PriceReportDto priceReport);
    //Task<UexResponseDto<ICollection<string>>> SubmitPriceReportsAsync(PriceReportDto[] priceReportDtos);
}