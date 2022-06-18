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
using System.Collections.ObjectModel;
using UexCorpDataRunner.Domain.DataRunner;

namespace UexCorpDataRunner.Persistence.Api.UnitTests.Uex;
public class UexCacheDataServiceTests
{
    IUexDataService _uexCacheDataService;
    Mock<IUexCorpWebApiClientAdapter> _mockWebApiClientAdapter;

    public UexCacheDataServiceTests()
    {
        _mockWebApiClientAdapter = new Mock<IUexCorpWebApiClientAdapter>();
        InitilizeMockWebApiClientAdapter();

        _uexCacheDataService = new UexCacheDataService(_mockWebApiClientAdapter.Object);
    }

    private void InitilizeMockWebApiClientAdapter()
    {
        DateTimeOffset _dateAdded = new DateTimeOffset(2020, 12, 26, 2, 25, 15, TimeSpan.Zero);
        DateTimeOffset _dateModified = new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero);

        _mockWebApiClientAdapter.Setup(s => s.GetSystemsAsync()).ReturnsAsync(
            new ReadOnlyCollection<Domain.DataRunner.System>(new List<Domain.DataRunner.System>()
            {
                new Domain.DataRunner.System() { Name = "Pyro", Code = "PY", IsAvailable = false, IsDefault = false, DateAdded = _dateAdded, DateModified = _dateModified },
                new Domain.DataRunner.System() { Name = "Stanton", Code = "ST", IsAvailable = true, IsDefault = true, DateAdded = _dateAdded, DateModified = _dateModified }
            }));

        _mockWebApiClientAdapter.Setup(s => s.GetPlanetsAsync(It.Is<string>(x => x.Equals("ST")))).ReturnsAsync(
            new ReadOnlyCollection<Planet>(
                new List<Planet>()
                {
                    new Planet() { System = "ST", Name = "ArcCorp", Code = "ARC", IsAvailable = true, DateAdded = _dateAdded, DateModified = _dateModified },
                    new Planet() { System = "ST", Name = "Crusader", Code = "CRU", IsAvailable = true, DateAdded = _dateAdded, DateModified = _dateModified },
                    new Planet() { System = "ST", Name = "Hurston", Code = "HUR", IsAvailable = true, DateAdded = _dateAdded, DateModified = _dateModified },
                    new Planet() { System = "ST", Name = "microTech", Code = "MIC", IsAvailable = true, DateAdded = _dateAdded, DateModified = _dateModified }
                })
            );

        _mockWebApiClientAdapter.Setup(s => s.GetCitiesAsync(It.Is<string>(x => x.Equals("ST")))).ReturnsAsync(
            new ReadOnlyCollection<City>(
                new List<City>()
                {
                    new City() { System = "ST", Planet = "ARC", Name = "Area 18", Code = "A18", IsAvailable = true, DateAdded = _dateAdded, DateModified = _dateModified },
                    new City() { System = "ST", Planet = "HUR", Name = "Lorville", Code = "LOR", IsAvailable = true, DateAdded = _dateAdded, DateModified = _dateModified },
                    new City() { System = "ST", Planet = "MIC", Name = "New Babbage", Code = "NBB", IsAvailable = true, DateAdded = _dateAdded, DateModified = _dateModified },
                    new City() { System = "ST", Planet = "CRU", Name = "Orison", Code = "ORI", IsAvailable = true, DateAdded = _dateAdded, DateModified = _dateModified }
                })
            );

        _mockWebApiClientAdapter.Setup(s => s.GetSatellitesAsync(It.Is<string>(x => x.Equals("ST")))).ReturnsAsync(
            new ReadOnlyCollection<Satellite>(
                new List<Satellite>()
                {
                    new Satellite() { System = "ST", Planet = "HUR", Name = "Aberdeen", Code = "ABER", IsAvailable = true, DateAdded = _dateAdded, DateModified = _dateModified },
                    new Satellite() { System = "ST", Planet = "HUR", Name = "Arial", Code = "ARIA", IsAvailable = true, DateAdded = _dateAdded, DateModified = _dateModified },
                    new Satellite() { System = "ST", Planet = "MIC", Name = "Calliope", Code = "CALL", IsAvailable = true, DateAdded = _dateAdded, DateModified = _dateModified },
                    new Satellite() { System = "ST", Planet = "CRU", Name = "Cellin", Code = "CELL", IsAvailable = true, DateAdded = _dateAdded, DateModified = _dateModified },
                    new Satellite() { System = "ST", Planet = "ARC", Name = "Wala", Code = "WALA", IsAvailable = true, DateAdded = _dateAdded, DateModified = _dateModified }
                })
            );

        _mockWebApiClientAdapter.Setup(s => s.GetTradeportsAsync(It.Is<string>(x => x.Equals("ST")))).ReturnsAsync(
            new ReadOnlyCollection<Tradeport>(
                new List<Tradeport>()
                {
                    new Tradeport() { System = "ST", Planet = "ARC", Satellite = "WALA", City = null, Name = "ArcCorp Mining Area 045", NameShort = "ArcCorp 045", Code = "AM045", IsVisible = true, IsArmisticeZone = true, HasTrade = true, WelcomesOutlaws = false, HasRefinery = false, HasShops = false, IsRestrictedArea = false, HasMinables = false, DateAdded = _dateAdded, DateModified = _dateAdded,
                        Prices = new List<TradeListing>() { new TradeListing() { Code = "PRFO", Name = "Processed Food", Kind = "Food", Operation = OperationType.Sell, PriceBuy = 0m, PriceSell = 1.5m, DateUpdate = _dateModified, IsUpdated = true } } }, 
                    new Tradeport() { System = "ST", Planet = "ARC", Satellite = "WALA", City = null, Name = "ArcCorp Mining Area 048", NameShort = "ArcCorp 048", Code = "AM048", IsVisible = true, IsArmisticeZone = true, HasTrade = true, WelcomesOutlaws = false, HasRefinery = false, HasShops = false, IsRestrictedArea = false, HasMinables = false, DateAdded = _dateAdded, DateModified = _dateAdded },
                    new Tradeport() { System = "ST", Planet = "ARC", Satellite = "WALA", City = null, Name = "ArcCorp Mining Area 056", NameShort = "ArcCorp 056", Code = "AM056", IsVisible = true, IsArmisticeZone = true, HasTrade = true, WelcomesOutlaws = false, HasRefinery = false, HasShops = false, IsRestrictedArea = false, HasMinables = false, DateAdded = _dateAdded, DateModified = _dateAdded },
                    new Tradeport() { System = "ST", Planet = "ARC", Satellite = "WALA", City = null, Name = "ArcCorp Mining Area 061", NameShort = "ArcCorp 061", Code = "AM061", IsVisible = true, IsArmisticeZone = true, HasTrade = true, WelcomesOutlaws = false, HasRefinery = false, HasShops = false, IsRestrictedArea = false, HasMinables = false, DateAdded = _dateAdded, DateModified = _dateAdded }
                })
            );

        _mockWebApiClientAdapter.Setup(s => s.GetCommoditiesAsync()).ReturnsAsync(
            new ReadOnlyCollection<Commodity>(
                new List<Commodity>()
                {
                    new Commodity() { Name = "Agricultural Supplies", Code = "ACSU", Kind = "Agricultural", BuyPrice = 1.01m, SellPrice = 1.21m, DateAdded = _dateAdded, DateModified = _dateModified },
                    new Commodity() { Name = "Agricium", Code = "AGRI", Kind = "Metal", BuyPrice = 23.79m, SellPrice = 27.5m, DateAdded = _dateAdded, DateModified = _dateModified },
                    new Commodity() { Name = "Agricium (Ore)", Code = "AGRW", Kind = "Metal", BuyPrice = 0m, SellPrice = 13.75m, DateAdded = _dateAdded, DateModified = _dateModified },
                    new Commodity() { Name = "Altruciatoxin", Code = "ALTX", Kind = "Drug", BuyPrice = 12.12m, SellPrice = 0m, DateAdded = _dateAdded, DateModified = _dateModified },
                    new Commodity() { Name = "Aluminum", Code = "ALUM", Kind = "Metal", BuyPrice = 1.11m, SellPrice = 1.3m, DateAdded = _dateAdded, DateModified = _dateModified },
                    new Commodity() { Name = "Aluminum (Ore)", Code = "ALUW", Kind = "Metal", BuyPrice = 0m, SellPrice = 0.67m, DateAdded = _dateAdded, DateModified = _dateModified }
                })
            );

        _mockWebApiClientAdapter.Setup(s => s.GetTradeportAsync(It.Is<string>(x => x.Equals("AM056")))).ReturnsAsync(
            new Tradeport()
            {
                System = "ST",
                Planet = "ARC",
                Satellite = "WALA",
                City = null,
                Name = "ArcCorp Mining Area 056",
                NameShort = "ArcCorp 056",
                Code = "AM056",
                IsVisible = true,
                IsArmisticeZone = true,
                HasTrade = true,
                WelcomesOutlaws = false,
                HasRefinery = false,
                HasShops = false,
                IsRestrictedArea = false,
                HasMinables = false,
                DateAdded = _dateAdded,
                DateModified = _dateAdded,
                Prices = new List<TradeListing>()
                    {
                        new TradeListing() { Code = "PRFO", Name = "Processed Food", Kind = "Food", Operation = OperationType.Sell, PriceBuy = 0m, PriceSell = 1.5m, DateUpdate = _dateModified, IsUpdated = true },
                        new TradeListing() { Code = "STIM", Name = "Stims", Kind = "Vice", Operation = OperationType.Sell, PriceBuy = 0m, PriceSell = 3.8m, DateUpdate = _dateModified, IsUpdated = true },
                        new TradeListing() { Code = "DISP", Name = "Distilled Spirits", Kind = "Vice", Operation = OperationType.Sell, PriceBuy = 0m, PriceSell = 5.56m, DateUpdate = _dateModified, IsUpdated = true },
                        new TradeListing() { Code = "MEDS", Name = "Medical Supplies", Kind = "Medical", Operation = OperationType.Sell, PriceBuy = 0m, PriceSell = 19.25m, DateUpdate = _dateModified, IsUpdated = true },
                        new TradeListing() { Code = "DOLI", Name = "Dolivine", Kind = "Mineral", Operation = OperationType.Sell, PriceBuy = 0m, PriceSell = 130m, DateUpdate = _dateModified, IsUpdated = false },
                        new TradeListing() { Code = "APHO", Name = "Aphorite", Kind = "Mineral", Operation = OperationType.Sell, PriceBuy = 0m, PriceSell = 152.5m, DateUpdate = _dateModified, IsUpdated = true },
                        new TradeListing() { Code = "HADA", Name = "Hadanite", Kind = "Mineral", Operation = OperationType.Sell, PriceBuy = 0m, PriceSell = 275m, DateUpdate = _dateModified, IsUpdated = true },
                        new TradeListing() { Code = "IODI", Name = "Iodine", Kind = "Halogen", Operation = OperationType.Buy, PriceBuy = 0.35m, PriceSell = 0m, DateUpdate = _dateModified, IsUpdated = true },
                        new TradeListing() { Code = "ALUM", Name = "Aluminum", Kind = "Metal", Operation = OperationType.Buy, PriceBuy = 1.11m, PriceSell = 0m, DateUpdate = _dateModified, IsUpdated = true },
                        new TradeListing() { Code = "TUNG", Name = "Tungsten", Kind = "Metal", Operation = OperationType.Buy, PriceBuy = 3.55m, PriceSell = 0m, DateUpdate = _dateModified, IsUpdated = true },
                        new TradeListing() { Code = "DIAM", Name = "Diamond", Kind = "Metal", Operation = OperationType.Buy, PriceBuy = 6.28m, PriceSell = 0m, DateUpdate = _dateModified, IsUpdated = true },
                        new TradeListing() { Code = "ASTA", Name = "Astatine", Kind = "Halogen", Operation = OperationType.Buy, PriceBuy = 7.52m, PriceSell = 0m, DateUpdate = _dateModified, IsUpdated = true },
                        new TradeListing() { Code = "LARA",  Name = "Laranite", Kind = "Metal", Operation = OperationType.Buy, PriceBuy = 27.74m, PriceSell = 0m, DateUpdate = _dateModified, IsUpdated = true }
                    }
            });

        _mockWebApiClientAdapter.Setup(s => s.SubmitPriceReportAsync(It.IsAny<PriceReport>())).ReturnsAsync(
            new PriceReportResponse()
            {
                Response = true,
                StatusMessage = "ok"
            });
    }

    [Fact]
    public async Task GetAllSystemsAsync_ShouldCallWebApiOnce()
    {
        // Assemble

        // Act
        var actual1 = await _uexCacheDataService.GetAllSystemsAsync();
        var actual2 = await _uexCacheDataService.GetAllSystemsAsync();

        // Assert
        actual1.Should().BeSameAs(actual2);
        _mockWebApiClientAdapter.Verify(v => v.GetSystemsAsync(), Times.Once());
    }

    [Fact]
    public async Task GetAllPlanetsAsync_ShouldCallWebApiOnce()
    {
        // Assemble
        const string SystemCode = "ST";

        // Act
        var actual1 = await _uexCacheDataService.GetAllPlanetsAsync(SystemCode);
        var actual2 = await _uexCacheDataService.GetAllPlanetsAsync(SystemCode);

        // Assert
        actual1.Should().BeSameAs(actual2);
        _mockWebApiClientAdapter.Verify(v => v.GetPlanetsAsync(SystemCode), Times.Once());
    }

    [Fact]
    public async Task GetAllCitiesAsync_ShouldCallWebApiOnce()
    {
        // Assemble
        const string SystemCode = "ST";

        // Act
        var actual1 = await _uexCacheDataService.GetAllCitiesAsync(SystemCode);
        var actual2 = await _uexCacheDataService.GetAllCitiesAsync(SystemCode);

        // Assert
        actual1.Should().BeSameAs(actual2);
        _mockWebApiClientAdapter.Verify(v => v.GetCitiesAsync(SystemCode), Times.Once());
    }

    [Fact]
    public async Task GetAllTradeportsAsync_ShouldCallWebApiOnce()
    {
        // Assemble
        const string SystemCode = "ST";

        // Act
        var actual1 = await _uexCacheDataService.GetAllTradeportsAsync(SystemCode);
        var actual2 = await _uexCacheDataService.GetAllTradeportsAsync(SystemCode);

        // Assert
        actual1.Should().BeSameAs(actual2);
        _mockWebApiClientAdapter.Verify(v => v.GetTradeportsAsync(SystemCode), Times.Once());
    }

    [Fact]
    public async Task GetAllCommoditiesAsync_ShouldCallWebApiTwice()
    {
        // Assemble

        // Act
        var actual1 = await _uexCacheDataService.GetAllCommoditiesAsync();
        var actual2 = await _uexCacheDataService.GetAllCommoditiesAsync();

        // Assert
        actual1.Should().BeEquivalentTo(actual2);
        _mockWebApiClientAdapter.Verify(v => v.GetCommoditiesAsync(), Times.Exactly(2));
    }

    [Fact]
    public async Task GetTradeportAsync_UexCacheDataService_ShouldCallWebApiTwice()
    {
        // Assemble
        const string TradeportCode = "AM056";

        // Act
        var actual1 = await _uexCacheDataService.GetTradeportAsync(TradeportCode);
        var actual2 = await _uexCacheDataService.GetTradeportAsync(TradeportCode);

        // Assert
        actual1.Should().BeEquivalentTo(actual2);
        _mockWebApiClientAdapter.Verify(v => v.GetTradeportAsync(TradeportCode), Times.Exactly(2));
    }

    [Fact]
    public async Task SubmitPriceReportAsync_UexCacheDataService_ShouldCallWebApiTwice()
    {
        // Assemble
        var priceReport = new UexCorpDataRunner.Domain.DataRunner.PriceReport()
        {
            CommodityCode = "PRFO",
            TradeportCode = "AM056",
            Operation = "sell",
            Price = "1.5",
            AccessCode = "c5e000",
            Confirm = false
        };

        // Act
        var actual1 = await _uexCacheDataService.SubmitPriceReportAsync(priceReport);
        var actual2 = await _uexCacheDataService.SubmitPriceReportAsync(priceReport);

        // Assert
        actual1.Should().BeEquivalentTo(actual2);
        _mockWebApiClientAdapter.Verify(v => v.SubmitPriceReportAsync(priceReport), Times.Exactly(2));
    }
}
