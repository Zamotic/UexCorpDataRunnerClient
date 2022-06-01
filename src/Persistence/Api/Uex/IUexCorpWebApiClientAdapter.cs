using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UexCorpDataRunner.Persistence.Api.Uex;
public interface IUexCorpWebApiClientAdapter
{
    public Task<IReadOnlyCollection<Domain.DataRunner.System>> GetSystemsAsync();
    public Task<IReadOnlyCollection<Domain.DataRunner.Planet>> GetPlanetsAsync(string systemCode);
}
