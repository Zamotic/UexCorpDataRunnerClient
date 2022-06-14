using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UexCorpDataRunner.Domain.DataRunner;

namespace UexCorpDataRunner.Persistence.Api.Uex;
public interface IUexCorpWebApiClientAdapter
{
    Task<IReadOnlyCollection<Domain.DataRunner.System>> GetSystemsAsync();
    Task<IReadOnlyCollection<Domain.DataRunner.Planet>> GetPlanetsAsync(string systemCode);
    Task<IReadOnlyCollection<Domain.DataRunner.Satellite>> GetSatellitesAsync(string systemCode);
    Task<IReadOnlyCollection<Domain.DataRunner.City>> GetCitiesAsync(string systemCode);
    Task<IReadOnlyCollection<Domain.DataRunner.Tradeport>> GetTradeportsAsync(string systemCode);
    Task<IReadOnlyCollection<Domain.DataRunner.Commodity>> GetCommoditiesAsync();
    Task<Tradeport> GetTradeportAsync(string tradeportCode);
    Task<Domain.DataRunner.PriceReportResponse> SubmitPriceReportAsync(Domain.DataRunner.PriceReport testPriceReport);
}
