using UexCorpDataRunner.Domain.DataRunner;

namespace UexCorpDataRunner.Persistence.Api.Uex;
public interface IUexCorpWebApiClient
{
    Task<IList<Domain.DataRunner.System>> GetSystems();
}