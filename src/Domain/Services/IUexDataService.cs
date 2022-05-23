using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UexCorpDataRunner.Domain.Models;

namespace UexCorpDataRunner.Domain.Services;
public interface IUexDataService
{
    Task<IReadOnlyList<Models.System>> GetAllSystemsAsync();
    Task<IReadOnlyList<Planet>> GetAllPlanetsAsync(string systemCode);
    Task<IReadOnlyList<Satellite>> GetAllSatellitesAsync(string systemCode);
    Task<IReadOnlyList<City>> GetAllCitiesAsync(string systemCode);
    Task<IReadOnlyList<Tradeport>> GetAllTradeportsAsync(string systemCode);
    Task<IReadOnlyList<Commodity>> GetAllCommoditiesAsync();
}
