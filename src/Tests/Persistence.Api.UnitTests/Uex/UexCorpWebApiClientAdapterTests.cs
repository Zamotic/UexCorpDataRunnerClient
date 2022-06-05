using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using AutoMapper;
using System.Net;
using UexCorpDataRunner.Persistence.Api.Uex;
using Moq;
using UexCorpDataRunner.Persistence.Api.Uex.Maps;
using UexCorpDataRunner.Domain.DataRunner;

namespace Persistence.Api.UnitTests.Uex;
public class UexCorpWebApiClientAdapterTests
{
    MapperConfiguration _config;
    IUexCorpWebApiClientAdapter _adapter;

    public UexCorpWebApiClientAdapterTests()
    {
        var webApiClient = new UexCorpDataRunner.Persistence.Api.Mock.Uex.UexCorpWebApiClientMock();
        _config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<SystemProfile>();
                cfg.AddProfile<PlanetProfile>();
                cfg.AddProfile<SatelliteProfile>();
                cfg.AddProfile<CityProfile>();
                cfg.AddProfile<TradeportProfile>();
                cfg.AddProfile<CommodityProfile>();
            });
        var mapper = _config.CreateMapper();
        _adapter = new UexCorpWebApiClientAdapter(webApiClient, mapper);
    }

    [Fact]
    public void AssertConfigurationIsValid_MapperConfiguration_ReturnsTrue()
    {
        // Assert
        _config.AssertConfigurationIsValid();
    }

    [Fact] 
    public async Task GetSystemsAsync_UexCorpWebApiClientAdapter_ReturnsExpectedCount()
    {
        // Assemble
        const int ExpectedCount = 2;

        // Act
        var actual = await _adapter.GetSystemsAsync();

        // Assert
        actual.Count.Should().Be(ExpectedCount);
    }

    [Fact]
    public async Task GetSystemsAsync_UexCorpWebApiClientAdapter_FirstObjectHasExpectedValues()
    {
        // Assemble
        var expectedSystem = new UexCorpDataRunner.Domain.DataRunner.System()
        {
            Name = "Pyro",
            Code = "PY",
            IsAvailable = false,
            IsDefault = false,
            DateAdded = DateTimeOffset.Parse("12/25/2020, 11:25:15 PM +03:00"),
            DateModified = DateTime.MinValue
        };

        // Act
        var actual = await _adapter.GetSystemsAsync();

        // Assert
        actual.First().Should().BeEquivalentTo(expectedSystem);
    }

    [Fact]
    public async Task GetPlanetsAsync_UexCorpWebApiClientAdapter_ReturnsExpectedCount()
    {
        // Assemble
        const int ExpectedCount = 4;
        const string SystemCode = "ST";

        // Act
        var actual = await _adapter.GetPlanetsAsync(SystemCode);

        // Assert
        actual.Count.Should().Be(ExpectedCount);
    }

    [Fact]
    public async Task GetPlanetsAsync_UexCorpWebApiClientAdapter_FirstObjectHasExpectedValues()
    {
        // Assemble
        var expectedPlanet = new UexCorpDataRunner.Domain.DataRunner.Planet()
        {
            System = "ST",
            Name = "ArcCorp",
            Code = "ARC",
            IsAvailable = true,
            DateAdded = DateTimeOffset.Parse("12/25/2020, 11:25:15 PM +03:00"),
            DateModified = DateTime.MinValue
        };
        const string SystemCode = "ST";

        // Act
        var actual = await _adapter.GetPlanetsAsync(SystemCode);

        // Assert
        actual.First().Should().BeEquivalentTo(expectedPlanet);
    }

    [Fact]
    public async Task GetSatellitesAsync_UexCorpWebApiClientAdapter_ReturnsExpectedCount()
    {
        // Assemble
        const int ExpectedCount = 4;
        const string SystemCode = "ST";

        // Act
        var actual = await _adapter.GetSatellitesAsync(SystemCode);

        // Assert
        actual.Count.Should().Be(ExpectedCount);
    }

    [Fact]
    public async Task GetSatellitesAsync_UexCorpWebApiClientAdapter_FirstObjectHasExpectedValues()
    {
        // Assemble
        var expectedSatellite = new UexCorpDataRunner.Domain.DataRunner.Satellite()
        {
            System = "ST",
            Planet = "HUR",
            Name = "Aberdeen",
            Code = "ABER",
            IsAvailable = true,
            DateAdded = DateTimeOffset.Parse("12/25/2020, 11:25:15 PM +03:00"),
            DateModified = DateTime.MinValue
        };
        const string SystemCode = "ST";

        // Act
        var actual = await _adapter.GetSatellitesAsync(SystemCode);

        // Assert
        actual.First().Should().BeEquivalentTo(expectedSatellite);
    }

    [Fact]
    public async Task GetCitiesAsync_UexCorpWebApiClientAdapter_ReturnsExpectedCount()
    {
        // Assemble
        const int ExpectedCount = 4;
        const string SystemCode = "ST";

        // Act
        var actual = await _adapter.GetCitiesAsync(SystemCode);

        // Assert
        actual.Count.Should().Be(ExpectedCount);
    }

    [Fact]
    public async Task GetCitiesAsync_UexCorpWebApiClientAdapter_FirstObjectHasExpectedValues()
    {
        // Assemble
        var expectedCity = new UexCorpDataRunner.Domain.DataRunner.City()
        {
            System = "ST",
            Planet = "ARC",
            Name = "Area 18",
            Code = "A18",
            IsAvailable = true,
            DateAdded = DateTimeOffset.Parse("12/25/2020, 11:25:15 PM +03:00"),
            DateModified = DateTime.MinValue
        };
        const string SystemCode = "ST";

        // Act
        var actual = await _adapter.GetCitiesAsync(SystemCode);

        // Assert
        actual.First().Should().BeEquivalentTo(expectedCity);
    }

    [Fact]
    public async Task GetTradeportsAsync_UexCorpWebApiClientAdapter_ReturnsExpectedCount()
    {
        // Assemble
        const int ExpectedCount = 4;
        const string SystemCode = "ST";

        // Act
        var actual = await _adapter.GetTradeportsAsync(SystemCode);

        // Assert
        actual.Count.Should().Be(ExpectedCount);
    }

    [Fact]
    public async Task GetTradeportsAsync_UexCorpWebApiClientAdapter_FirstObjectHasExpectedValues()
    {
        // Assemble
        var expectedTradeport = new UexCorpDataRunner.Domain.DataRunner.Tradeport()
        {
            System = "ST",
            Planet = "ARC",
            Satellite = "WALA",
            City = null,
            Name = "ArcCorp Mining Area 045",
            NameShort = "ArcCorp 045",
            Code = "AM045",
            IsVisible = true,
            IsArmisticeZone = true,
            HasTrade = true,
            WelcomesOutlaws = false,
            HasRefinery = false,
            HasShops = false,
            IsRestrictedArea = false,
            HasMinables = false,
            DateAdded = DateTimeOffset.Parse("12/25/2020, 11:25:15 PM +03:00"),
            DateModified = DateTimeOffset.Parse("12/25/2020, 11:25:15 PM +03:00")
        };
        const string SystemCode = "ST";

        // Act
        var actual = await _adapter.GetTradeportsAsync(SystemCode);

        // Assert
        actual.First().Should().BeEquivalentTo(expectedTradeport);
    }

    [Fact]
    public async Task GetCommoditiesAsync_UexCorpWebApiClientAdapter_ReturnsExpectedCount()
    {
        // Assemble
        const int ExpectedCount = 6;

        // Act
        var actual = await _adapter.GetCommoditiesAsync();

        // Assert
        actual.Count.Should().Be(ExpectedCount);
    }

    [Fact]
    public async Task GetCommoditiesAsync_UexCorpWebApiClientAdapter_FirstObjectHasExpectedValues()
    {
        // Assemble
        var expectedCommodity = new UexCorpDataRunner.Domain.DataRunner.Commodity()
        {            
            Name = "Agricultural Supplies",
            Kind = "Agricultural",
            Code = "ACSU",
            BuyPrice = 1.01m,
            SellPrice = 1.21m,
            DateAdded = DateTimeOffset.Parse("12/25/2020, 11:25:15 PM +03:00"),
            DateModified = DateTimeOffset.Parse("6/4/2022, 8:00:06 PM +03:00")
        };

        // Act
        var actual = await _adapter.GetCommoditiesAsync();

        // Assert
        actual.First().Should().BeEquivalentTo(expectedCommodity);
    }
}
