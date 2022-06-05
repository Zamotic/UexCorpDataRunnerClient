﻿using Moq;
using UexCorpDataRunner.Persistence.Api.Uex.DataTransferObjects;
using UexCorpDataRunner.Persistence.Api.Uex;

namespace UexCorpDataRunner.Persistence.Api.Mock.Uex;

public class UexCorpWebApiClientMock : Mock<IUexCorpWebApiClient>, IUexCorpWebApiClient
{
    public UexCorpWebApiClientMock()
    {
        var dateAdded = new DateTimeOffset(2020, 12, 25, 23, 25, 15, TimeSpan.FromHours(3));
        var dateModified = new DateTimeOffset(2022, 06, 04, 20, 00, 06, TimeSpan.FromHours(3));

        ICollection<SystemDto> systemDtoList = new List<SystemDto>()
            {
                new SystemDto() { Name = "Pyro", Code = "PY", IsAvailable = false, IsDefault = false, DateAdded = dateAdded, DateModified = DateTime.MinValue },
                new SystemDto() { Name = "Stanton", Code = "ST", IsAvailable = true, IsDefault = true, DateAdded = dateAdded, DateModified = DateTime.MinValue }
            };
        this.Setup(s => s.GetSystemsAsync()).Returns(Task.FromResult(systemDtoList));

        ICollection<PlanetDto> planetDtoList = new List<PlanetDto>()
            {
                new PlanetDto() { System = "ST", Name = "ArcCorp", Code = "ARC", IsAvailable = true, DateAdded = dateAdded, DateModified = DateTime.MinValue },
                new PlanetDto() { System = "ST", Name = "Crusader", Code = "CRU", IsAvailable = true, DateAdded = dateAdded, DateModified = DateTime.MinValue },
                new PlanetDto() { System = "ST", Name = "Hurston", Code = "HUR", IsAvailable = true, DateAdded = dateAdded, DateModified = DateTime.MinValue },
                new PlanetDto() { System = "ST", Name = "microTech", Code = "MIC", IsAvailable = true, DateAdded = dateAdded, DateModified = DateTime.MinValue }
            };
        this.Setup(s => s.GetPlanetsAsync(It.Is((string s) => s.Equals("ST")))).Returns(Task.FromResult(planetDtoList));

        ICollection<SatelliteDto> satelliteDtoList = new List<SatelliteDto>()
            {
                new SatelliteDto() { System = "ST", Planet = "HUR", Name = "Aberdeen", Code = "ABER", IsAvailable = true, DateAdded = dateAdded, DateModified = DateTime.MinValue },
                new SatelliteDto() { System = "ST", Planet = "HUR", Name = "Arial", Code = "ARIA", IsAvailable = true, DateAdded = dateAdded, DateModified = DateTime.MinValue },
                new SatelliteDto() { System = "ST", Planet = "MIC", Name = "Calliope", Code = "CALL", IsAvailable = true, DateAdded = dateAdded, DateModified = DateTime.MinValue },
                new SatelliteDto() { System = "ST", Planet = "CRU", Name = "Cellin", Code = "CELL", IsAvailable = true, DateAdded = dateAdded, DateModified = DateTime.MinValue }
            };
        this.Setup(s => s.GetSatellitesAsync(It.Is((string s) => s.Equals("ST")))).Returns(Task.FromResult(satelliteDtoList));

        ICollection<CityDto> cityDtoList = new List<CityDto>()
            {
                new CityDto() { System = "ST", Planet = "ARC", Name = "Area 18", Code = "A18", IsAvailable = true, DateAdded = dateAdded, DateModified = DateTime.MinValue },
                new CityDto() { System = "ST", Planet = "HUR", Name = "Lorville", Code = "LOR", IsAvailable = true, DateAdded = dateAdded, DateModified = DateTime.MinValue },
                new CityDto() { System = "ST", Planet = "MIC", Name = "New Babbage", Code = "NBB", IsAvailable = true, DateAdded = dateAdded, DateModified = DateTime.MinValue },
                new CityDto() { System = "ST", Planet = "CRU", Name = "Orison", Code = "ORI", IsAvailable = true, DateAdded = dateAdded, DateModified = DateTime.MinValue }
            };
        this.Setup(s => s.GetCitiesAsync(It.Is((string s) => s.Equals("ST")))).Returns(Task.FromResult(cityDtoList));

        ICollection<TradeportDto> tradeportDtoList = new List<TradeportDto>()
            {
                new TradeportDto() { System = "ST", Planet = "ARC", Satellite = "WALA", City = null, Name = "ArcCorp Mining Area 045", NameShort = "ArcCorp 045", Code = "AM045", IsVisible = true, IsArmisticeZone = true, HasTrade = true, WelcomesOutlaws = false, HasRefinery = false, HasShops = false, IsRestrictedArea = false, HasMinables = false, DateAdded = dateAdded, DateModified = dateAdded, 
                    Prices = new Dictionary<string, TradeListingDto>() { { "PRFO", new TradeListingDto() { Name = "Processed Food", Kind = "Food", Operation = "sell", PriceBuy = 0m, PriceSell = 1.5m, DateUpdate = dateModified, IsUpdated = true } } } },
                new TradeportDto() { System = "ST", Planet = "ARC", Satellite = "WALA", City = null, Name = "ArcCorp Mining Area 048", NameShort = "ArcCorp 048", Code = "AM048", IsVisible = true, IsArmisticeZone = true, HasTrade = true, WelcomesOutlaws = false, HasRefinery = false, HasShops = false, IsRestrictedArea = false, HasMinables = false, DateAdded = dateAdded, DateModified = dateAdded },
                new TradeportDto() { System = "ST", Planet = "ARC", Satellite = "WALA", City = null, Name = "ArcCorp Mining Area 056", NameShort = "ArcCorp 056", Code = "AM056", IsVisible = true, IsArmisticeZone = true, HasTrade = true, WelcomesOutlaws = false, HasRefinery = false, HasShops = false, IsRestrictedArea = false, HasMinables = false, DateAdded = dateAdded, DateModified = dateAdded },
                new TradeportDto() { System = "ST", Planet = "ARC", Satellite = "WALA", City = null, Name = "ArcCorp Mining Area 061", NameShort = "ArcCorp 061", Code = "AM061", IsVisible = true, IsArmisticeZone = true, HasTrade = true, WelcomesOutlaws = false, HasRefinery = false, HasShops = false, IsRestrictedArea = false, HasMinables = false, DateAdded = dateAdded, DateModified = dateAdded }
            };
        this.Setup(s => s.GetTradeportsAsync(It.Is((string s) => s.Equals("ST")))).Returns(Task.FromResult(tradeportDtoList));

        ICollection<CommodityDto> commodityDtoList = new List<CommodityDto>()
            {
                new CommodityDto() { Name = "Agricultural Supplies", Code = "ACSU", Kind = "Agricultural", BuyPrice = 1.01m, SellPrice = 1.21m, DateAdded = dateAdded, DateModified = dateModified },
                new CommodityDto() { Name = "Agricium", Code = "AGRI", Kind = "Metal", BuyPrice = 23.79m, SellPrice = 27.5m, DateAdded = dateAdded, DateModified = dateModified },
                new CommodityDto() { Name = "Agricium (Ore)", Code = "AGRW", Kind = "Metal", BuyPrice = 0m, SellPrice = 13.75m, DateAdded = dateAdded, DateModified = dateModified },
                new CommodityDto() { Name = "Altruciatoxin", Code = "ALTX", Kind = "Drug", BuyPrice = 12.12m, SellPrice = 0m, DateAdded = dateAdded, DateModified = dateModified },
                new CommodityDto() { Name = "Aluminum", Code = "ALUM", Kind = "Metal", BuyPrice = 1.11m, SellPrice = 1.3m, DateAdded = dateAdded, DateModified = dateModified },
                new CommodityDto() { Name = "Aluminum (Ore)", Code = "ALUW", Kind = "Metal", BuyPrice = 0m, SellPrice = 0.67m, DateAdded = dateAdded, DateModified = dateModified }
            };
        this.Setup(s => s.GetCommoditiesAsync()).Returns(Task.FromResult(commodityDtoList));
    }

    public async Task<ICollection<SystemDto>> GetSystemsAsync()
    {
        return await this.Object.GetSystemsAsync();
    }

    public async Task<ICollection<PlanetDto>> GetPlanetsAsync(string systemCode)
    {
        return await this.Object.GetPlanetsAsync(systemCode);
    }

    public async Task<ICollection<SatelliteDto>> GetSatellitesAsync(string systemCode)
    {
        return await this.Object.GetSatellitesAsync(systemCode);
    }

    public async Task<ICollection<CityDto>> GetCitiesAsync(string systemCode)
    {
        return await this.Object.GetCitiesAsync(systemCode);
    }

    public async Task<ICollection<TradeportDto>> GetTradeportsAsync(string systemCode)
    {
        return await this.Object.GetTradeportsAsync(systemCode);
    }

    public async Task<ICollection<CommodityDto>> GetCommoditiesAsync()
    {
        return await this.Object.GetCommoditiesAsync();
    }
}
