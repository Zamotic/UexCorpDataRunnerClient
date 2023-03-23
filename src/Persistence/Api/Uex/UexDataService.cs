using UexCorpDataRunner.Domain.DataRunner;
using UexCorpDataRunner.Domain.Services;

namespace UexCorpDataRunner.Persistence.Api.Uex;
public class UexDataService : IUexDataService
{
    readonly IUexCorpWebApiClientAdapter _WebClientAdapter;

    public UexDataService(IUexCorpWebApiClientAdapter webClientAdapter)
    {
        _WebClientAdapter = webClientAdapter;
    }

    public virtual async Task<Domain.DataRunner.GameVersion> GetCurrentVersionAsync()
    {
        var version = await _WebClientAdapter.GetCurrentVersionAsync();
        return version;
    }

    public virtual async Task<IReadOnlyCollection<Domain.DataRunner.StarSystem>> GetAllSystemsAsync()
    {
        var collection = await _WebClientAdapter.GetSystemsAsync();
        return collection;
    }

    public virtual async Task<IReadOnlyCollection<Planet>> GetAllPlanetsAsync(string systemCode)
    {
        var collection = await _WebClientAdapter.GetPlanetsAsync(systemCode);
        return collection;
    }

    public virtual async Task<IReadOnlyCollection<Satellite>> GetAllSatellitesAsync(string systemCode)
    {
        var collection = await _WebClientAdapter.GetSatellitesAsync(systemCode);
        return collection;
    }

    public virtual async Task<IReadOnlyCollection<City>> GetAllCitiesAsync(string systemCode)
    {
        var collection = await _WebClientAdapter.GetCitiesAsync(systemCode);
        return collection;
    }

    public virtual async Task<IReadOnlyCollection<Tradeport>> GetAllTradeportsAsync(string systemCode)
    {
        var collection = await _WebClientAdapter.GetTradeportsAsync(systemCode);
        return collection;
    }

    public virtual async Task<Tradeport> GetTradeportAsync(string tradeportCode)
    {
        Tradeport tradeport = await _WebClientAdapter.GetTradeportAsync(tradeportCode);
        return tradeport;
    }

    public virtual async Task<IReadOnlyCollection<Commodity>> GetAllCommoditiesAsync()
    {
        var collection = await _WebClientAdapter.GetCommoditiesAsync();
        return collection;
    }
    public virtual async Task<PriceReportResponse> SubmitPriceReportAsync(PriceReport priceReport)
    {
        var response = await _WebClientAdapter.SubmitPriceReportAsync(priceReport);
        return response;
    }
    public virtual async Task<PriceReportsResponse> SubmitPriceReportsAsync(PriceReport[] priceReports)
    {
        var response = await _WebClientAdapter.SubmitPriceReportsAsync(priceReports);
        return response;
    }
}
