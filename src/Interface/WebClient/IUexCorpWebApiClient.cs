using System.Collections.Generic;
using System.Threading.Tasks;

namespace UexCorpDataRunner.Application.WebClient;
public interface IUexCorpWebApiClientAdapter
{
    Task<IList<Domain.DataRunner.System>> GetSystems();
}