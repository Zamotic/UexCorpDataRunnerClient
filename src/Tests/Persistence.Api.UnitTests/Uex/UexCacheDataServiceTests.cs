using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UexCorpDataRunner.Domain.Services;
using UexCorpDataRunner.Persistence.Api.Mock.Uex;
using UexCorpDataRunner.Persistence.Api.Uex;
using Xunit;
using Moq;
using AutoMapper;
using UexCorpDataRunner.Persistence.Api.Uex.Maps;

namespace UexCorpDataRunner.Persistence.Api.UnitTests.Uex;
public class UexCacheDataServiceTests
{
    IUexDataService _uexCacheDataService;
    UexCorpWebApiClientMock _webApiClientMock;

    public UexCacheDataServiceTests()
    {
        _webApiClientMock = new UexCorpWebApiClientMock();
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<SystemProfile>();
            cfg.AddProfile<PlanetProfile>();
            cfg.AddProfile<SatelliteProfile>();
            cfg.AddProfile<CityProfile>();
            cfg.AddProfile<TradeportProfile>();
            cfg.AddProfile<CommodityProfile>();
        });
        var mapper = config.CreateMapper();
        IUexCorpWebApiClientAdapter webApiClientAdapter = new UexCorpWebApiClientAdapter(_webApiClientMock, mapper);
        _uexCacheDataService = new UexCacheDataService(webApiClientAdapter);
    }

    [Fact]
    public async Task GetAllSystemsAsync_UexCacheDataService_ShouldCallWebApiOnce()
    {
        // Assemble

        // Act
        var actual1 = await _uexCacheDataService.GetAllSystemsAsync();
        var actual2 = await _uexCacheDataService.GetAllSystemsAsync();

        // Assert
        actual1.Should().BeSameAs(actual2);
        _webApiClientMock.Verify(w => w.GetSystemsAsync(), Times.Once());
    }

    [Fact]
    public async Task GetAllPlanetsAsync_UexCacheDataService_ShouldCallWebApiOnce()
    {
        // Assemble
        const string SystemCode = "ST";

        // Act
        var actual1 = await _uexCacheDataService.GetAllPlanetsAsync(SystemCode);
        var actual2 = await _uexCacheDataService.GetAllPlanetsAsync(SystemCode);

        // Assert
        actual1.Should().BeSameAs(actual2);
        _webApiClientMock.Verify(w => w.GetPlanetsAsync(SystemCode), Times.Once());
    }

    [Fact]
    public async Task GetAllCitiesAsync_UexCacheDataService_ShouldCallWebApiOnce()
    {
        // Assemble
        const string SystemCode = "ST";

        // Act
        var actual1 = await _uexCacheDataService.GetAllCitiesAsync(SystemCode);
        var actual2 = await _uexCacheDataService.GetAllCitiesAsync(SystemCode);

        // Assert
        actual1.Should().BeSameAs(actual2);
        _webApiClientMock.Verify(w => w.GetCitiesAsync(SystemCode), Times.Once());
    }

    [Fact]
    public async Task GetAllTradeportsAsync_UexCacheDataService_ShouldCallWebApiOnce()
    {
        // Assemble
        const string SystemCode = "ST";

        // Act
        var actual1 = await _uexCacheDataService.GetAllTradeportsAsync(SystemCode);
        var actual2 = await _uexCacheDataService.GetAllTradeportsAsync(SystemCode);

        // Assert
        actual1.Should().BeSameAs(actual2);
        _webApiClientMock.Verify(w => w.GetTradeportsAsync(SystemCode), Times.Once());
    }

    [Fact]
    public async Task GetAllCommoditiesAsync_UexCacheDataService_ShouldCallWebApiOnce()
    {
        // Assemble

        // Act
        var actual1 = await _uexCacheDataService.GetAllCommoditiesAsync();
        var actual2 = await _uexCacheDataService.GetAllCommoditiesAsync();

        // Assert
        actual1.Should().BeEquivalentTo(actual2);
        _webApiClientMock.Verify(w => w.GetCommoditiesAsync(), Times.Exactly(2));
    }

    [Fact]
    public async Task GetTradeportAsync_UexCacheDataService_ShouldCallWebApiOnce()
    {
        // Assemble
        const string TradeportCode = "AM056";

        // Act
        var actual1 = await _uexCacheDataService.GetTradeportAsync(TradeportCode);
        var actual2 = await _uexCacheDataService.GetTradeportAsync(TradeportCode);

        // Assert
        actual1.Should().BeEquivalentTo(actual2);
        _webApiClientMock.Verify(w => w.GetTradeportAsync(TradeportCode), Times.Exactly(2));
    }
}
