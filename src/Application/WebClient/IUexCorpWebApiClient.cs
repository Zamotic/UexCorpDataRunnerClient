using System.Collections.Generic;
using System.Threading.Tasks;

namespace UexCorpDataRunner.Application.WebClient;
public interface IUexCorpWebApiClient
{
    Task<IList<Domain.Models.System>> GetSystems();
}