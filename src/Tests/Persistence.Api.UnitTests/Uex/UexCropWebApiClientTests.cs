 using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UexCorpDataRunner.Domain.Services;
using UexCorpDataRunner.Domain.Settings;
using UexCorpDataRunner.Persistence.Api.Mock.Uex;
using UexCorpDataRunner.Persistence.Api.Uex;
using UexCorpDataRunner.Persistence.Api.Uex.DataTransferObjects;
using Xunit;

namespace UexCorpDataRunner.Persistence.Api.UnitTests.Uex;
public class UexCropWebApiClientTests
{
    DateTimeOffset _dateAdded = new DateTimeOffset(2020, 12, 26, 2, 25, 15, TimeSpan.Zero);
    DateTimeOffset _dateModified = new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero);

    IUexCorpWebApiClient _webApiClient;
    UexWebApiMockHttpMessageHandler _uexWebApiMockHttpMessageHandler;

    public UexCropWebApiClientTests()
    {
        Mock<IUexCorpWebApiConfiguration> mockWebConfiguration = new Mock<IUexCorpWebApiConfiguration>();
        mockWebConfiguration.SetupGet(g => g.DataRunnerEndpointPath).Returns(string.Empty);
        mockWebConfiguration.SetupGet(g => g.WebApiEndPointUrl).Returns("https://portal.uexcorp.space/api/");

        Mock<ISettingsService> settingsService = new Mock<ISettingsService>();
        settingsService.SetupGet(g => g.Settings).Returns(new SettingsValues() { UserApiKey = "TestApiKey" });

        var httpClientFactory = new UexMockHttpClientFactory(mockWebConfiguration.Object, settingsService.Object);
        _uexWebApiMockHttpMessageHandler = httpClientFactory.GetMockHttpMessageHandler();
        _webApiClient = new UexCorpWebApiClient(mockWebConfiguration.Object, httpClientFactory);
    }

    [Fact]
    public async Task GetSystemsAsync_ShouldHaveExpectedCountOf2()
    {
        // Assemble

        // Act
        var actual = await _webApiClient.GetSystemsAsync().ConfigureAwait(false);

        // Assert
        actual.Should().HaveCount(2);
    }

    private static ICollection<SystemDto> _ActualGetSystemsAsyncValue = new List<SystemDto>();
    private async Task<ICollection<SystemDto>> GetActualSystemsAsyncValue()
    {
        if (_ActualGetSystemsAsyncValue.Any() == false)
        {
            _ActualGetSystemsAsyncValue = await _webApiClient.GetSystemsAsync().ConfigureAwait(false);
        }

        return _ActualGetSystemsAsyncValue;
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
        actual.DateAdded.Should().Be(ExpectedValue);
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
        actual.DateModified.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetPlanetsAsync_ShouldHaveExpectedCountOf4()
    {
        // Assemble
        const string systemCode = "ST";

        // Act
        var actual = await _webApiClient.GetPlanetsAsync(systemCode).ConfigureAwait(false);

        // Assert
        actual.Should().HaveCount(4);
    }


    private static ICollection<PlanetDto> _ActualGetPlanetsAsyncValue = new List<PlanetDto>();
    private async Task<ICollection<PlanetDto>> GetActualPlanetsAsyncValue()
    {
        const string systemCode = "ST";
        if (_ActualGetPlanetsAsyncValue.Any() == false)
        {
            _ActualGetPlanetsAsyncValue = await _webApiClient.GetPlanetsAsync(systemCode).ConfigureAwait(false);
        }

        return _ActualGetPlanetsAsyncValue;
    }

    [Fact]
    public async Task GetPlanetsAsync_ShouldHaveExpectedSystem()
    {
        // Assemble
        const string ExpectedValue = "ST";

        // Act
        var planets = await GetActualPlanetsAsyncValue().ConfigureAwait(false);

        // Assert
        planets.Should().NotBeNull();
        var actual = planets.First();
        actual.System.Should().Be(ExpectedValue);
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
    public async Task GetPlanetsAsync_ShouldHaveExpectedDateAdded()
    {
        // Assemble
        DateTimeOffset ExpectedValue = _dateAdded;

        // Act
        var planets = await GetActualPlanetsAsyncValue().ConfigureAwait(false);

        // Assert
        planets.Should().NotBeNull();
        var actual = planets.First();
        actual.DateAdded.Should().Be(ExpectedValue);
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
        actual.DateModified.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetSatellitesAsync_ShouldHaveExpectedCountOf12()
    {
        // Assemble
        const string systemCode = "ST";

        // Act
        var actual = await _webApiClient.GetSatellitesAsync(systemCode).ConfigureAwait(false);

        // Assert
        actual.Should().HaveCount(12);
    }


    private static ICollection<SatelliteDto> _ActualGetSatellitesAsyncValue = new List<SatelliteDto>();
    private async Task<ICollection<SatelliteDto>> GetActualSatellitesAsyncValue()
    {
        const string systemCode = "ST";
        if (_ActualGetSatellitesAsyncValue.Any() == false)
        {
            _ActualGetSatellitesAsyncValue = await _webApiClient.GetSatellitesAsync(systemCode).ConfigureAwait(false);
        }

        return _ActualGetSatellitesAsyncValue;
    }

    [Fact]
    public async Task GetSatellitesAsync_ShouldHaveExpectedSystem()
    {
        // Assemble
        const string ExpectedValue = "ST";

        // Act
        var satellites = await GetActualSatellitesAsyncValue().ConfigureAwait(false);

        // Assert
        satellites.Should().NotBeNull();
        var actual = satellites.First();
        actual.System.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetSatellitesAsync_ShouldHaveExpectedPlanet()
    {
        // Assemble
        const string ExpectedValue = "HUR";

        // Act
        var satellites = await GetActualSatellitesAsyncValue().ConfigureAwait(false);

        // Assert
        satellites.Should().NotBeNull();
        var actual = satellites.First();
        actual.Planet.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetSatellitesAsync_ShouldHaveExpectedName()
    {
        // Assemble
        const string ExpectedValue = "Aberdeen";

        // Act
        var satellites = await GetActualSatellitesAsyncValue().ConfigureAwait(false);

        // Assert
        satellites.Should().NotBeNull();
        var actual = satellites.First();
        actual.Name.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetSatellitesAsync_ShouldHaveExpectedCode()
    {
        // Assemble
        const string ExpectedValue = "ABER";

        // Act
        var satellites = await GetActualSatellitesAsyncValue().ConfigureAwait(false);

        // Assert
        satellites.Should().NotBeNull();
        var actual = satellites.First();
        actual.Code.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetSatellitesAsync_ShouldHaveIsAvailable()
    {
        // Assemble
        const bool ExpectedValue = true;

        // Act
        var satellites = await GetActualSatellitesAsyncValue().ConfigureAwait(false);

        // Assert
        satellites.Should().NotBeNull();
        var actual = satellites.First();
        actual.IsAvailable.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetSatellitesAsync_ShouldHaveDateAdded()
    {
        // Assemble
        DateTimeOffset ExpectedValue = _dateAdded;

        // Act
        var satellites = await GetActualSatellitesAsyncValue().ConfigureAwait(false);

        // Assert
        satellites.Should().NotBeNull();
        var actual = satellites.First();
        actual.DateAdded.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetSatellitesAsync_ShouldHaveDateModified()
    {
        // Assemble
        DateTimeOffset ExpectedValue = _dateModified;

        // Act
        var satellites = await GetActualSatellitesAsyncValue().ConfigureAwait(false);

        // Assert
        satellites.Should().NotBeNull();
        var actual = satellites.First();
        actual.DateModified.Should().Be(ExpectedValue);
    }


    [Fact]
    public async Task GetCitiesAsync_ShouldHaveExpectedCountOf4()
    {
        // Assemble
        const string systemCode = "ST";

        // Act
        var actual = await _webApiClient.GetCitiesAsync(systemCode).ConfigureAwait(false);

        // Assert
        actual.Should().HaveCount(4);
    }


    private static ICollection<CityDto> _ActualGetCitiesAsyncValue = new List<CityDto>();
    private async Task<ICollection<CityDto>> GetActualCitiesAsyncValue()
    {
        const string systemCode = "ST";
        if (_ActualGetCitiesAsyncValue.Any() == false)
        {
            _ActualGetCitiesAsyncValue = await _webApiClient.GetCitiesAsync(systemCode).ConfigureAwait(false);
        }

        return _ActualGetCitiesAsyncValue;
    }

    [Fact]
    public async Task GetCitiesAsync_ShouldHaveExpectedSystem()
    {
        // Assemble
        const string ExpectedValue = "ST";

        // Act
        var cities = await GetActualCitiesAsyncValue().ConfigureAwait(false);

        // Assert
        cities.Should().NotBeNull();
        var actual = cities.First();
        actual.System.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCitiesAsync_ShouldHaveExpectedPlanet()
    {
        // Assemble
        const string ExpectedValue = "ARC";

        // Act
        var cities = await GetActualCitiesAsyncValue().ConfigureAwait(false);

        // Assert
        cities.Should().NotBeNull();
        var actual = cities.First();
        actual.Planet.Should().Be(ExpectedValue);
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
        const string ExpectedValue = "A18";

        // Act
        var cities = await GetActualCitiesAsyncValue().ConfigureAwait(false);

        // Assert
        cities.Should().NotBeNull();
        var actual = cities.First();
        actual.Code.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCitiesAsync_ShouldHaveIsAvailable()
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
    public async Task GetCitiesAsync_ShouldHaveDateAdded()
    {
        // Assemble
        DateTimeOffset ExpectedValue = _dateAdded;

        // Act
        var cities = await GetActualCitiesAsyncValue().ConfigureAwait(false);

        // Assert
        cities.Should().NotBeNull();
        var actual = cities.First();
        actual.DateAdded.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCitiesAsync_ShouldHaveDateModified()
    {
        // Assemble
        DateTimeOffset ExpectedValue = _dateModified;

        // Act
        var cities = await GetActualCitiesAsyncValue().ConfigureAwait(false);

        // Assert
        cities.Should().NotBeNull();
        var actual = cities.First();
        actual.DateModified.Should().Be(ExpectedValue);
    }


    [Fact]
    public async Task GetTradeportsAsync_ShouldHaveExpectedCountOf82()
    {
        // Assemble
        const string systemCode = "ST";

        // Act
        var actual = await _webApiClient.GetTradeportsAsync(systemCode).ConfigureAwait(false);

        // Assert
        actual.Should().HaveCount(82);
    }


    private static ICollection<TradeportDto> _ActualGetTradeportsAsyncValue = new List<TradeportDto>();
    private async Task<ICollection<TradeportDto>> GetActualTradeportsAsyncValue()
    {
        const string SystemCode = "ST";
        if (_ActualGetTradeportsAsyncValue.Any() == false)
        {
            _ActualGetTradeportsAsyncValue = await _webApiClient.GetTradeportsAsync(SystemCode).ConfigureAwait(false);
        }

        return _ActualGetTradeportsAsyncValue;
    }

    [Fact]
    public async Task GetTradeportsAsync_FirstItem_ShouldHaveExpectedSystem()
    {
        // Assemble
        const string ExpectedValue = "ST";

        // Act
        var tradeport = await GetActualTradeportsAsyncValue().ConfigureAwait(false);

        // Assert
        tradeport.Should().NotBeNull();
        var actual = tradeport.First();
        actual.System.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetTradeportsAsync_FirstItem_ShouldHaveExpectedPlanet()
    {
        // Assemble
        const string ExpectedValue = "ARC";

        // Act
        var tradeport = await GetActualTradeportsAsyncValue().ConfigureAwait(false);

        // Assert
        tradeport.Should().NotBeNull();
        var actual = tradeport.First();
        actual.Planet.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetTradeportsAsync_FirstItem_ShouldHaveExpectedSatellite()
    {
        // Assemble
        const string ExpectedValue = "WALA";

        // Act
        var tradeport = await GetActualTradeportsAsyncValue().ConfigureAwait(false);

        // Assert
        tradeport.Should().NotBeNull();
        var actual = tradeport.First();
        actual.Satellite.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetTradeportsAsync_FirstItem_ShouldHaveExpectedCity()
    {
        // Assemble

        // Act
        var tradeport = await GetActualTradeportsAsyncValue().ConfigureAwait(false);

        // Assert
        tradeport.Should().NotBeNull();
        var actual = tradeport.First();
        actual.City.Should().BeNull();
    }

    [Fact]
    public async Task GetTradeportsAsync_FirstItem_ShouldHaveExpectedCode()
    {
        // Assemble
        const string ExpectedValue = "AM045";

        // Act
        var tradeport = await GetActualTradeportsAsyncValue().ConfigureAwait(false);

        // Assert
        tradeport.Should().NotBeNull();
        var actual = tradeport.First();
        actual.Code.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetTradeportsAsync_FirstItem_ShouldHaveExpectedName()
    {
        // Assemble
        const string ExpectedValue = "ArcCorp Mining Area 045";

        // Act
        var tradeport = await GetActualTradeportsAsyncValue().ConfigureAwait(false);

        // Assert
        tradeport.Should().NotBeNull();
        var actual = tradeport.First();
        actual.Name.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetTradeportsAsync_FirstItem_ShouldHaveExpectedShortName()
    {
        // Assemble
        const string ExpectedValue = "ArcCorp 045";

        // Act
        var tradeport = await GetActualTradeportsAsyncValue().ConfigureAwait(false);

        // Assert
        tradeport.Should().NotBeNull();
        var actual = tradeport.First();
        actual.NameShort.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetTradeportsAsync_FirstItem_ShouldHaveExpectedIsVisible()
    {
        // Assemble
        const bool ExpectedValue = true;

        // Act
        var tradeport = await GetActualTradeportsAsyncValue().ConfigureAwait(false);

        // Assert
        tradeport.Should().NotBeNull();
        var actual = tradeport.First();
        actual.IsVisible.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetTradeportsAsync_FirstItem_ShouldHaveExpectedIsArmisticeZone()
    {
        // Assemble
        const bool ExpectedValue = true;

        // Act
        var tradeport = await GetActualTradeportsAsyncValue().ConfigureAwait(false);

        // Assert
        tradeport.Should().NotBeNull();
        var actual = tradeport.First();
        actual.IsArmisticeZone.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetTradeportsAsync_FirstItem_ShouldHaveExpectedHasTrade()
    {
        // Assemble
        const bool ExpectedValue = true;

        // Act
        var tradeport = await GetActualTradeportsAsyncValue().ConfigureAwait(false);

        // Assert
        tradeport.Should().NotBeNull();
        var actual = tradeport.First();
        actual.HasTrade.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetTradeportsAsync_FirstItem_ShouldHaveExpectedWelcomesOutlaws()
    {
        // Assemble
        const bool ExpectedValue = false;

        // Act
        var tradeport = await GetActualTradeportsAsyncValue().ConfigureAwait(false);

        // Assert
        tradeport.Should().NotBeNull();
        var actual = tradeport.First();
        actual.WelcomesOutlaws.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetTradeportsAsync_FirstItem_ShouldHaveExpectedHasRefinery()
    {
        // Assemble
        const bool ExpectedValue = false;

        // Act
        var tradeport = await GetActualTradeportsAsyncValue().ConfigureAwait(false);

        // Assert
        tradeport.Should().NotBeNull();
        var actual = tradeport.First();
        actual.HasRefinery.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetTradeportsAsync_FirstItem_ShouldHaveExpectedHasShops()
    {
        // Assemble
        const bool ExpectedValue = false;

        // Act
        var tradeport = await GetActualTradeportsAsyncValue().ConfigureAwait(false);

        // Assert
        tradeport.Should().NotBeNull();
        var actual = tradeport.First();
        actual.HasShops.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetTradeportsAsync_FirstItem_ShouldHaveExpectedIsRestrictedArea()
    {
        // Assemble
        const bool ExpectedValue = false;

        // Act
        var tradeport = await GetActualTradeportsAsyncValue().ConfigureAwait(false);

        // Assert
        tradeport.Should().NotBeNull();
        var actual = tradeport.First();
        actual.IsRestrictedArea.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetTradeportsAsync_FirstItem_ShouldHaveExpectedHasMinables()
    {
        // Assemble
        const bool ExpectedValue = false;

        // Act
        var tradeport = await GetActualTradeportsAsyncValue().ConfigureAwait(false);

        // Assert
        tradeport.Should().NotBeNull();
        var actual = tradeport.First();
        actual.HasMinables.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetTradeportsAsync_FirstItem_ShouldHaveExpectedDateAdded()
    {
        // Assemble
        DateTimeOffset ExpectedValue = _dateAdded;

        // Act
        var tradeport = await GetActualTradeportsAsyncValue().ConfigureAwait(false);

        // Assert
        tradeport.Should().NotBeNull();
        var actual = tradeport.First();
        actual.DateAdded.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetTradeportsAsync_FirstItem_ShouldHaveExpectedDateModified()
    {
        // Assemble
        DateTimeOffset ExpectedValue = new DateTimeOffset(2022, 6, 8, 19, 0, 47, TimeSpan.Zero);

        // Act
        var tradeport = await GetActualTradeportsAsyncValue().ConfigureAwait(false);

        // Assert
        tradeport.Should().NotBeNull();
        var actual = tradeport.First();
        actual.DateModified.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetTradeportsAsync_FirstItemsFirstPrice_ShouldHaveExpectedKey()
    {
        // Assemble
        const string ExpectedValue = "PRFO";

        // Act
        var tradeport = await GetActualTradeportsAsyncValue().ConfigureAwait(false);

        // Assert
        tradeport.Should().NotBeNull();
        var firstTradeport = tradeport.First();
        firstTradeport.Should().NotBeNull();
        var actual = firstTradeport.Prices?.First();
        actual.HasValue.Should().BeTrue();
        actual?.Key.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetTradeportsAsync_FirstItemsFirstPrice_ShouldHaveExpectedName()
    {
        // Assemble
        const string ExpectedValue = "Processed Food";

        // Act
        var tradeport = await GetActualTradeportsAsyncValue().ConfigureAwait(false);

        // Assert
        tradeport.Should().NotBeNull();
        var firstTradeport = tradeport.First();
        firstTradeport.Should().NotBeNull();
        var actual = firstTradeport.Prices?.First();
        actual.HasValue.Should().BeTrue();
        actual?.Value.Name.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetTradeportsAsync_FirstItemsFirstPrice_ShouldHaveExpectedKind()
    {
        // Assemble
        const string ExpectedValue = "Food";

        // Act
        var tradeport = await GetActualTradeportsAsyncValue().ConfigureAwait(false);

        // Assert
        tradeport.Should().NotBeNull();
        var firstTradeport = tradeport.First();
        firstTradeport.Should().NotBeNull();
        var actual = firstTradeport.Prices?.First();
        actual.HasValue.Should().BeTrue();
        actual?.Value.Kind.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetTradeportsAsync_FirstItemsFirstPrice_ShouldHaveExpectedOperation()
    {
        // Assemble
        const string ExpectedValue = "sell";

        // Act
        var tradeport = await GetActualTradeportsAsyncValue().ConfigureAwait(false);

        // Assert
        tradeport.Should().NotBeNull();
        var firstTradeport = tradeport.First();
        firstTradeport.Should().NotBeNull();
        var actual = firstTradeport.Prices?.First();
        actual.HasValue.Should().BeTrue();
        actual?.Value.Operation.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetTradeportsAsync_FirstItemsFirstPrice_ShouldHaveExpectedPriceBuy()
    {
        // Assemble
        const decimal ExpectedValue = 0m;

        // Act
        var tradeport = await GetActualTradeportsAsyncValue().ConfigureAwait(false);

        // Assert
        tradeport.Should().NotBeNull();
        var firstTradeport = tradeport.First();
        firstTradeport.Should().NotBeNull();
        var actual = firstTradeport.Prices?.First();
        actual.HasValue.Should().BeTrue();
        actual?.Value.PriceBuy.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetTradeportsAsync_FirstItemsFirstPrice_ShouldHaveExpectedPriceSell()
    {
        // Assemble
        const decimal ExpectedValue = 1.5m;

        // Act
        var tradeport = await GetActualTradeportsAsyncValue().ConfigureAwait(false);

        // Assert
        tradeport.Should().NotBeNull();
        var firstTradeport = tradeport.First();
        firstTradeport.Should().NotBeNull();
        var actual = firstTradeport.Prices?.First();
        actual.HasValue.Should().BeTrue();
        actual?.Value.PriceSell.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetTradeportsAsync_FirstItemsFirstPrice_ShouldHaveExpectedDateUpdate()
    {
        // Assemble
        DateTimeOffset ExpectedValue = new DateTimeOffset(2022, 5, 11, 20, 0, 29, TimeSpan.Zero);

        // Act
        var tradeport = await GetActualTradeportsAsyncValue().ConfigureAwait(false);

        // Assert
        tradeport.Should().NotBeNull();
        var firstTradeport = tradeport.First();
        firstTradeport.Should().NotBeNull();
        var actual = firstTradeport.Prices?.First();
        actual.HasValue.Should().BeTrue();
        actual?.Value.DateUpdate.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetTradeportsAsync_FirstItemsFirstPrice_ShouldHaveExpectedIsUpdated()
    {
        // Assemble
        const bool ExpectedValue = true;

        // Act
        var tradeport = await GetActualTradeportsAsyncValue().ConfigureAwait(false);

        // Assert
        tradeport.Should().NotBeNull();
        var firstTradeport = tradeport.First();
        firstTradeport.Should().NotBeNull();
        var actual = firstTradeport.Prices?.First();
        actual.HasValue.Should().BeTrue();
        actual?.Value.IsUpdated.Should().Be(ExpectedValue);
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
    public async Task GetCommoditiesAsync_ShouldHaveExpectedCountOf69()
    {
        // Assemble

        // Act
        var actual = await _webApiClient.GetCommoditiesAsync().ConfigureAwait(false);

        // Assert
        actual.Should().HaveCount(69);
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
        const string ExpectedValue = "ACSU";

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
        const decimal ExpectedValue = 1.01m;

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
        const decimal ExpectedValue = 1.21m;

        // Act
        var commodities = await GetActualCommoditiesAsyncValue().ConfigureAwait(false);

        // Assert
        commodities.Should().NotBeNull();
        var actual = commodities.First();
        actual.SellPrice.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommoditiesAsync_ShouldHaveDateAdded()
    {
        // Assemble
        DateTimeOffset ExpectedValue = _dateAdded;

        // Act
        var commodities = await GetActualCommoditiesAsyncValue().ConfigureAwait(false);

        // Assert
        commodities.Should().NotBeNull();
        var actual = commodities.First();
        actual.DateAdded.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetCommoditiesAsync_ShouldHaveDateModified()
    {
        // Assemble
        DateTimeOffset ExpectedValue = new DateTimeOffset(2022, 07, 02, 00, 00, 11, TimeSpan.Zero);

        // Act
        var commodities = await GetActualCommoditiesAsyncValue().ConfigureAwait(false);

        // Assert
        commodities.Should().NotBeNull();
        var actual = commodities.First();
        actual.DateModified.Should().Be(ExpectedValue);
    }


    private static TradeportDto _ActualGetTradeportAsyncValue = new TradeportDto();
    private async Task<TradeportDto> GetActualTradeportAsyncValue()
    {
        const string tradeportCode = "AM056";
        if (string.IsNullOrEmpty(_ActualGetTradeportAsyncValue.Code) == true)
        {
            _ActualGetTradeportAsyncValue = await _webApiClient.GetTradeportAsync(tradeportCode).ConfigureAwait(false);
        }

        return _ActualGetTradeportAsyncValue;
    }

    [Fact]
    public async Task GetTradeportAsync_ShouldHaveExpectedSystem()
    {
        // Assemble
        const string ExpectedValue = "ST";

        // Act
        var actual = await GetActualTradeportAsyncValue().ConfigureAwait(false);

        // Assert
        actual.Should().NotBeNull();
        actual.System.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetTradeportAsync_ShouldHaveExpectedPlanet()
    {
        // Assemble
        const string ExpectedValue = "ARC";

        // Act
        var actual = await GetActualTradeportAsyncValue().ConfigureAwait(false);

        // Assert
        actual.Should().NotBeNull();
        actual.Planet.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetTradeportAsync_ShouldHaveExpectedSatellite()
    {
        // Assemble
        const string ExpectedValue = "WALA";

        // Act
        var actual = await GetActualTradeportAsyncValue().ConfigureAwait(false);

        // Assert
        actual.Should().NotBeNull();
        actual.Satellite.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetTradeportAsync_ShouldHaveExpectedCity()
    {
        // Assemble

        // Act
        var actual = await GetActualTradeportAsyncValue().ConfigureAwait(false);

        // Assert
        actual.Should().NotBeNull();
        actual.City.Should().BeNull();
    }

    [Fact]
    public async Task GetTradeportAsync_ShouldHaveExpectedName()
    {
        // Assemble
        const string ExpectedValue = "ArcCorp Mining Area 056";

        // Act
        var actual = await GetActualTradeportAsyncValue().ConfigureAwait(false);

        // Assert
        actual.Should().NotBeNull();
        actual.Name.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetTradeportAsync_ShouldHaveExpectedCode()
    {
        // Assemble
        const string ExpectedValue = "AM056";

        // Act
        var actual = await GetActualTradeportAsyncValue().ConfigureAwait(false);

        // Assert
        actual.Should().NotBeNull();
        actual.Code.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetTradeportAsync_ShouldHaveExpectedNameShort()
    {
        // Assemble
        const string ExpectedValue = "ArcCorp 056";

        // Act
        var actual = await GetActualTradeportAsyncValue().ConfigureAwait(false);

        // Assert
        actual.Should().NotBeNull();
        actual.NameShort.Should().Be(ExpectedValue);
    }

    public async Task GetTradeportAsync_ShouldHaveExpectedIsVisible()
    {
        // Assemble
        const bool ExpectedValue = true;

        // Act
        var actual = await GetActualTradeportAsyncValue().ConfigureAwait(false);

        // Assert
        actual.Should().NotBeNull();
        actual.IsVisible.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetTradeportAsync_ShouldHaveExpectedIsArmisticeZone()
    {
        // Assemble
        const bool ExpectedValue = true;

        // Act
        var actual = await GetActualTradeportAsyncValue().ConfigureAwait(false);

        // Assert
        actual.Should().NotBeNull();
        actual.IsArmisticeZone.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetTradeportAsync_ShouldHaveExpectedHasTrade()
    {
        // Assemble
        const bool ExpectedValue = true;

        // Act
        var actual = await GetActualTradeportAsyncValue().ConfigureAwait(false);

        // Assert
        actual.Should().NotBeNull();
        actual.HasTrade.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetTradeportAsync_ShouldHaveExpectedWelcomesOutlaws()
    {
        // Assemble
        const bool ExpectedValue = false;

        // Act
        var actual = await GetActualTradeportAsyncValue().ConfigureAwait(false);

        // Assert
        actual.Should().NotBeNull();
        actual.WelcomesOutlaws.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetTradeportAsync_ShouldHaveExpectedHasRefinery()
    {
        // Assemble
        const bool ExpectedValue = false;

        // Act
        var actual = await GetActualTradeportAsyncValue().ConfigureAwait(false);

        // Assert
        actual.Should().NotBeNull();
        actual.HasRefinery.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetTradeportAsync_ShouldHaveExpectedHasShops()
    {
        // Assemble
        const bool ExpectedValue = false;

        // Act
        var actual = await GetActualTradeportAsyncValue().ConfigureAwait(false);

        // Assert
        actual.Should().NotBeNull();
        actual.HasShops.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetTradeportAsync_ShouldHaveExpectedIsRestrictedArea()
    {
        // Assemble
        const bool ExpectedValue = false;

        // Act
        var actual = await GetActualTradeportAsyncValue().ConfigureAwait(false);

        // Assert
        actual.Should().NotBeNull();
        actual.IsRestrictedArea.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetTradeportAsync_ShouldHaveExpectedHasMinables()
    {
        // Assemble
        const bool ExpectedValue = false;

        // Act
        var actual = await GetActualTradeportAsyncValue().ConfigureAwait(false);

        // Assert
        actual.Should().NotBeNull();
        actual.HasMinables.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetTradeportAsync_ShouldHaveExpectedDateAdded()
    {
        // Assemble
        DateTimeOffset ExpectedValue = _dateAdded;

        // Act
        var actual = await GetActualTradeportAsyncValue().ConfigureAwait(false);

        // Assert
        actual.Should().NotBeNull();
        actual.DateAdded.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task GetTradeportAsync_ShouldHaveExpectedDateModified()
    {
        // Assemble
        DateTimeOffset ExpectedValue = new DateTimeOffset(2022, 6, 7, 20, 0, 22, TimeSpan.Zero);

        // Act
        var actual = await GetActualTradeportAsyncValue().ConfigureAwait(false);

        // Assert
        actual.Should().NotBeNull();
        actual.DateModified.Should().Be(ExpectedValue);
    }


    private static UexResponseDto<string> _ActualSubmitPriceReportAsyncValue = new UexResponseDto<string>();
    private async Task<UexResponseDto<string>> GetSubmitPriceReportAsyncValue()
    {
        if (string.IsNullOrEmpty(_ActualSubmitPriceReportAsyncValue.Status) == true)
        {
            PriceReportDto reportDto = new PriceReportDto()
            {
                CommodityCode = "PRFO",
                TradeportCode = "AM056",
                Operation = "sell",
                Price = "1.5",
                AccessCode = "c5e000",
                Confirm = false
            };

            _ActualSubmitPriceReportAsyncValue = await _webApiClient.SubmitPriceReportAsync(reportDto).ConfigureAwait(false);
        }

        return _ActualSubmitPriceReportAsyncValue;
    }

    [Fact]
    public async Task SubmitPriceReportAsync_ShouldHaveExpectedStatus()
    {
        // Assemble
        const string ExpectedValue = "ok";

        // Act
        var actual = await GetSubmitPriceReportAsyncValue().ConfigureAwait(false);

        // Assert
        actual.Should().NotBeNull();
        actual.Status.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task SubmitPriceReportAsync_ShouldHaveExpectedCode()
    {
        // Assemble
        const int ExpectedValue = 200;

        // Act
        var actual = await GetSubmitPriceReportAsyncValue().ConfigureAwait(false);

        // Assert
        actual.Should().NotBeNull();
        actual.Code.Should().Be(ExpectedValue);
    }

    [Fact]
    public async Task SubmitPriceReportAsync_ShouldHaveExpectedData()
    {
        // Assemble
        const string ExpectedValue = "1234";

        // Act
        var actual = await GetSubmitPriceReportAsyncValue().ConfigureAwait(false);

        // Assert
        actual.Should().NotBeNull();
        actual.Data.Should().Be(ExpectedValue);
    }
}
