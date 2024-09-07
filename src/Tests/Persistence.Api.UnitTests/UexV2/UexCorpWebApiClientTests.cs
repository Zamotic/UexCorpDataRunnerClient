using NSubstitute;
using UexCorpDataRunner.Domain.Services;
using UexCorpDataRunner.Persistence.Api.Mock.UexV2;
using UexCorpDataRunner.Persistence.Api.UexV2;
using UexCorpDataRunner.Persistence.Api.UexV2.DataTransferObjects;

namespace Persistence.Api.UnitTests.UexV2;
public class UexCorpWebApiClientTests
{
    DateTimeOffset _dateAdded = new DateTimeOffset(2020, 12, 26, 2, 25, 15, TimeSpan.Zero);
    DateTimeOffset _dateModified = new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero);

    IUexCorpWebApiClient _webApiClient;
    UexWebApiMockHttpMessageHandler _uexWebApiMockHttpMessageHandler;

    public UexCorpWebApiClientTests()
    {
        ISettingsService substituteSettingsService = Substitute.For<ISettingsService>();
        IUexCorpWebApiConfiguration substituteWebConfiguration = Substitute.For<IUexCorpWebApiConfiguration>();
        substituteWebConfiguration.DataRunnerEndpointPath.Returns(string.Empty);
        substituteWebConfiguration.WebApiEndPointUrl.Returns("https://ptu.uexcorp.space/api/");
        substituteWebConfiguration.ApiKey.Returns("tFzGU35mHdBZVBVO9TMR/muwuHz8P7TimgK66fSj1wrBoCUsEL7ea9TVuJGakVvQ");

        _uexWebApiMockHttpMessageHandler = new UexWebApiMockHttpMessageHandler();
        HttpClient mockedHttpClient = new HttpClient(_uexWebApiMockHttpMessageHandler);
        _webApiClient = new UexCorpWebApiClient(substituteWebConfiguration, mockedHttpClient, substituteSettingsService);
    }

    #region     GameVersion
    [Fact]
    public async Task GetCurrentGameVersionAsync_ShouldHaveExpectedLiveValue()
    {
        // Assemble
        const string ExpectedValue = "3.22";

        // Act
        var actual = await _webApiClient.GetCurrentVersionAsync().ConfigureAwait(false);

        // Assert
        actual.Live.Should().Be(ExpectedValue);
    }
    [Fact]
    public async Task GetCurrentGameVersionAsync_ShouldHaveExpectedPtuValue()
    {
        // Assemble
        const string ExpectedValue = "3.22.1";

        // Act
        var actual = await _webApiClient.GetCurrentVersionAsync().ConfigureAwait(false);

        // Assert
        actual.Ptu.Should().Be(ExpectedValue);
    }
    #endregion  GameVersion

    #region     System

    [Fact]
    public async Task GetSystemsAsync_ShouldHaveExpectedCountOf2()
    {
        // Assemble

        // Act
        var actual = await _webApiClient.GetSystemsAsync().ConfigureAwait(false);

        // Assert
        actual.Should().HaveCount(2);
    }

    private static ICollection<StarSystemDto> _ActualGetSystemsAsyncValue = new List<StarSystemDto>();
    private async Task<ICollection<StarSystemDto>> GetActualSystemsAsyncValue()
    {
        if (_ActualGetSystemsAsyncValue.Any() == false)
        {
            _ActualGetSystemsAsyncValue = await _webApiClient.GetSystemsAsync().ConfigureAwait(false);
        }

        return _ActualGetSystemsAsyncValue;
    }

    [Fact]
    public async Task GetSystemsAsync_ShouldHaveExpectedId()
    {
        // Assemble
        const int ExpectedValue = 64;

        // Act
        var systems = await GetActualSystemsAsyncValue().ConfigureAwait(false);

        // Assert
        systems.Should().NotBeNull();
        var actual = systems.First();
        actual.Id.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetSystemsAsync_ShouldHaveExpectedName()
    {
        // Assemble
        const string ExpectedValue = "Pyro";

        // Act
        var systems = await GetActualSystemsAsyncValue().ConfigureAwait(false);

        // Assert
        systems.Should().NotBeNull();
        var actual = systems.First();
        actual.Name.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetSystemsAsync_ShouldHaveExpectedCode()
    {
        // Assemble
        const string ExpectedValue = "PY";

        // Act
        var systems = await GetActualSystemsAsyncValue().ConfigureAwait(false);

        // Assert
        systems.Should().NotBeNull();
        var actual = systems.First();
        actual.Code.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetSystemsAsync_ShouldHaveExpectedIsAvailable()
    {
        // Assemble
        const bool ExpectedValue = false;

        // Act
        var systems = await GetActualSystemsAsyncValue().ConfigureAwait(false);

        // Assert
        systems.Should().NotBeNull();
        var actual = systems.First();
        actual.IsAvailable.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetSystemsAsync_ShouldHaveExpectedIsVisible()
    {
        // Assemble
        const bool ExpectedValue = false;

        // Act
        var systems = await GetActualSystemsAsyncValue().ConfigureAwait(false);

        // Assert
        systems.Should().NotBeNull();
        var actual = systems.First();
        actual.IsVisible.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetSystemsAsync_ShouldHaveExpectedIsDefault()
    {
        // Assemble
        const bool ExpectedValue = false;

        // Act
        var systems = await GetActualSystemsAsyncValue().ConfigureAwait(false);

        // Assert
        systems.Should().NotBeNull();
        var actual = systems.First();
        actual.IsDefault.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetSystemsAsync_ShouldHaveExpectedDateAdded()
    {
        // Assemble
        DateTimeOffset ExpectedValue = _dateAdded;

        // Act
        var systems = await GetActualSystemsAsyncValue().ConfigureAwait(false);

        // Assert
        systems.Should().NotBeNull();
        var actual = systems.First();
        actual.DateAdded.Should().NotBe(DateTimeOffset.MinValue);
    }

    [Fact]
    public async Task GetSystemsAsync_ShouldHaveExpectedDateModified()
    {
        // Assemble
        DateTimeOffset ExpectedValue = _dateModified;

        // Act
        var systems = await GetActualSystemsAsyncValue().ConfigureAwait(false);

        // Assert
        systems.Should().NotBeNull();
        var actual = systems.First();
        actual.DateAdded.Should().NotBe(DateTimeOffset.MinValue);
    }

    #endregion  System

    #region     Planet
    [Fact]
    public async Task GetPlanetsAsync_ShouldHaveExpectedCountOf25()
    {
        // Assemble
        const int starSystemId = 68;

        // Act
        var actual = await _webApiClient.GetPlanetsAsync(starSystemId).ConfigureAwait(false);

        // Assert
        actual.Should().HaveCount(25);
    }

    private static ICollection<PlanetDto> _ActualGetPlanetsAsyncValue = new List<PlanetDto>();
    private async Task<ICollection<PlanetDto>> GetActualPlanetsAsyncValue()
    {
        const int starSystemId = 68;
        if (_ActualGetPlanetsAsyncValue.Any() == false)
        {
            _ActualGetPlanetsAsyncValue = await _webApiClient.GetPlanetsAsync(starSystemId).ConfigureAwait(false);
        }

        return _ActualGetPlanetsAsyncValue;
    }

    [Fact]
    public async Task GetPlanetsAsync_ShouldHaveExpectedId()
    {
        // Assemble
        const int ExpectedId = 4;

        // Act
        var planets = await GetActualPlanetsAsyncValue().ConfigureAwait(false);

        // Assert
        planets.Should().NotBeNull();
        var actual = planets.First();
        actual.Id.Should().Be(ExpectedId);
    }

    [Fact]
    public async Task GetPlanetsAsync_ShouldHaveExpectedStarSystemId()
    {
        // Assemble
        const int ExpectedStarSystemId = 68;

        // Act
        var planets = await GetActualPlanetsAsyncValue().ConfigureAwait(false);

        // Assert
        planets.Should().NotBeNull();
        var actual = planets.First();
        actual.StarSystemId.Should().Be(ExpectedStarSystemId);
    }

    [Fact]
    public async Task GetPlanetsAsync_ShouldHaveExpectedName()
    {
        // Assemble
        const string ExpectedValue = "ArcCorp";

        // Act
        var planets = await GetActualPlanetsAsyncValue().ConfigureAwait(false);

        // Assert
        planets.Should().NotBeNull();
        var actual = planets.First();
        actual.Name.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetPlanetsAsync_ShouldHaveExpectedCode()
    {
        // Assemble
        const string ExpectedValue = "ARC";

        // Act
        var planets = await GetActualPlanetsAsyncValue().ConfigureAwait(false);

        // Assert
        planets.Should().NotBeNull();
        var actual = planets.First();
        actual.Code.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetPlanetsAsync_ShouldHaveExpectedIsAvailable()
    {
        // Assemble
        const bool ExpectedValue = true;

        // Act
        var planets = await GetActualPlanetsAsyncValue().ConfigureAwait(false);

        // Assert
        planets.Should().NotBeNull();
        var actual = planets.First();
        actual.IsAvailable.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetPlanetsAsync_ShouldHaveExpectedIsVisible()
    {
        // Assemble
        const bool ExpectedValue = true;

        // Act
        var planets = await GetActualPlanetsAsyncValue().ConfigureAwait(false);

        // Assert
        planets.Should().NotBeNull();
        var actual = planets.First();
        actual.IsVisible.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetPlanetsAsync_ShouldHaveExpectedDateAdded()
    {
        // Assemble
        DateTimeOffset ExpectedValue = _dateAdded;

        // Act
        var planets = await GetActualPlanetsAsyncValue().ConfigureAwait(false);

        // Assert
        planets.Should().NotBeNull();
        var actual = planets.First();
        actual.DateAdded.Should().NotBe(DateTimeOffset.MinValue);
    }

    [Fact]
    public async Task GetPlanetsAsync_ShouldHaveExpectedDateModified()
    {
        // Assemble
        DateTimeOffset ExpectedValue = _dateModified;

        // Act
        var planets = await GetActualPlanetsAsyncValue().ConfigureAwait(false);

        // Assert
        planets.Should().NotBeNull();
        var actual = planets.First();
        actual.DateModified.Should().NotBe(DateTimeOffset.MinValue);
    }

    #endregion  Planet

    #region     Moon

    [Fact]
    public async Task GetMoonsBySystemAsync_ShouldHaveExpectedCountOf12()
    {
        // Assemble
        const int starSystemId = 68;

        // Act
        var actual = await _webApiClient.GetMoonsByStarSystemIdAsync(starSystemId).ConfigureAwait(false);

        // Assert
        actual.Should().HaveCount(12);
    }

    [Fact]
    public async Task GetMoonsByPlanetAsync_ShouldHaveExpectedCount()
    {
        // Assemble
        //const int starSystemId = 68;
        const int planetId = 4;
        const int ExpectedCount = 2;

        // Act
        var actual = await _webApiClient.GetMoonsByPlanetIdAsync(planetId).ConfigureAwait(false);

        // Assert
        actual.Should().HaveCount(ExpectedCount);
    }

    private static ICollection<MoonDto> _ActualGetMoonsAsyncValue = new List<MoonDto>();
    private async Task<ICollection<MoonDto>> GetActualMoonsAsyncValue()
    {
        const int starSystemId = 68;
        if (_ActualGetMoonsAsyncValue.Any() == false)
        {
            _ActualGetMoonsAsyncValue = await _webApiClient.GetMoonsByStarSystemIdAsync(starSystemId).ConfigureAwait(false);
        }

        return _ActualGetMoonsAsyncValue;
    }

    [Fact]
    public async Task GetMoonsAsync_ShouldHaveExpectedId()
    {
        // Assemble
        const int ExpectedId = 1;

        // Act
        var moons = await GetActualMoonsAsyncValue().ConfigureAwait(false);

        // Assert
        moons.Should().NotBeNull();
        var actual = moons.First();
        actual.Id.Should().Be(ExpectedId);
    }

    [Fact]
    public async Task GetMoonsAsync_ShouldHaveExpectedStarSystemId()
    {
        // Assemble
        const int ExpectedStarSystemId = 68;

        // Act
        var moons = await GetActualMoonsAsyncValue().ConfigureAwait(false);

        // Assert
        moons.Should().NotBeNull();
        var actual = moons.First();
        actual.StarSystemId.Should().Be(ExpectedStarSystemId);
    }

    [Fact]
    public async Task GetMoonsAsync_ShouldHaveExpectedPlanetId()
    {
        // Assemble
        const int ExpectedPlanetId = 116;

        // Act
        var moons = await GetActualMoonsAsyncValue().ConfigureAwait(false);

        // Assert
        moons.Should().NotBeNull();
        var actual = moons.First();
        actual.PlanetId.Should().Be(ExpectedPlanetId);
    }

    [Fact]
    public async Task GetMoonsAsync_ShouldHaveExpectedName()
    {
        // Assemble
        const string ExpectedValue = "Aberdeen";

        // Act
        var moons = await GetActualMoonsAsyncValue().ConfigureAwait(false);

        // Assert
        moons.Should().NotBeNull();
        var actual = moons.First();
        actual.Name.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetMoonsAsync_ShouldHaveExpectedNameOrigin()
    {
        // Assemble
        const string ExpectedValue = "Stanton I b";

        // Act
        var moons = await GetActualMoonsAsyncValue().ConfigureAwait(false);

        // Assert
        moons.Should().NotBeNull();
        var actual = moons.First();
        actual.NameOrigin.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetMoonsAsync_ShouldHaveExpectedCode()
    {
        // Assemble
        const string ExpectedValue = "ABE";

        // Act
        var moons = await GetActualMoonsAsyncValue().ConfigureAwait(false);

        // Assert
        moons.Should().NotBeNull();
        var actual = moons.First();
        actual.Code.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetMoonsAsync_ShouldHaveExpectedIsAvailable()
    {
        // Assemble
        const bool ExpectedValue = true;

        // Act
        var moons = await GetActualMoonsAsyncValue().ConfigureAwait(false);

        // Assert
        moons.Should().NotBeNull();
        var actual = moons.First();
        actual.IsAvailable.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetMoonsAsync_ShouldHaveExpectedIsVisible()
    {
        // Assemble
        const bool ExpectedValue = true;

        // Act
        var moons = await GetActualMoonsAsyncValue().ConfigureAwait(false);

        // Assert
        moons.Should().NotBeNull();
        var actual = moons.First();
        actual.IsVisible.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetMoonsAsync_ShouldHaveExpectedDateAdded()
    {
        // Assemble
        DateTimeOffset ExpectedValue = _dateAdded;

        // Act
        var moons = await GetActualMoonsAsyncValue().ConfigureAwait(false);

        // Assert
        moons.Should().NotBeNull();
        var actual = moons.First();
        actual.DateAdded.Should().NotBe(DateTimeOffset.MinValue);
    }

    [Fact]
    public async Task GetMoonsAsync_ShouldHaveExpectedDateModified()
    {
        // Assemble
        DateTimeOffset ExpectedValue = _dateModified;

        // Act
        var moons = await GetActualMoonsAsyncValue().ConfigureAwait(false);

        // Assert
        moons.Should().NotBeNull();
        var actual = moons.First();
        actual.DateModified.Should().NotBe(DateTimeOffset.MinValue);
    }

    #endregion  Moon

    #region     City

    [Fact]
    public async Task GetCitiesAsync_ShouldHaveExpectedCountOf4()
    {
        // Assemble
        const int starSystemId = 68;

        // Act
        var actual = await _webApiClient.GetCitiesByStarSystemIdAsync(starSystemId).ConfigureAwait(false);

        // Assert
        actual.Should().HaveCount(4);
    }

    private static ICollection<CityDto> _ActualGetCitiesAsyncValue = new List<CityDto>();
    private async Task<ICollection<CityDto>> GetActualCitiesAsyncValue()
    {
        const int starSystemId = 68;
        if (_ActualGetCitiesAsyncValue.Any() == false)
        {
            _ActualGetCitiesAsyncValue = await _webApiClient.GetCitiesByStarSystemIdAsync(starSystemId).ConfigureAwait(false);
        }

        return _ActualGetCitiesAsyncValue;
    }

    [Fact]
    public async Task GetCitiesAsync_ShouldHaveExpectedId()
    {
        // Assemble
        const int ExpectedId = 1;

        // Act
        var cities = await GetActualCitiesAsyncValue().ConfigureAwait(false);

        // Assert
        cities.Should().NotBeNull();
        var actual = cities.First();
        actual.Id.Should().Be(ExpectedId);
    }

    [Fact]
    public async Task GetCitiesAsync_ShouldHaveExpectedStarSystemId()
    {
        // Assemble
        const int ExpectedStarSystemId = 68;

        // Act
        var cities = await GetActualCitiesAsyncValue().ConfigureAwait(false);

        // Assert
        cities.Should().NotBeNull();
        var actual = cities.First();
        actual.StarSystemId.Should().Be(ExpectedStarSystemId);
    }

    [Fact]
    public async Task GetCitiesAsync_ShouldHaveExpectedPlanetId()
    {
        // Assemble
        const int ExpectedPlanetId = 4;

        // Act
        var cities = await GetActualCitiesAsyncValue().ConfigureAwait(false);

        // Assert
        cities.Should().NotBeNull();
        var actual = cities.First();
        actual.PlanetId.Should().Be(ExpectedPlanetId);
    }

    [Fact]
    public async Task GetCitiesAsync_ShouldHaveExpectedMoonId()
    {
        // Assemble
        const int ExpectedMoonId = 0;

        // Act
        var cities = await GetActualCitiesAsyncValue().ConfigureAwait(false);

        // Assert
        cities.Should().NotBeNull();
        var actual = cities.First();
        actual.MoonId.Should().Be(ExpectedMoonId);
    }

    [Fact]
    public async Task GetCitiesAsync_ShouldHaveExpectedName()
    {
        // Assemble
        const string ExpectedValue = "Area 18";

        // Act
        var cities = await GetActualCitiesAsyncValue().ConfigureAwait(false);

        // Assert
        cities.Should().NotBeNull();
        var actual = cities.First();
        actual.Name.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCitiesAsync_ShouldHaveExpectedCode()
    {
        // Assemble
        const string ExpectedValue = "AR18";

        // Act
        var cities = await GetActualCitiesAsyncValue().ConfigureAwait(false);

        // Assert
        cities.Should().NotBeNull();
        var actual = cities.First();
        actual.Code.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCitiesAsync_ShouldHaveExpectedIsAvailable()
    {
        // Assemble
        const bool ExpectedValue = true;

        // Act
        var cities = await GetActualCitiesAsyncValue().ConfigureAwait(false);

        // Assert
        cities.Should().NotBeNull();
        var actual = cities.First();
        actual.IsAvailable.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCitiesAsync_ShouldHaveExpectedIsVisible()
    {
        // Assemble
        const bool ExpectedValue = true;

        // Act
        var cities = await GetActualCitiesAsyncValue().ConfigureAwait(false);

        // Assert
        cities.Should().NotBeNull();
        var actual = cities.First();
        actual.IsVisible.Should().Be(ExpectedValue);
    }

    //[Fact]
    //public async Task GetCitiesAsync_ShouldHaveExpectedIsMonitored()
    //{
    //    // Assemble
    //    const bool ExpectedValue = true;

    //    // Act
    //    var cities = await GetActualCitiesAsyncValue().ConfigureAwait(false);

    //    // Assert
    //    cities.Should().NotBeNull();
    //    var actual = cities.First();
    //    actual.IsMonitored.Should().Be(ExpectedValue);
    //}

    //[Fact]
    //public async Task GetCitiesAsync_ShouldHaveExpectedIsArmistice()
    //{
    //    // Assemble
    //    const bool ExpectedValue = true;

    //    // Act
    //    var cities = await GetActualCitiesAsyncValue().ConfigureAwait(false);

    //    // Assert
    //    cities.Should().NotBeNull();
    //    var actual = cities.First();
    //    actual.IsArmistice.Should().Be(ExpectedValue);
    //}

    //[Fact]
    //public async Task GetCitiesAsync_ShouldHaveExpectedIsLandable()
    //{
    //    // Assemble
    //    const bool ExpectedValue = true;

    //    // Act
    //    var cities = await GetActualCitiesAsyncValue().ConfigureAwait(false);

    //    // Assert
    //    cities.Should().NotBeNull();
    //    var actual = cities.First();
    //    actual.IsLandable.Should().Be(ExpectedValue);
    //}

    //[Fact]
    //public async Task GetCitiesAsync_ShouldHaveExpectedIsDecomissioned()
    //{
    //    // Assemble
    //    const bool ExpectedValue = false;

    //    // Act
    //    var cities = await GetActualCitiesAsyncValue().ConfigureAwait(false);

    //    // Assert
    //    cities.Should().NotBeNull();
    //    var actual = cities.First();
    //    actual.IsDecomissioned.Should().Be(ExpectedValue);
    //}

    //[Fact]
    //public async Task GetCitiesAsync_ShouldHaveExpectedHasQuantumMarker()
    //{
    //    // Assemble
    //    const bool ExpectedValue = true;

    //    // Act
    //    var cities = await GetActualCitiesAsyncValue().ConfigureAwait(false);

    //    // Assert
    //    cities.Should().NotBeNull();
    //    var actual = cities.First();
    //    actual.HasQuantumMarker.Should().Be(ExpectedValue);
    //}

    [Fact]
    public async Task GetCitiesAsync_ShouldHaveExpectedHasTradeTerminal()
    {
        // Assemble
        const bool ExpectedValue = true;

        // Act
        var cities = await GetActualCitiesAsyncValue().ConfigureAwait(false);

        // Assert
        cities.Should().NotBeNull();
        var actual = cities.First();
        actual.HasTradeTerminal.Should().Be(ExpectedValue);
    }

    //[Fact]
    //public async Task GetCitiesAsync_ShouldHaveExpectedHasHabitation()
    //{
    //    // Assemble
    //    const bool ExpectedValue = true;

    //    // Act
    //    var cities = await GetActualCitiesAsyncValue().ConfigureAwait(false);

    //    // Assert
    //    cities.Should().NotBeNull();
    //    var actual = cities.First();
    //    actual.HasHabitation.Should().Be(ExpectedValue);
    //}

    //[Fact]
    //public async Task GetCitiesAsync_ShouldHaveExpectedHasRefinery()
    //{
    //    // Assemble
    //    const bool ExpectedValue = false;

    //    // Act
    //    var cities = await GetActualCitiesAsyncValue().ConfigureAwait(false);

    //    // Assert
    //    cities.Should().NotBeNull();
    //    var actual = cities.First();
    //    actual.HasRefinery.Should().Be(ExpectedValue);
    //}

    //[Fact]
    //public async Task GetCitiesAsync_ShouldHaveExpectedHasCargoCenter()
    //{
    //    // Assemble
    //    const bool ExpectedValue = false;

    //    // Act
    //    var cities = await GetActualCitiesAsyncValue().ConfigureAwait(false);

    //    // Assert
    //    cities.Should().NotBeNull();
    //    var actual = cities.First();
    //    actual.HasCargoCenter.Should().Be(ExpectedValue);
    //}

    //[Fact]
    //public async Task GetCitiesAsync_ShouldHaveExpectedHasClinic()
    //{
    //    // Assemble
    //    const bool ExpectedValue = true;

    //    // Act
    //    var cities = await GetActualCitiesAsyncValue().ConfigureAwait(false);

    //    // Assert
    //    cities.Should().NotBeNull();
    //    var actual = cities.First();
    //    actual.HasClinic.Should().Be(ExpectedValue);
    //}

    //[Fact]
    //public async Task GetCitiesAsync_ShouldHaveExpectedHasFood()
    //{
    //    // Assemble
    //    const bool ExpectedValue = true;

    //    // Act
    //    var cities = await GetActualCitiesAsyncValue().ConfigureAwait(false);

    //    // Assert
    //    cities.Should().NotBeNull();
    //    var actual = cities.First();
    //    actual.HasFood.Should().Be(ExpectedValue);
    //}

    [Fact]
    public async Task GetCitiesAsync_ShouldHaveExpectedHasShops()
    {
        // Assemble
        const bool ExpectedValue = true;

        // Act
        var cities = await GetActualCitiesAsyncValue().ConfigureAwait(false);

        // Assert
        cities.Should().NotBeNull();
        var actual = cities.First();
        actual.HasShops.Should().Be(ExpectedValue);
    }

    //[Fact]
    //public async Task GetCitiesAsync_ShouldHaveExpectedHasRefuel()
    //{
    //    // Assemble
    //    const bool ExpectedValue = true;

    //    // Act
    //    var cities = await GetActualCitiesAsyncValue().ConfigureAwait(false);

    //    // Assert
    //    cities.Should().NotBeNull();
    //    var actual = cities.First();
    //    actual.HasRefuel.Should().Be(ExpectedValue);
    //}

    //[Fact]
    //public async Task GetCitiesAsync_ShouldHaveExpectedHasRepair()
    //{
    //    // Assemble
    //    const bool ExpectedValue = true;

    //    // Act
    //    var cities = await GetActualCitiesAsyncValue().ConfigureAwait(false);

    //    // Assert
    //    cities.Should().NotBeNull();
    //    var actual = cities.First();
    //    actual.HasRepair.Should().Be(ExpectedValue);
    //}

    //[Fact]
    //public async Task GetCitiesAsync_ShouldHaveExpectedHasGravity()
    //{
    //    // Assemble
    //    const bool ExpectedValue = true;

    //    // Act
    //    var cities = await GetActualCitiesAsyncValue().ConfigureAwait(false);

    //    // Assert
    //    cities.Should().NotBeNull();
    //    var actual = cities.First();
    //    actual.HasGravity.Should().Be(ExpectedValue);
    //}

    [Fact]
    public async Task GetCitiesAsync_ShouldHaveExpectedDateAdded()
    {
        // Assemble
        DateTimeOffset ExpectedValue = _dateAdded;

        // Act
        var cities = await GetActualCitiesAsyncValue().ConfigureAwait(false);

        // Assert
        cities.Should().NotBeNull();
        var actual = cities.First();
        actual.DateAdded.Should().NotBe(DateTimeOffset.MinValue);
    }

    [Fact]
    public async Task GetCitiesAsync_ShouldHaveExpectedDateModified()
    {
        // Assemble
        DateTimeOffset ExpectedValue = _dateModified;

        // Act
        var cities = await GetActualCitiesAsyncValue().ConfigureAwait(false);

        // Assert
        cities.Should().NotBeNull();
        var actual = cities.First();
        actual.DateModified.Should().NotBe(DateTimeOffset.MinValue);
    }

    #endregion  City

    #region     Outpost

    [Fact]
    public async Task GetOutpostsAsync_ShouldHaveExpectedCount()
    {
        // Assemble
        const int starSystemId = 68;
        const int ExpectedCount = 67;

        // Act
        var actual = await _webApiClient.GetOutpostsByStarSystemIdAsync(starSystemId).ConfigureAwait(false);

        // Assert
        actual.Should().HaveCount(ExpectedCount);
    }

    private static ICollection<OutpostDto> _ActualGetOutpostsAsyncValue = new List<OutpostDto>();
    private async Task<ICollection<OutpostDto>> GetActualOutpostsAsyncValue()
    {
        const int starSystemId = 68;
        if (_ActualGetOutpostsAsyncValue.Any() == false)
        {
            _ActualGetOutpostsAsyncValue = await _webApiClient.GetOutpostsByStarSystemIdAsync(starSystemId).ConfigureAwait(false);
        }

        return _ActualGetOutpostsAsyncValue;
    }

    [Fact]
    public async Task GetOutpostsAsync_ShouldHaveExpectedId()
    {
        // Assemble
        const int ExpectedId = 1;

        // Act
        var cities = await GetActualOutpostsAsyncValue().ConfigureAwait(false);

        // Assert
        cities.Should().NotBeNull();
        var actual = cities.First();
        actual.Id.Should().Be(ExpectedId);
    }

    [Fact]
    public async Task GetOutpostsAsync_ShouldHaveExpectedStarSystemId()
    {
        // Assemble
        const int ExpectedStarSystemId = 68;

        // Act
        var cities = await GetActualOutpostsAsyncValue().ConfigureAwait(false);

        // Assert
        cities.Should().NotBeNull();
        var actual = cities.First();
        actual.StarSystemId.Should().Be(ExpectedStarSystemId);
    }

    [Fact]
    public async Task GetOutpostsAsync_ShouldHaveExpectedPlanetId()
    {
        // Assemble
        const int ExpectedPlanetId = 4;

        // Act
        var cities = await GetActualOutpostsAsyncValue().ConfigureAwait(false);

        // Assert
        cities.Should().NotBeNull();
        var actual = cities.First();
        actual.PlanetId.Should().Be(ExpectedPlanetId);
    }

    [Fact]
    public async Task GetOutpostsAsync_ShouldHaveExpectedMoonId()
    {
        // Assemble
        const int ExpectedMoonId = 74;

        // Act
        var cities = await GetActualOutpostsAsyncValue().ConfigureAwait(false);

        // Assert
        cities.Should().NotBeNull();
        var actual = cities.First();
        actual.MoonId.Should().Be(ExpectedMoonId);
    }

    [Fact]
    public async Task GetOutpostsAsync_ShouldHaveExpectedName()
    {
        // Assemble
        const string ExpectedValue = "ArcCorp Mining Area 045";

        // Act
        var cities = await GetActualOutpostsAsyncValue().ConfigureAwait(false);

        // Assert
        cities.Should().NotBeNull();
        var actual = cities.First();
        actual.Name.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetOutpostsAsync_ShouldHaveExpectedIsAvailable()
    {
        // Assemble
        const bool ExpectedValue = true;

        // Act
        var cities = await GetActualOutpostsAsyncValue().ConfigureAwait(false);

        // Assert
        cities.Should().NotBeNull();
        var actual = cities.First();
        actual.IsAvailable.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetOutpostsAsync_ShouldHaveExpectedIsVisible()
    {
        // Assemble
        const bool ExpectedValue = true;

        // Act
        var cities = await GetActualOutpostsAsyncValue().ConfigureAwait(false);

        // Assert
        cities.Should().NotBeNull();
        var actual = cities.First();
        actual.IsVisible.Should().Be(ExpectedValue);
    }

    //[Fact]
    //public async Task GetOutpostsAsync_ShouldHaveExpectedIsMonitored()
    //{
    //    // Assemble
    //    const bool ExpectedValue = true;

    //    // Act
    //    var cities = await GetActualOutpostsAsyncValue().ConfigureAwait(false);

    //    // Assert
    //    cities.Should().NotBeNull();
    //    var actual = cities.First();
    //    actual.IsMonitored.Should().Be(ExpectedValue);
    //}

    //[Fact]
    //public async Task GetOutpostsAsync_ShouldHaveExpectedIsArmistice()
    //{
    //    // Assemble
    //    const bool ExpectedValue = true;

    //    // Act
    //    var cities = await GetActualOutpostsAsyncValue().ConfigureAwait(false);

    //    // Assert
    //    cities.Should().NotBeNull();
    //    var actual = cities.First();
    //    actual.IsArmistice.Should().Be(ExpectedValue);
    //}

    //[Fact]
    //public async Task GetOutpostsAsync_ShouldHaveExpectedIsLandable()
    //{
    //    // Assemble
    //    const bool ExpectedValue = true;

    //    // Act
    //    var cities = await GetActualOutpostsAsyncValue().ConfigureAwait(false);

    //    // Assert
    //    cities.Should().NotBeNull();
    //    var actual = cities.First();
    //    actual.IsLandable.Should().Be(ExpectedValue);
    //}

    //[Fact]
    //public async Task GetOutpostsAsync_ShouldHaveExpectedIsDecomissioned()
    //{
    //    // Assemble
    //    const bool ExpectedValue = false;

    //    // Act
    //    var cities = await GetActualOutpostsAsyncValue().ConfigureAwait(false);

    //    // Assert
    //    cities.Should().NotBeNull();
    //    var actual = cities.First();
    //    actual.IsDecomissioned.Should().Be(ExpectedValue);
    //}

    //[Fact]
    //public async Task GetOutpostsAsync_ShouldHaveExpectedHasQuantumMarker()
    //{
    //    // Assemble
    //    const bool ExpectedValue = true;

    //    // Act
    //    var cities = await GetActualOutpostsAsyncValue().ConfigureAwait(false);

    //    // Assert
    //    cities.Should().NotBeNull();
    //    var actual = cities.First();
    //    actual.HasQuantumMarker.Should().Be(ExpectedValue);
    //}

    [Fact]
    public async Task GetOutpostsAsync_ShouldHaveExpectedHasTradeTerminal()
    {
        // Assemble
        const bool ExpectedValue = true;

        // Act
        var cities = await GetActualOutpostsAsyncValue().ConfigureAwait(false);

        // Assert
        cities.Should().NotBeNull();
        var actual = cities.First();
        actual.HasTradeTerminal.Should().Be(ExpectedValue);
    }

    //[Fact]
    //public async Task GetOutpostsAsync_ShouldHaveExpectedHasHabitation()
    //{
    //    // Assemble
    //    const bool ExpectedValue = true;

    //    // Act
    //    var cities = await GetActualOutpostsAsyncValue().ConfigureAwait(false);

    //    // Assert
    //    cities.Should().NotBeNull();
    //    var actual = cities.First();
    //    actual.HasHabitation.Should().Be(ExpectedValue);
    //}

    //[Fact]
    //public async Task GetOutpostsAsync_ShouldHaveExpectedHasRefinery()
    //{
    //    // Assemble
    //    const bool ExpectedValue = false;

    //    // Act
    //    var cities = await GetActualOutpostsAsyncValue().ConfigureAwait(false);

    //    // Assert
    //    cities.Should().NotBeNull();
    //    var actual = cities.First();
    //    actual.HasRefinery.Should().Be(ExpectedValue);
    //}

    //[Fact]
    //public async Task GetOutpostsAsync_ShouldHaveExpectedHasCargoCenter()
    //{
    //    // Assemble
    //    const bool ExpectedValue = false;

    //    // Act
    //    var cities = await GetActualOutpostsAsyncValue().ConfigureAwait(false);

    //    // Assert
    //    cities.Should().NotBeNull();
    //    var actual = cities.First();
    //    actual.HasCargoCenter.Should().Be(ExpectedValue);
    //}

    //[Fact]
    //public async Task GetOutpostsAsync_ShouldHaveExpectedHasClinic()
    //{
    //    // Assemble
    //    const bool ExpectedValue = true;

    //    // Act
    //    var cities = await GetActualOutpostsAsyncValue().ConfigureAwait(false);

    //    // Assert
    //    cities.Should().NotBeNull();
    //    var actual = cities.First();
    //    actual.HasClinic.Should().Be(ExpectedValue);
    //}

    //[Fact]
    //public async Task GetOutpostsAsync_ShouldHaveExpectedHasFood()
    //{
    //    // Assemble
    //    const bool ExpectedValue = true;

    //    // Act
    //    var cities = await GetActualOutpostsAsyncValue().ConfigureAwait(false);

    //    // Assert
    //    cities.Should().NotBeNull();
    //    var actual = cities.First();
    //    actual.HasFood.Should().Be(ExpectedValue);
    //}

    [Fact]
    public async Task GetOutpostsAsync_ShouldHaveExpectedHasShops()
    {
        // Assemble
        const bool ExpectedValue = false;

        // Act
        var cities = await GetActualOutpostsAsyncValue().ConfigureAwait(false);

        // Assert
        cities.Should().NotBeNull();
        var actual = cities.First();
        actual.HasShops.Should().Be(ExpectedValue);
    }

    //[Fact]
    //public async Task GetOutpostsAsync_ShouldHaveExpectedHasRefuel()
    //{
    //    // Assemble
    //    const bool ExpectedValue = true;

    //    // Act
    //    var cities = await GetActualOutpostsAsyncValue().ConfigureAwait(false);

    //    // Assert
    //    cities.Should().NotBeNull();
    //    var actual = cities.First();
    //    actual.HasRefuel.Should().Be(ExpectedValue);
    //}

    //[Fact]
    //public async Task GetOutpostsAsync_ShouldHaveExpectedHasRepair()
    //{
    //    // Assemble
    //    const bool ExpectedValue = true;

    //    // Act
    //    var cities = await GetActualOutpostsAsyncValue().ConfigureAwait(false);

    //    // Assert
    //    cities.Should().NotBeNull();
    //    var actual = cities.First();
    //    actual.HasRepair.Should().Be(ExpectedValue);
    //}

    //[Fact]
    //public async Task GetOutpostsAsync_ShouldHaveExpectedHasGravity()
    //{
    //    // Assemble
    //    const bool ExpectedValue = true;

    //    // Act
    //    var cities = await GetActualOutpostsAsyncValue().ConfigureAwait(false);

    //    // Assert
    //    cities.Should().NotBeNull();
    //    var actual = cities.First();
    //    actual.HasGravity.Should().Be(ExpectedValue);
    //}

    [Fact]
    public async Task GetOutpostsAsync_ShouldHaveExpectedDateAdded()
    {
        // Assemble
        DateTimeOffset ExpectedValue = _dateAdded;

        // Act
        var cities = await GetActualOutpostsAsyncValue().ConfigureAwait(false);

        // Assert
        cities.Should().NotBeNull();
        var actual = cities.First();
        actual.DateAdded.Should().NotBe(DateTimeOffset.MinValue);
    }

    [Fact]
    public async Task GetOutpostsAsync_ShouldHaveExpectedDateModified()
    {
        // Assemble
        DateTimeOffset ExpectedValue = _dateModified;

        // Act
        var cities = await GetActualOutpostsAsyncValue().ConfigureAwait(false);

        // Assert
        cities.Should().NotBeNull();
        var actual = cities.First();
        actual.DateModified.Should().NotBe(DateTimeOffset.MinValue);
    }

    #endregion  Outpost

    #region     Commodity

    [Fact]
    public async Task GetCommoditiesAsync_ShouldHaveExpectedCountOf80()
    {
        // Assemble

        // Act
        var actual = await _webApiClient.GetCommoditiesAsync().ConfigureAwait(false);

        // Assert
        actual.Should().HaveCount(80);
    }

    private static ICollection<CommodityDto> _ActualGetCommoditiesAsyncValue = new List<CommodityDto>();
    private async Task<ICollection<CommodityDto>> GetActualCommoditiesAsyncValue()
    {
        if (_ActualGetCommoditiesAsyncValue.Any() == false)
        {
            _ActualGetCommoditiesAsyncValue = await _webApiClient.GetCommoditiesAsync().ConfigureAwait(false);
        }

        return _ActualGetCommoditiesAsyncValue;
    }

    [Fact]
    public async Task GetCommoditiesAsync_ShouldHaveExpectedId()
    {
        // Assemble
        const int ExpectedId = 1;

        // Act
        var commodities = await GetActualCommoditiesAsyncValue().ConfigureAwait(false);

        // Assert
        commodities.Should().NotBeNull();
        var actual = commodities.First();
        actual.Id.Should().Be(ExpectedId);
    }

    [Fact]
    public async Task GetCommoditiesAsync_ShouldHaveExpectedParentId()
    {
        // Assemble
        const int ExpectedId = 0;

        // Act
        var commodities = await GetActualCommoditiesAsyncValue().ConfigureAwait(false);

        // Assert
        commodities.Should().NotBeNull();
        var actual = commodities.First();
        actual.ParentId.Should().Be(ExpectedId);
    }

    [Fact]
    public async Task GetCommoditiesAsync_ShouldHaveExpectedName()
    {
        // Assemble
        const string ExpectedValue = "Agricultural Supplies";

        // Act
        var commodities = await GetActualCommoditiesAsyncValue().ConfigureAwait(false);

        // Assert
        commodities.Should().NotBeNull();
        var actual = commodities.First();
        actual.Name.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommoditiesAsync_ShouldHaveExpectedCode()
    {
        // Assemble
        const string ExpectedValue = "AGRSU";

        // Act
        var commodities = await GetActualCommoditiesAsyncValue().ConfigureAwait(false);

        // Assert
        commodities.Should().NotBeNull();
        var actual = commodities.First();
        actual.Code.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommoditiesAsync_ShouldHaveExpectedKind()
    {
        // Assemble
        const string ExpectedValue = "Agricultural";

        // Act
        var commodities = await GetActualCommoditiesAsyncValue().ConfigureAwait(false);

        // Assert
        commodities.Should().NotBeNull();
        var actual = commodities.First();
        actual.Kind.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommoditiesAsync_ShouldHaveExpectedBuyPrice()
    {
        // Assemble
        const float ExpectedValue = 99.2308f;

        // Act
        var commodities = await GetActualCommoditiesAsyncValue().ConfigureAwait(false);

        // Assert
        commodities.Should().NotBeNull();
        var actual = commodities.First();
        actual.BuyPrice.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommoditiesAsync_ShouldHaveExpectedSellPrice()
    {
        // Assemble
        const float ExpectedValue = 119.556f;

        // Act
        var commodities = await GetActualCommoditiesAsyncValue().ConfigureAwait(false);

        // Assert
        commodities.Should().NotBeNull();
        var actual = commodities.First();
        actual.SellPrice.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommoditiesAsync_ShouldHaveExpectedIsAvailable()
    {
        // Assemble
        const bool ExpectedValue = true;

        // Act
        var commodities = await GetActualCommoditiesAsyncValue().ConfigureAwait(false);

        // Assert
        commodities.Should().NotBeNull();
        var actual = commodities.First();
        actual.IsAvailable.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommoditiesAsync_ShouldHaveExpectedIsVisible()
    {
        // Assemble
        const bool ExpectedValue = true;

        // Act
        var commodities = await GetActualCommoditiesAsyncValue().ConfigureAwait(false);

        // Assert
        commodities.Should().NotBeNull();
        var actual = commodities.First();
        actual.IsVisible.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommoditiesAsync_ShouldHaveExpectedIsRaw()
    {
        // Assemble
        const bool ExpectedValue = false;

        // Act
        var commodities = await GetActualCommoditiesAsyncValue().ConfigureAwait(false);

        // Assert
        commodities.Should().NotBeNull();
        var actual = commodities.First();
        actual.IsRaw.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommoditiesAsync_ShouldHaveExpectedIsHarvestable()
    {
        // Assemble
        const bool ExpectedValue = false;

        // Act
        var commodities = await GetActualCommoditiesAsyncValue().ConfigureAwait(false);

        // Assert
        commodities.Should().NotBeNull();
        var actual = commodities.First();
        actual.IsHarvestable.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommoditiesAsync_ShouldHaveExpectedIsBuyable()
    {
        // Assemble
        const bool ExpectedValue = true;

        // Act
        var commodities = await GetActualCommoditiesAsyncValue().ConfigureAwait(false);

        // Assert
        commodities.Should().NotBeNull();
        var actual = commodities.First();
        actual.IsBuyable.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommoditiesAsync_ShouldHaveExpectedIsSellable()
    {
        // Assemble
        const bool ExpectedValue = true;

        // Act
        var commodities = await GetActualCommoditiesAsyncValue().ConfigureAwait(false);

        // Assert
        commodities.Should().NotBeNull();
        var actual = commodities.First();
        actual.IsSellable.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommoditiesAsync_ShouldHaveExpectedIsTemporary()
    {
        // Assemble
        const bool ExpectedValue = false;

        // Act
        var commodities = await GetActualCommoditiesAsyncValue().ConfigureAwait(false);

        // Assert
        commodities.Should().NotBeNull();
        var actual = commodities.First();
        actual.IsTemporary.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommoditiesAsync_ShouldHaveExpectedIsIllegal()
    {
        // Assemble
        const bool ExpectedValue = false;

        // Act
        var commodities = await GetActualCommoditiesAsyncValue().ConfigureAwait(false);

        // Assert
        commodities.Should().NotBeNull();
        var actual = commodities.First();
        actual.IsIllegal.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommoditiesAsync_ShouldHaveExpectedDateAdded()
    {
        // Assemble
        DateTimeOffset ExpectedValue = _dateAdded;

        // Act
        var commodities = await GetActualCommoditiesAsyncValue().ConfigureAwait(false);

        // Assert
        commodities.Should().NotBeNull();
        var actual = commodities.First();
        actual.DateAdded.Should().NotBe(DateTimeOffset.MinValue);
    }

    [Fact]
    public async Task GetCommoditiesAsync_ShouldHaveExpectedDateModified()
    {
        // Assemble
        DateTimeOffset ExpectedValue = _dateModified;

        // Act
        var commodities = await GetActualCommoditiesAsyncValue().ConfigureAwait(false);

        // Assert
        commodities.Should().NotBeNull();
        var actual = commodities.First();
        actual.DateModified.Should().NotBe(DateTimeOffset.MinValue);
    }

    #endregion  Commodity

    #region     CommodityPrice

    [Fact]
    public async Task GetCommodityPricesByCommodityAsync_ShouldHaveExpectedCountOf9()
    {
        // Assemble
        const int commodityId = 4;

        // Act
        var actual = await _webApiClient.GetCommodityPricesByCommodityIdAsync(commodityId).ConfigureAwait(false);

        // Assert
        actual.Should().HaveCount(9);
    }

    [Fact]
    public async Task GetCommodityPricesByTerminalAsync_ShouldHaveExpectedCountOf14()
    {
        // Assemble
        const int terminalId = 33;

        // Act
        var actual = await _webApiClient.GetCommodityPricesAsync(terminalId).ConfigureAwait(false);

        // Assert
        actual.Should().HaveCount(14);
    }

    private static ICollection<CommodityPriceDto> _ActualGetCommodityPricesAsyncValue = new List<CommodityPriceDto>();
    private async Task<ICollection<CommodityPriceDto>> GetActualCommodityPricesAsyncValue()
    {
        const int commodityId = 4;
        if (_ActualGetCommodityPricesAsyncValue.Any() == false)
        {
            _ActualGetCommodityPricesAsyncValue = await _webApiClient.GetCommodityPricesByCommodityIdAsync(commodityId).ConfigureAwait(false);
        }

        return _ActualGetCommodityPricesAsyncValue;
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedCount()
    {
        // Assemble
        const int ExpectedCount = 9;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        commodityPrices.Count.Should().Be(ExpectedCount);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedId()
    {
        // Assemble
        const int ExpectedId = 443;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First(x => x.Id.Equals(ExpectedId));
        actual.Id.Should().Be(ExpectedId);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedCommodityId()
    {
        // Assemble
        const int ExpectedId = 443;
        const int ExpectedValue = 4;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First(x => x.Id.Equals(ExpectedId));
        actual.CommodityId.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedStarSystemId()
    {
        // Assemble
        const int ExpectedId = 443;
        const int ExpectedValue = 68;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First(x => x.Id.Equals(ExpectedId));
        actual.StarSystemId.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedPlanetId()
    {
        // Assemble
        const int ExpectedId = 443;
        const int ExpectedValue = 190;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First(x => x.Id.Equals(ExpectedId));
        actual.PlanetId.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedMoonId()
    {
        // Assemble
        const int ExpectedId = 443;
        const int ExpectedValue = 0;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First(x => x.Id.Equals(ExpectedId));
        actual.MoonId.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedCityId()
    {
        // Assemble
        const int ExpectedId = 443;
        const int ExpectedValue = 0;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First(x => x.Id.Equals(ExpectedId));
        actual.CityId.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedOutpostId()
    {
        // Assemble
        const int ExpectedId = 443;
        const int ExpectedValue = 35;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First(x => x.Id.Equals(ExpectedId));
        actual.OutpostId.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedTerminalId()
    {
        // Assemble
        const int ExpectedId = 443;
        const int ExpectedValue = 63;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First(x => x.Id.Equals(ExpectedId));
        actual.TerminalId.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedBuyPrice()
    {
        // Assemble
        const int ExpectedId = 443;
        const float ExpectedValue = 4324f;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First(x => x.Id.Equals(ExpectedId));
        actual.BuyPrice.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedBuyPriceMin()
    {
        // Assemble
        const int ExpectedId = 443;
        const float ExpectedValue = 4324f;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First(x => x.Id.Equals(ExpectedId));
        actual.BuyPriceMin.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedBuyPriceMinWeek()
    {
        // Assemble
        const int ExpectedId = 443;
        const float ExpectedValue = 4324f;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First(x => x.Id.Equals(ExpectedId));
        actual.BuyPriceMinWeek.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedBuyPriceMinWeekMonth()
    {
        // Assemble
        const int ExpectedId = 443;
        const float ExpectedValue = 4324f;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First(x => x.Id.Equals(ExpectedId));
        actual.BuyPriceMinMonth.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedBuyPriceMax()
    {
        // Assemble
        const int ExpectedId = 443;
        const float ExpectedValue = 4549f;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First(x => x.Id.Equals(ExpectedId));
        actual.BuyPriceMax.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedBuyPriceMaxWeek()
    {
        // Assemble
        const int ExpectedId = 443;
        const float ExpectedValue = 4549f;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First(x => x.Id.Equals(ExpectedId));
        actual.BuyPriceMaxWeek.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedBuyPriceMaxMonth()
    {
        // Assemble
        const int ExpectedId = 443;
        const float ExpectedValue = 4549f;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First(x => x.Id.Equals(ExpectedId));
        actual.BuyPriceMaxMonth.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedBuyPriceAvg()
    {
        // Assemble
        const int ExpectedId = 443;
        const float ExpectedValue = 4436.5f;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First(x => x.Id.Equals(ExpectedId));
        actual.BuyPriceAvg.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedBuyPriceAvgWeek()
    {
        // Assemble
        const int ExpectedId = 443;
        const float ExpectedValue = 4436.5f;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First(x => x.Id.Equals(ExpectedId));
        actual.BuyPriceAvgWeek.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedBuyPriceAvgMonth()
    {
        // Assemble
        const int ExpectedId = 443;
        const float ExpectedValue = 4436.5f;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First(x => x.Id.Equals(ExpectedId));
        actual.BuyPriceAvgMonth.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedSellPrice()
    {
        // Assemble
        const int ExpectedId = 566;
        const float ExpectedValue = 5335f;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First(x => x.Id.Equals(ExpectedId));
        actual.SellPrice.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedSellPriceMin()
    {
        // Assemble
        const int ExpectedId = 566;
        const float ExpectedValue = 5335f;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First(x => x.Id.Equals(ExpectedId));
        actual.SellPriceMin.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedSellPriceMinWeek()
    {
        // Assemble
        const int ExpectedId = 566;
        const float ExpectedValue = 5335f;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First(x => x.Id.Equals(ExpectedId));
        actual.SellPriceMinWeek.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedSellPriceMinMonth()
    {
        // Assemble
        const int ExpectedId = 566;
        const float ExpectedValue = 5335f;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First(x => x.Id.Equals(ExpectedId));
        actual.SellPriceMinMonth.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedSellPriceMax()
    {
        // Assemble
        const int ExpectedId = 566;
        const float ExpectedValue = 5335f;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First(x => x.Id.Equals(ExpectedId));
        actual.SellPriceMax.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedSellPriceMaxWeek()
    {
        // Assemble
        const int ExpectedId = 566;
        const float ExpectedValue = 5335f;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First(x => x.Id.Equals(ExpectedId));
        actual.SellPriceMaxWeek.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedSellPriceMaxMonth()
    {
        // Assemble
        const int ExpectedId = 566;
        const float ExpectedValue = 5335f;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First(x => x.Id.Equals(ExpectedId));
        actual.SellPriceMaxMonth.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedSellPriceAvg()
    {
        // Assemble
        const int ExpectedId = 566;
        const float ExpectedValue = 5335f;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First(x => x.Id.Equals(ExpectedId));
        actual.SellPriceAvg.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedSellPriceAvgWeek()
    {
        // Assemble
        const int ExpectedId = 566;
        const float ExpectedValue = 5335f;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First(x => x.Id.Equals(ExpectedId));
        actual.SellPriceAvgWeek.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedSellPriceAvgMonth()
    {
        // Assemble
        const int ExpectedId = 566;
        const float ExpectedValue = 5335f;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First(x => x.Id.Equals(ExpectedId));
        actual.SellPriceAvgMonth.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedScuBuy()
    {
        // Assemble
        const int ExpectedId = 443;
        const int ExpectedValue = 50;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First(x => x.Id.Equals(ExpectedId));
        actual.ScuBuy.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedScuBuyMin()
    {
        // Assemble
        const int ExpectedId = 443;
        const int ExpectedValue = 11;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First(x => x.Id.Equals(ExpectedId));
        actual.ScuBuyMin.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedScuBuyMinWeek()
    {
        // Assemble
        const int ExpectedId = 443;
        const int ExpectedValue = 11;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First(x => x.Id.Equals(ExpectedId));
        actual.ScuBuyMinWeek.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedScuBuyMinMonth()
    {
        // Assemble
        const int ExpectedId = 443;
        const int ExpectedValue = 11;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First(x => x.Id.Equals(ExpectedId));
        actual.ScuBuyMinMonth.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedScuBuyMax()
    {
        // Assemble
        const int ExpectedId = 443;
        const int ExpectedValue = 50;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First(x => x.Id.Equals(ExpectedId));
        actual.ScuBuyMax.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedScuBuyMaxWeek()
    {
        // Assemble
        const int ExpectedId = 443;
        const int ExpectedValue = 50;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First(x => x.Id.Equals(ExpectedId));
        actual.ScuBuyMaxWeek.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedScuBuyMaxMonth()
    {
        // Assemble
        const int ExpectedId = 443;
        const int ExpectedValue = 50;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First(x => x.Id.Equals(ExpectedId));
        actual.ScuBuyMaxMonth.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedScuBuyAvg()
    {
        // Assemble
        const int ExpectedId = 443;
        const int ExpectedValue = 31;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First(x => x.Id.Equals(ExpectedId));
        actual.ScuBuyAvg.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedScuBuyAvgWeek()
    {
        // Assemble
        const int ExpectedId = 443;
        const int ExpectedValue = 31;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First(x => x.Id.Equals(ExpectedId));
        actual.ScuBuyAvgWeek.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedScuBuyAvgMonth()
    {
        // Assemble
        const int ExpectedId = 443;
        const int ExpectedValue = 31;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First(x => x.Id.Equals(ExpectedId));
        actual.ScuBuyAvgMonth.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedScuSell()
    {
        // Assemble
        const int ExpectedId = 566;
        const int ExpectedValue = 292;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First(x => x.Id.Equals(ExpectedId));
        actual.ScuSell.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedScuSellMin()
    {
        // Assemble
        const int ExpectedId = 566;
        const int ExpectedValue = 292;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First(x => x.Id.Equals(ExpectedId));
        actual.ScuSellMin.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedScuSellMinWeek()
    {
        // Assemble
        const int ExpectedId = 566;
        const int ExpectedValue = 292;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First(x => x.Id.Equals(ExpectedId));
        actual.ScuSellMinWeek.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedScuSellMinMonth()
    {
        // Assemble
        const int ExpectedId = 566;
        const int ExpectedValue = 292;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First(x => x.Id.Equals(ExpectedId));
        actual.ScuSellMinMonth.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedScuSellMax()
    {
        // Assemble
        const int ExpectedId = 566;
        const int ExpectedValue = 292;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First(x => x.Id.Equals(ExpectedId));
        actual.ScuSellMax.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedScuSellMaxWeek()
    {
        // Assemble
        const int ExpectedId = 566;
        const int ExpectedValue = 292;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First(x => x.Id.Equals(ExpectedId));
        actual.ScuSellMaxWeek.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedScuSellMaxMonth()
    {
        // Assemble
        const int ExpectedId = 566;
        const int ExpectedValue = 292;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First(x => x.Id.Equals(ExpectedId));
        actual.ScuSellMaxMonth.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedScuSellAvg()
    {
        // Assemble
        const int ExpectedId = 566;
        const int ExpectedValue = 292;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First(x => x.Id.Equals(ExpectedId));
        actual.ScuSellAvg.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedScuSellAvgWeek()
    {
        // Assemble
        const int ExpectedId = 566;
        const int ExpectedValue = 292;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First(x => x.Id.Equals(ExpectedId));
        actual.ScuSellAvgWeek.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedScuSellAvgMonth()
    {
        // Assemble
        const int ExpectedId = 566;
        const int ExpectedValue = 292;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First(x => x.Id.Equals(ExpectedId));
        actual.ScuSellAvgMonth.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedGameVersion()
    {
        // Assemble
        const int ExpectedId = 443;
        const string ExpectedValue = "3.22";

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First(x => x.Id.Equals(ExpectedId));
        actual.GameVersion.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedCommodityName()
    {
        // Assemble
        const int ExpectedId = 443;
        const string ExpectedValue = "Altruciatoxin";

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First(x => x.Id.Equals(ExpectedId));
        actual.CommodityName.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedStarSystemName()
    {
        // Assemble
        const int ExpectedId = 443;
        const string ExpectedValue = "Stanton";

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First(x => x.Id.Equals(ExpectedId));
        actual.StarSystemName.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedPlanetName()
    {
        // Assemble
        const int ExpectedId = 443;
        const string ExpectedValue = "MicroTech";

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First(x => x.Id.Equals(ExpectedId));
        actual.PlanetName.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedMoonName()
    {
        // Assemble
        const int ExpectedId = 443;
        const string ExpectedValue = null;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First(x => x.Id.Equals(ExpectedId));
        actual.MoonName.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedSpaceStationName()
    {
        // Assemble
        const int ExpectedId = 443;
        const string ExpectedValue = null;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First(x => x.Id.Equals(ExpectedId));
        actual.SpaceStationName.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedOutpostName()
    {
        // Assemble
        const int ExpectedId = 443;
        const string ExpectedValue = "Outpost 54";

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First(x => x.Id.Equals(ExpectedId));
        actual.OutpostName.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedCityName()
    {
        // Assemble
        const int ExpectedId = 443;
        const string ExpectedValue = null;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First(x => x.Id.Equals(ExpectedId));
        actual.CityName.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedFactionName()
    {
        // Assemble
        const int ExpectedId = 443;
        const string ExpectedValue = "United Empire of Earth";

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First(x => x.Id.Equals(ExpectedId));
        actual.FactionName.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedDateAdded()
    {
        // Assemble
        DateTimeOffset ExpectedValue = _dateAdded;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First();
        actual.DateAdded.Should().NotBe(DateTimeOffset.MinValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedDateModified()
    {
        // Assemble
        DateTimeOffset ExpectedValue = _dateModified;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First();
        actual.DateModified.Should().NotBe(DateTimeOffset.MinValue);
    }

    #endregion  CommodityPrice

    #region     Terminal

    [Fact]
    public async Task GetTerminalsAsync_ShouldHaveExpectedCountOf290()
    {
        // Assemble
        int starSystemId = 68;

        // Act
        var actual = await _webApiClient.GetTerminalsAsync(starSystemId).ConfigureAwait(false);

        // Assert
        actual.Should().HaveCount(290);
    }

    private static ICollection<TerminalDto> _ActualGetTerminalsAsyncValue = new List<TerminalDto>();
    private async Task<ICollection<TerminalDto>> GetActualTerminalsAsyncValue()
    {
        int starSystemId = 68;
        if (_ActualGetTerminalsAsyncValue.Any() == false)
        {
            _ActualGetTerminalsAsyncValue = await _webApiClient.GetTerminalsAsync(starSystemId).ConfigureAwait(false);
        }

        return _ActualGetTerminalsAsyncValue;
    }

    [Fact]
    public async Task GetTerminalsAsync_ShouldHaveExpectedId()
    {
        // Assemble
        const int ExpectedId = 1;

        // Act
        var terminals = await GetActualTerminalsAsyncValue().ConfigureAwait(false);

        // Assert
        terminals.Should().NotBeNull();
        var actual = terminals.First();
        actual.Id.Should().Be(ExpectedId);
    }

    [Fact]
    public async Task GetTerminalsAsync_ShouldHaveExpectedStarSystemId()
    {
        // Assemble
        const int ExpectedId = 68;

        // Act
        var terminals = await GetActualTerminalsAsyncValue().ConfigureAwait(false);

        // Assert
        terminals.Should().NotBeNull();
        var actual = terminals.First();
        actual.StarSystemId.Should().Be(ExpectedId);
    }

    [Fact]
    public async Task GetTerminalsAsync_ShouldHaveExpectedPlanetId()
    {
        // Assemble
        const int ExpectedId = 326;

        // Act
        var terminals = await GetActualTerminalsAsyncValue().ConfigureAwait(false);

        // Assert
        terminals.Should().NotBeNull();
        var actual = terminals.First();
        actual.PlanetId.Should().Be(ExpectedId);
    }

    [Fact]
    public async Task GetTerminalsAsync_ShouldHaveExpectedMoonId()
    {
        // Assemble
        const int ExpectedId = 0;

        // Act
        var terminals = await GetActualTerminalsAsyncValue().ConfigureAwait(false);

        // Assert
        terminals.Should().NotBeNull();
        var actual = terminals.First();
        actual.MoonId.Should().Be(ExpectedId);
    }

    [Fact]
    public async Task GetTerminalsAsync_ShouldHaveExpectedSpaceStationId()
    {
        // Assemble
        const int ExpectedId = 1;

        // Act
        var terminals = await GetActualTerminalsAsyncValue().ConfigureAwait(false);

        // Assert
        terminals.Should().NotBeNull();
        var actual = terminals.First();
        actual.SpaceStationId.Should().Be(ExpectedId);
    }

    [Fact]
    public async Task GetTerminalsAsync_ShouldHaveExpectedOutpostId()
    {
        // Assemble
        const int ExpectedId = 0;

        // Act
        var terminals = await GetActualTerminalsAsyncValue().ConfigureAwait(false);

        // Assert
        terminals.Should().NotBeNull();
        var actual = terminals.First();
        actual.OutpostId.Should().Be(ExpectedId);
    }

    [Fact]
    public async Task GetTerminalsAsync_ShouldHaveExpectedCityId()
    {
        // Assemble
        const int ExpectedId = 0;

        // Act
        var terminals = await GetActualTerminalsAsyncValue().ConfigureAwait(false);

        // Assert
        terminals.Should().NotBeNull();
        var actual = terminals.First();
        actual.CityId.Should().Be(ExpectedId);
    }

    [Fact]
    public async Task GetTerminalsAsync_ShouldHaveExpectedName()
    {
        // Assemble
        const string ExpectedValue = "Admin - ARC-L1";

        // Act
        var terminals = await GetActualTerminalsAsyncValue().ConfigureAwait(false);

        // Assert
        terminals.Should().NotBeNull();
        var actual = terminals.First();
        actual.Name.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetTerminalsAsync_ShouldHaveExpectedNickname()
    {
        // Assemble
        const string ExpectedValue = "ARC-L1";

        // Act
        var terminals = await GetActualTerminalsAsyncValue().ConfigureAwait(false);

        // Assert
        terminals.Should().NotBeNull();
        var actual = terminals.First();
        actual.Nickname.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetTerminalsAsync_ShouldHaveExpectedCode()
    {
        // Assemble
        const string ExpectedValue = "ARCL1";

        // Act
        var terminals = await GetActualTerminalsAsyncValue().ConfigureAwait(false);

        // Assert
        terminals.Should().NotBeNull();
        var actual = terminals.First();
        actual.Code.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetTerminalsAsync_ShouldHaveExpectedType()
    {
        // Assemble
        const string ExpectedValue = "commodity";

        // Act
        var terminals = await GetActualTerminalsAsyncValue().ConfigureAwait(false);

        // Assert
        terminals.Should().NotBeNull();
        var actual = terminals.First();
        actual.Type.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetTerminalsAsync_ShouldHaveExpectedScreenshot()
    {
        // Assemble
        const string ExpectedValue = null;

        // Act
        var terminals = await GetActualTerminalsAsyncValue().ConfigureAwait(false);

        // Assert
        terminals.Should().NotBeNull();
        var actual = terminals.First();
        actual.Screenshot.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetTerminalsAsync_ShouldHaveExpectedStarSystemName()
    {
        // Assemble
        const string ExpectedValue = "Stanton";

        // Act
        var terminals = await GetActualTerminalsAsyncValue().ConfigureAwait(false);

        // Assert
        terminals.Should().NotBeNull();
        var actual = terminals.First();
        actual.StarSystemName.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetTerminalsAsync_ShouldHaveExpectedPlanetName()
    {
        // Assemble
        const string ExpectedValue = "ArcCorp Lagrange 1";

        // Act
        var terminals = await GetActualTerminalsAsyncValue().ConfigureAwait(false);

        // Assert
        terminals.Should().NotBeNull();
        var actual = terminals.First();
        actual.PlanetName.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetTerminalsAsync_ShouldHaveExpectedMoonName()
    {
        // Assemble
        const string ExpectedValue = null;

        // Act
        var terminals = await GetActualTerminalsAsyncValue().ConfigureAwait(false);

        // Assert
        terminals.Should().NotBeNull();
        var actual = terminals.First();
        actual.MoonName.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetTerminalsAsync_ShouldHaveExpectedSpaceStationName()
    {
        // Assemble
        const string ExpectedValue = "ARC-L1 Wide Forest Station";

        // Act
        var terminals = await GetActualTerminalsAsyncValue().ConfigureAwait(false);

        // Assert
        terminals.Should().NotBeNull();
        var actual = terminals.First();
        actual.SpaceStationName.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetTerminalsAsync_ShouldHaveExpectedOutpostName()
    {
        // Assemble
        const string ExpectedValue = null;

        // Act
        var terminals = await GetActualTerminalsAsyncValue().ConfigureAwait(false);

        // Assert
        terminals.Should().NotBeNull();
        var actual = terminals.First();
        actual.OutpostName.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetTerminalsAsync_ShouldHaveExpectedCityName()
    {
        // Assemble
        const string ExpectedValue = null;

        // Act
        var terminals = await GetActualTerminalsAsyncValue().ConfigureAwait(false);

        // Assert
        terminals.Should().NotBeNull();
        var actual = terminals.First();
        actual.CityName.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetTerminalsAsync_ShouldHaveExpectedDateAdded()
    {
        // Assemble
        DateTimeOffset ExpectedValue = _dateAdded;

        // Act
        var terminals = await GetActualTerminalsAsyncValue().ConfigureAwait(false);

        // Assert
        terminals.Should().NotBeNull();
        var actual = terminals.First();
        actual.DateAdded.Should().NotBe(DateTimeOffset.MinValue);
    }

    [Fact]
    public async Task GetTerminalsAsync_ShouldHaveExpectedDateModified()
    {
        // Assemble
        DateTimeOffset ExpectedValue = _dateModified;

        // Act
        var terminals = await GetActualTerminalsAsyncValue().ConfigureAwait(false);

        // Assert
        terminals.Should().NotBeNull();
        var actual = terminals.First();
        actual.DateModified.Should().NotBe(DateTimeOffset.MinValue);
    }

    #endregion  Terminal

    #region     DataSubmit
    [Fact]
    public async Task SubmitDataAsync_ShouldHaveExpectedCode()
    {
        // Assemble
        int ExpectedValue = 200;
        DataSubmitDto submitDto = new DataSubmitDto();

        // Act
        var actual = await _webApiClient.SubmitDataAsync(submitDto).ConfigureAwait(false);

        // Assert
        actual.Should().NotBeNull();
        actual.Code.Should().Be(ExpectedValue);
    }
    #endregion  DataSubmit
}
