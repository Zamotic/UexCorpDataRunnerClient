using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UexCorpDataRunner.Domain.DataRunner;
using UexCorpDataRunner.Persistence.Api.Uex.DataTransferObjects;

namespace UexCorpDataRunner.Persistence.Api.Uex;
public interface IUexCorpWebApiClientAdapter
{
    Task<GameVersion> GetCurrentVersionAsync();
    Task<IReadOnlyCollection<StarSystem>> GetSystemsAsync();
    Task<IReadOnlyCollection<Planet>> GetPlanetsAsync(string systemCode);
    Task<IReadOnlyCollection<Satellite>> GetSatellitesAsync(string systemCode);
    Task<IReadOnlyCollection<City>> GetCitiesAsync(string systemCode);
    Task<IReadOnlyCollection<Tradeport>> GetTradeportsAsync(string systemCode);
    Task<IReadOnlyCollection<Commodity>> GetCommoditiesAsync();
    Task<Tradeport> GetTradeportAsync(string tradeportCode);
    Task<PriceReportResponse> SubmitPriceReportAsync(Domain.DataRunner.PriceReport testPriceReport);
}
