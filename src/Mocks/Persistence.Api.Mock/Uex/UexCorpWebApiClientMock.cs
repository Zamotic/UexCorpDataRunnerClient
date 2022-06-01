using Moq;
using UexCorpDataRunner.Persistence.Api.Uex.DataTransferObjects;
using UexCorpDataRunner.Persistence.Api.Uex;

namespace UexCorpDataRunner.Persistence.Api.Mock.Uex;

public class UexCorpWebApiClientMock : Mock<IUexCorpWebApiClient>, IUexCorpWebApiClient
{
    public UexCorpWebApiClientMock()
    {
        ICollection<SystemDto> systemDtoList = new List<SystemDto>()
            {
                new SystemDto() { Name = "Pyro", Code = "PY", IsAvailable = false, IsDefault = false, DateAdded = new DateTime((long)1608949515 * 10000000), DateModified = DateTime.MinValue },
                new SystemDto() { Name = "Stanton", Code = "ST", IsAvailable = true, IsDefault = true, DateAdded = new DateTime((long)1608949515 * 10000000), DateModified = DateTime.MinValue }
            };
        this.Setup(s => s.GetSystemsAsync()).Returns(Task.FromResult(systemDtoList));

        ICollection<PlanetDto> planetDtoList = new List<PlanetDto>()
            {
                new PlanetDto() { System = "ST", Name = "ArcCorp", Code = "ARC", IsAvailable = true, DateAdded = new DateTime((long)1608949515 * 10000000), DateModified = DateTime.MinValue },
                new PlanetDto() { System = "ST", Name = "Crusader", Code = "CRU", IsAvailable = true, DateAdded = new DateTime((long)1608949515 * 10000000), DateModified = DateTime.MinValue },
                new PlanetDto() { System = "ST", Name = "Hurston", Code = "HUR", IsAvailable = true, DateAdded = new DateTime((long)1608949515 * 10000000), DateModified = DateTime.MinValue },
                new PlanetDto() { System = "ST", Name = "microTech", Code = "MIC", IsAvailable = true, DateAdded = new DateTime((long)1608949515 * 10000000), DateModified = DateTime.MinValue }
            };
        this.Setup(s => s.GetPlanetsAsync(It.Is((string s) => s.Equals("ST")))).Returns(Task.FromResult(planetDtoList));
    }

    public async Task<ICollection<SystemDto>> GetSystemsAsync()
    {
        return await this.Object.GetSystemsAsync();
    }

    public async Task<ICollection<PlanetDto>> GetPlanetsAsync(string systemCode)
    {
        return await this.Object.GetPlanetsAsync(systemCode);
    }
}
