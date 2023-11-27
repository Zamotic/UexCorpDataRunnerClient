using UexCorpDataRunner.Persistence.Api.UexV2.DataTransferObjects;

namespace UexCorpDataRunner.Persistence.Api.UexV2;
public interface IUexCorpWebApiClient
{
    Task<GameVersionDto> GetCurrentVersionAsync();
    Task<ICollection<StarSystemDto>> GetSystemsAsync();
    Task<ICollection<PlanetDto>> GetPlanetsAsync(int starSystemId);
    Task<ICollection<MoonDto>> GetMoonsAsync(int starSystemId);
    Task<ICollection<MoonDto>> GetMoonsAsync(int starSystemId,int planetId);
    Task<ICollection<CityDto>> GetCitiesAsync(int starSystemId);
    Task<ICollection<CommodityDto>> GetCommoditiesAsync();
    Task<ICollection<CommodityPriceDto>> GetCommodityPricesByTerminalIdAsync(int terminalId);
    Task<ICollection<CommodityPriceDto>> GetCommodityPricesByCommodityIdAsync(int commodityId);
    Task<ICollection<TerminalDto>> GetTerminalsAsync(int starSystemId);

    //Task<TradeportDto> GetTradeportAsync(string tradeportCode);
    //Task<UexResponseDto<string>> SubmitPriceReportAsync(PriceReportDto priceReport);
    //Task<UexResponseDto<ICollection<string>>> SubmitPriceReportsAsync(PriceReportDto[] priceReportDtos);
}