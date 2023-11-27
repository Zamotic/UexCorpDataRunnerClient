using NSubstitute;
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
        IUexCorpWebApiConfiguration substituteWebConfiguration = Substitute.For<IUexCorpWebApiConfiguration>();
        substituteWebConfiguration.DataRunnerEndpointPath.Returns(string.Empty);
        substituteWebConfiguration.WebApiEndPointUrl.Returns("https://ptu.uexcorp.space/api/");
        substituteWebConfiguration.ApiKey.Returns("FoGk3H4kH1DnbSHkBRtoyyPQ/Uo/Ar0VPSXaqdVtI4RgoB4zJ25CiOH7ne5JzzbH");

        _uexWebApiMockHttpMessageHandler = new UexWebApiMockHttpMessageHandler();
        HttpClient mockedHttpClient = new HttpClient(_uexWebApiMockHttpMessageHandler);
        _webApiClient = new UexCorpWebApiClient(substituteWebConfiguration, mockedHttpClient);
    }

    #region     GameVersion
    [Fact]
    public async Task GetCurrentGameVersionAsync_ShouldHaveExpectedLiveValue()
    {
        // Assemble
        const string ExpectedValue = "3.21.1";

        // Act
        var actual = await _webApiClient.GetCurrentVersionAsync().ConfigureAwait(false);

        // Assert
        actual.Live.Should().Be(ExpectedValue);
    }
    [Fact]
    public async Task GetCurrentGameVersionAsync_ShouldHaveExpectedPtuValue()
    {
        // Assemble
        const string ExpectedValue = "3.22";

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
    public async Task GetPlanetsAsync_ShouldHaveExpectedCountOf4()
    {
        // Assemble
        const int starSystemId = 68;

        // Act
        var actual = await _webApiClient.GetPlanetsAsync(starSystemId).ConfigureAwait(false);

        // Assert
        actual.Should().HaveCount(4);
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
        const int ExpectedId = 1;

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
        var actual = await _webApiClient.GetMoonsAsync(starSystemId).ConfigureAwait(false);

        // Assert
        actual.Should().HaveCount(12);
    }

    [Fact]
    public async Task GetMoonsByPlanetAsync_ShouldHaveExpectedCountOf4()
    {
        // Assemble
        const int starSystemId = 68;
        const int planetId = 4;

        // Act
        var actual = await _webApiClient.GetMoonsAsync(starSystemId, planetId).ConfigureAwait(false);

        // Assert
        actual.Should().HaveCount(4);
    }

    private static ICollection<MoonDto> _ActualGetMoonsAsyncValue = new List<MoonDto>();
    private async Task<ICollection<MoonDto>> GetActualMoonsAsyncValue()
    {
        const int starSystemId = 68;
        if (_ActualGetMoonsAsyncValue.Any() == false)
        {
            _ActualGetMoonsAsyncValue = await _webApiClient.GetMoonsAsync(starSystemId).ConfigureAwait(false);
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
        const int ExpectedPlanetId = 4;

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
        const string ExpectedValue = "Stanton 1b";

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
        var actual = await _webApiClient.GetCitiesAsync(starSystemId).ConfigureAwait(false);

        // Assert
        actual.Should().HaveCount(4);
    }

    private static ICollection<CityDto> _ActualGetCitiesAsyncValue = new List<CityDto>();
    private async Task<ICollection<CityDto>> GetActualCitiesAsyncValue()
    {
        const int starSystemId = 68;
        if (_ActualGetCitiesAsyncValue.Any() == false)
        {
            _ActualGetCitiesAsyncValue = await _webApiClient.GetCitiesAsync(starSystemId).ConfigureAwait(false);
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
        const int ExpectedPlanetId = 1;

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

    [Fact]
    public async Task GetCitiesAsync_ShouldHaveExpectedIsMonitored()
    {
        // Assemble
        const bool ExpectedValue = true;

        // Act
        var cities = await GetActualCitiesAsyncValue().ConfigureAwait(false);

        // Assert
        cities.Should().NotBeNull();
        var actual = cities.First();
        actual.IsMonitored.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCitiesAsync_ShouldHaveExpectedIsArmistice()
    {
        // Assemble
        const bool ExpectedValue = true;

        // Act
        var cities = await GetActualCitiesAsyncValue().ConfigureAwait(false);

        // Assert
        cities.Should().NotBeNull();
        var actual = cities.First();
        actual.IsArmistice.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCitiesAsync_ShouldHaveExpectedIsLandable()
    {
        // Assemble
        const bool ExpectedValue = true;

        // Act
        var cities = await GetActualCitiesAsyncValue().ConfigureAwait(false);

        // Assert
        cities.Should().NotBeNull();
        var actual = cities.First();
        actual.IsLandable.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCitiesAsync_ShouldHaveExpectedIsDecomissioned()
    {
        // Assemble
        const bool ExpectedValue = false;

        // Act
        var cities = await GetActualCitiesAsyncValue().ConfigureAwait(false);

        // Assert
        cities.Should().NotBeNull();
        var actual = cities.First();
        actual.IsDecomissioned.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCitiesAsync_ShouldHaveExpectedHasQuantumMarker()
    {
        // Assemble
        const bool ExpectedValue = true;

        // Act
        var cities = await GetActualCitiesAsyncValue().ConfigureAwait(false);

        // Assert
        cities.Should().NotBeNull();
        var actual = cities.First();
        actual.HasQuantumMarker.Should().Be(ExpectedValue);
    }

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

    [Fact]
    public async Task GetCitiesAsync_ShouldHaveExpectedHasHabitation()
    {
        // Assemble
        const bool ExpectedValue = true;

        // Act
        var cities = await GetActualCitiesAsyncValue().ConfigureAwait(false);

        // Assert
        cities.Should().NotBeNull();
        var actual = cities.First();
        actual.HasHabitation.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCitiesAsync_ShouldHaveExpectedHasRefinery()
    {
        // Assemble
        const bool ExpectedValue = false;

        // Act
        var cities = await GetActualCitiesAsyncValue().ConfigureAwait(false);

        // Assert
        cities.Should().NotBeNull();
        var actual = cities.First();
        actual.HasRefinery.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCitiesAsync_ShouldHaveExpectedHasCargoCenter()
    {
        // Assemble
        const bool ExpectedValue = false;

        // Act
        var cities = await GetActualCitiesAsyncValue().ConfigureAwait(false);

        // Assert
        cities.Should().NotBeNull();
        var actual = cities.First();
        actual.HasCargoCenter.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCitiesAsync_ShouldHaveExpectedHasClinic()
    {
        // Assemble
        const bool ExpectedValue = true;

        // Act
        var cities = await GetActualCitiesAsyncValue().ConfigureAwait(false);

        // Assert
        cities.Should().NotBeNull();
        var actual = cities.First();
        actual.HasClinic.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCitiesAsync_ShouldHaveExpectedHasFood()
    {
        // Assemble
        const bool ExpectedValue = true;

        // Act
        var cities = await GetActualCitiesAsyncValue().ConfigureAwait(false);

        // Assert
        cities.Should().NotBeNull();
        var actual = cities.First();
        actual.HasFood.Should().Be(ExpectedValue);
    }

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

    [Fact]
    public async Task GetCitiesAsync_ShouldHaveExpectedHasRefuel()
    {
        // Assemble
        const bool ExpectedValue = true;

        // Act
        var cities = await GetActualCitiesAsyncValue().ConfigureAwait(false);

        // Assert
        cities.Should().NotBeNull();
        var actual = cities.First();
        actual.HasRefuel.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCitiesAsync_ShouldHaveExpectedHasRepair()
    {
        // Assemble
        const bool ExpectedValue = true;

        // Act
        var cities = await GetActualCitiesAsyncValue().ConfigureAwait(false);

        // Assert
        cities.Should().NotBeNull();
        var actual = cities.First();
        actual.HasRepair.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCitiesAsync_ShouldHaveExpectedHasGravity()
    {
        // Assemble
        const bool ExpectedValue = true;

        // Act
        var cities = await GetActualCitiesAsyncValue().ConfigureAwait(false);

        // Assert
        cities.Should().NotBeNull();
        var actual = cities.First();
        actual.HasGravity.Should().Be(ExpectedValue);
    }

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

    #region     Commodity

    [Fact]
    public async Task GetCommoditiesAsync_ShouldHaveExpectedCountOf10()
    {
        // Assemble

        // Act
        var actual = await _webApiClient.GetCommoditiesAsync().ConfigureAwait(false);

        // Assert
        actual.Should().HaveCount(10);
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
    public async Task GetCommoditiesAsync_ShouldHaveExpectedPriceBuy()
    {
        // Assemble
        const float ExpectedValue = 99.3023f;

        // Act
        var commodities = await GetActualCommoditiesAsyncValue().ConfigureAwait(false);

        // Assert
        commodities.Should().NotBeNull();
        var actual = commodities.First();
        actual.PriceBuy.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommoditiesAsync_ShouldHaveExpectedPriceSell()
    {
        // Assemble
        const float ExpectedValue = 118.918f;

        // Act
        var commodities = await GetActualCommoditiesAsyncValue().ConfigureAwait(false);

        // Assert
        commodities.Should().NotBeNull();
        var actual = commodities.First();
        actual.PriceSell.Should().Be(ExpectedValue);
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
    public async Task GetCommodityPricesByCommodityAsync_ShouldHaveExpectedCountOf12()
    {
        // Assemble
        const int commodityId = 4;

        // Act
        var actual = await _webApiClient.GetCommodityPricesByCommodityIdAsync(commodityId).ConfigureAwait(false);

        // Assert
        actual.Should().HaveCount(12);
    }

    [Fact]
    public async Task GetCommodityPricesByTerminalAsync_ShouldHaveExpectedCountOf8()
    {
        // Assemble
        const int terminalId = 33;

        // Act
        var actual = await _webApiClient.GetCommodityPricesByTerminalIdAsync(terminalId).ConfigureAwait(false);

        // Assert
        actual.Should().HaveCount(8);
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
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedId()
    {
        // Assemble
        const int ExpectedId = 6;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First();
        actual.Id.Should().Be(ExpectedId);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedCommodityId()
    {
        // Assemble
        const int ExpectedValue = 4;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First();
        actual.CommodityId.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedStarSystemId()
    {
        // Assemble
        const int ExpectedValue = 68;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First();
        actual.StarSystemId.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedPlanetId()
    {
        // Assemble
        const int ExpectedValue = 1;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First();
        actual.PlanetId.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedMoonId()
    {
        // Assemble
        const int ExpectedValue = 11;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First();
        actual.MoonId.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedCityId()
    {
        // Assemble
        const int ExpectedValue = 0;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First();
        actual.CityId.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedOutpostId()
    {
        // Assemble
        const int ExpectedValue = 3;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First();
        actual.OutpostId.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedTerminalId()
    {
        // Assemble
        const int ExpectedValue = 8;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First();
        actual.TerminalId.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedPriceBuy()
    {
        // Assemble
        const float ExpectedValue = 706f;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First();
        actual.PriceBuy.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedPriceBuyMin()
    {
        // Assemble
        const float ExpectedValue = 706f;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First();
        actual.PriceBuyMin.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedPriceBuyMinWeek()
    {
        // Assemble
        const float ExpectedValue = 706f;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First();
        actual.PriceBuyMinWeek.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedPriceBuyMinWeekMonth()
    {
        // Assemble
        const float ExpectedValue = 706f;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First();
        actual.PriceBuyMinMonth.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedPriceBuyMax()
    {
        // Assemble
        const float ExpectedValue = 706f;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First();
        actual.PriceBuyMax.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedPriceBuyMaxWeek()
    {
        // Assemble
        const float ExpectedValue = 706f;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First();
        actual.PriceBuyMaxWeek.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedPriceBuyMaxMonth()
    {
        // Assemble
        const float ExpectedValue = 706f;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First();
        actual.PriceBuyMaxMonth.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedPriceBuyAvg()
    {
        // Assemble
        const float ExpectedValue = 706f;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First();
        actual.PriceBuyAvg.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedPriceBuyAvgWeek()
    {
        // Assemble
        const float ExpectedValue = 706f;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First();
        actual.PriceBuyAvgWeek.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedPriceBuyAvgMonth()
    {
        // Assemble
        const float ExpectedValue = 706f;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First();
        actual.PriceBuyAvgMonth.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedPriceSell()
    {
        // Assemble
        const float ExpectedValue = 0f;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First();
        actual.PriceSell.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedPriceSellMin()
    {
        // Assemble
        const float ExpectedValue = 0f;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First();
        actual.PriceSellMin.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedPriceSellMinWeek()
    {
        // Assemble
        const float ExpectedValue = 0f;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First();
        actual.PriceSellMinWeek.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedPriceSellMinMonth()
    {
        // Assemble
        const float ExpectedValue = 0f;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First();
        actual.PriceSellMinMonth.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedPriceSellMax()
    {
        // Assemble
        const float ExpectedValue = 0f;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First();
        actual.PriceSellMax.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedPriceSellMaxWeek()
    {
        // Assemble
        const float ExpectedValue = 0f;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First();
        actual.PriceSellMaxWeek.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedPriceSellMaxMonth()
    {
        // Assemble
        const float ExpectedValue = 0f;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First();
        actual.PriceSellMaxMonth.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedPriceSellAvg()
    {
        // Assemble
        const float ExpectedValue = 0f;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First();
        actual.PriceSellAvg.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedPriceSellAvgWeek()
    {
        // Assemble
        const float ExpectedValue = 0f;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First();
        actual.PriceSellAvgWeek.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedPriceSellAvgMonth()
    {
        // Assemble
        const float ExpectedValue = 0f;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First();
        actual.PriceSellAvgMonth.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedScuBuy()
    {
        // Assemble
        const int ExpectedValue = 437;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First();
        actual.ScuBuy.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedScuBuyMin()
    {
        // Assemble
        const int ExpectedValue = 437;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First();
        actual.ScuBuyMin.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedScuBuyMinWeek()
    {
        // Assemble
        const int ExpectedValue = 437;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First();
        actual.ScuBuyMinWeek.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedScuBuyMinMonth()
    {
        // Assemble
        const int ExpectedValue = 437;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First();
        actual.ScuBuyMinMonth.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedScuBuyMax()
    {
        // Assemble
        const int ExpectedValue = 437;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First();
        actual.ScuBuyMax.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedScuBuyMaxWeek()
    {
        // Assemble
        const int ExpectedValue = 437;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First();
        actual.ScuBuyMaxWeek.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedScuBuyMaxMonth()
    {
        // Assemble
        const int ExpectedValue = 437;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First();
        actual.ScuBuyMaxMonth.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedScuBuyAvg()
    {
        // Assemble
        const int ExpectedValue = 437;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First();
        actual.ScuBuyAvg.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedScuBuyAvgWeek()
    {
        // Assemble
        const int ExpectedValue = 437;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First();
        actual.ScuBuyAvgWeek.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedScuBuyAvgMonth()
    {
        // Assemble
        const int ExpectedValue = 437;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First();
        actual.ScuBuyAvgMonth.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedScuSell()
    {
        // Assemble
        const int ExpectedValue = 0;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First();
        actual.ScuSell.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedScuSellMin()
    {
        // Assemble
        const int ExpectedValue = 0;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First();
        actual.ScuSellMin.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedScuSellMinWeek()
    {
        // Assemble
        const int ExpectedValue = 0;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First();
        actual.ScuSellMinWeek.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedScuSellMinMonth()
    {
        // Assemble
        const int ExpectedValue = 0;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First();
        actual.ScuSellMinMonth.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedScuSellMax()
    {
        // Assemble
        const int ExpectedValue = 0;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First();
        actual.ScuSellMax.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedScuSellMaxWeek()
    {
        // Assemble
        const int ExpectedValue = 0;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First();
        actual.ScuSellMaxWeek.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedScuSellMaxMonth()
    {
        // Assemble
        const int ExpectedValue = 0;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First();
        actual.ScuSellMaxMonth.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedScuSellAvg()
    {
        // Assemble
        const int ExpectedValue = 0;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First();
        actual.ScuSellAvg.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedScuSellAvgWeek()
    {
        // Assemble
        const int ExpectedValue = 0;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First();
        actual.ScuSellAvgWeek.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedScuSellAvgMonth()
    {
        // Assemble
        const int ExpectedValue = 0;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First();
        actual.ScuSellAvgMonth.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedGameVersion()
    {
        // Assemble
        const string ExpectedValue = "3.20";

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First();
        actual.GameVersion.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedCommodityName()
    {
        // Assemble
        const string ExpectedValue = "Astatine";

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First();
        actual.CommodityName.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedStarSystemName()
    {
        // Assemble
        const string ExpectedValue = "Stanton";

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First();
        actual.StarSystemName.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedPlanetName()
    {
        // Assemble
        const string ExpectedValue = "ArcCorp";

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First();
        actual.PlanetName.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedMoonName()
    {
        // Assemble
        const string ExpectedValue = "Wala";

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First();
        actual.MoonName.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedSpaceStationName()
    {
        // Assemble
        const string ExpectedValue = null;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First();
        actual.SpaceStationName.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedOutpostName()
    {
        // Assemble
        const string ExpectedValue = "ArcCorp Mining Area 056";

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First();
        actual.OutpostName.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommodityPricesAsync_ShouldHaveExpectedCityName()
    {
        // Assemble
        const string ExpectedValue = null;

        // Act
        var commodityPrices = await GetActualCommodityPricesAsyncValue().ConfigureAwait(false);

        // Assert
        commodityPrices.Should().NotBeNull();
        var actual = commodityPrices.First();
        actual.CityName.Should().Be(ExpectedValue);
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
    public async Task GetTerminalsAsync_ShouldHaveExpectedCountOf6()
    {
        // Assemble
        int starSystemId = 68;

        // Act
        var actual = await _webApiClient.GetTerminalsAsync(starSystemId).ConfigureAwait(false);

        // Assert
        actual.Should().HaveCount(6);
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
        const int ExpectedId = 1;

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
        const string ExpectedValue = "ARC-L1 - Admin Office";

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
        const string ExpectedValue = "ArcCorp";

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
}
