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

    //public virtual async Task<IReadOnlyCollection<Planet>> GetAllPlanetsAsync(string systemCode)
    //{
    //    var collection = await _WebClientAdapter.GetPlanetsAsync(systemCode);
    //    return collection;
    //}

    //public virtual async Task<IReadOnlyCollection<Satellite>> GetAllSatellitesAsync(string systemCode)
    //{
    //    var collection = await _WebClientAdapter.GetSatellitesAsync(systemCode);
    //    return collection;
    //}

    //public virtual async Task<IReadOnlyCollection<City>> GetAllCitiesAsync(string systemCode)
    //{
    //    var collection = await _WebClientAdapter.GetCitiesAsync(systemCode);
    //    return collection;
    //}

    //public virtual async Task<IReadOnlyCollection<Tradeport>> GetAllTradeportsAsync(string systemCode)
    //{
    //    var collection = await _WebClientAdapter.GetTradeportsAsync(systemCode);
    //    return collection;
    //}

    //public virtual async Task<Tradeport> GetTradeportAsync(string tradeportCode)
    //{
    //    Tradeport tradeport = await _WebClientAdapter.GetTradeportAsync(tradeportCode);
    //    return tradeport;
    //}

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

    //public virtual async Task<PriceReportResponse> SubmitPriceReportAsync(PriceReport priceReport)
    //{
    //    var response = await _WebClientAdapter.SubmitPriceReportAsync(priceReport);
    //    return response;
    //}
    //public virtual async Task<PriceReportsResponse> SubmitPriceReportsAsync(PriceReport[] priceReports)
    //{
    //    var response = await _WebClientAdapter.SubmitPriceReportsAsync(priceReports);
    //    return response;
    //}
}
