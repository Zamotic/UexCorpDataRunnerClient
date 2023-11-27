using UexCorpDataRunner.Domain.DataRunnerV2;

namespace UexCorpDataRunner.Persistence.Api.UexV2;
public interface IUexCorpWebApiClientAdapter
{
    //Task<GameVersion> GetCurrentVersionAsync();
    Task<IReadOnlyCollection<StarSystem>> GetSystemsAsync();
    //Task<IReadOnlyCollection<Planet>> GetPlanetsAsync(string systemCode);
    //Task<IReadOnlyCollection<Satellite>> GetSatellitesAsync(string systemCode);
    //Task<IReadOnlyCollection<City>> GetCitiesAsync(string systemCode);
    //Task<IReadOnlyCollection<Tradeport>> GetTradeportsAsync(string systemCode);
    //Task<IReadOnlyCollection<Commodity>> GetCommoditiesAsync();
    //Task<Tradeport> GetTradeportAsync(string tradeportCode);
    //Task<PriceReportResponse> SubmitPriceReportAsync(PriceReport priceReport);
    //Task<PriceReportsResponse> SubmitPriceReportsAsync(PriceReport[] priceReports);
}
