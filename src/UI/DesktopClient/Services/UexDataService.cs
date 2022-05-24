using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UexCorpDataRunner.Domain.Models;
using UexCorpDataRunner.Domain.Services;

namespace UexCorpDataRunner.DesktopClient.Services;
public class UexDataService : IUexDataService
{
    public Task<IReadOnlyList<City>> GetAllCitiesAsync(string systemCode)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<Commodity>> GetAllCommoditiesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<Planet>> GetAllPlanetsAsync(string systemCode)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<Satellite>> GetAllSatellitesAsync(string systemCode)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<Domain.Models.System>> GetAllSystemsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<Tradeport>> GetAllTradeportsAsync(string systemCode)
    {
        throw new NotImplementedException();
    }
}
