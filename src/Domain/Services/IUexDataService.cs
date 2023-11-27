using UexCorpDataRunner.Domain.DataRunner;

namespace UexCorpDataRunner.Domain.Services;
public interface IUexDataService
{
    Task<GameVersion> GetCurrentVersionAsync();
    Task<IReadOnlyCollection<DataRunner.StarSystem>> GetAllSystemsAsync();
    Task<IReadOnlyCollection<Planet>> GetAllPlanetsAsync(string systemCode);
    Task<IReadOnlyCollection<Satellite>> GetAllSatellitesAsync(string systemCode);
    Task<IReadOnlyCollection<City>> GetAllCitiesAsync(string systemCode);
    Task<IReadOnlyCollection<Tradeport>> GetAllTradeportsAsync(string systemCode);
    Task<Tradeport> GetTradeportAsync(string tradeportCode);
    Task<IReadOnlyCollection<Commodity>> GetAllCommoditiesAsync();
    Task<PriceReportResponse> SubmitPriceReportAsync(PriceReport priceReport);
    Task<PriceReportsResponse> SubmitPriceReportsAsync(PriceReport[] priceReports);
}
