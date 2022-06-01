using UexCorpDataRunner.Persistence.Api.Uex.DataTransferObjects;

namespace UexCorpDataRunner.Persistence.Api.Uex;
public interface IUexCorpWebApiClient
{
    Task<ICollection<SystemDto>> GetSystemsAsync();
    Task<ICollection<PlanetDto>> GetPlanetsAsync(string systemCode);
}