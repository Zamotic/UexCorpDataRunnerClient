using NSubstitute;
using UexCorpDataRunner.Persistence.Api.UexV2;
using UexCorpDataRunner.Persistence.Api.UexV2.DataTransferObjects;

namespace UexCorpDataRunner.Persistence.Api.Mock.UexV2;
public class UexCorpWebApiClientMock : IUexCorpWebApiClient
{
    IUexCorpWebApiClient _SubstituteClient = Substitute.For<IUexCorpWebApiClient>();

    public UexCorpWebApiClientMock()
    {
        var dateAdded = new DateTimeOffset(2020, 12, 25, 23, 25, 15, TimeSpan.FromHours(3));
        var dateModified = new DateTimeOffset(2022, 06, 04, 20, 00, 06, TimeSpan.FromHours(3));

        GameVersionDto gameVersionDto = new GameVersionDto()
            { 
                Live = "3.21.1", 
                Ptu = "3.22" 
            };
        _SubstituteClient.GetCurrentVersionAsync().Returns(gameVersionDto);

        ICollection<StarSystemDto> systemDtoList = new List<StarSystemDto>()
            {
                new StarSystemDto() { Id = 64, Name = "Pyro", Code = "PY", IsAvailable = false, IsDefault = false, DateAdded = dateAdded, DateModified = DateTime.MinValue },
                new StarSystemDto() { Id = 68, Name = "Stanton", Code = "ST", IsAvailable = true, IsDefault = true, DateAdded = dateAdded, DateModified = DateTime.MinValue }
            };
        _SubstituteClient.GetSystemsAsync().Returns(systemDtoList);

        ICollection<PlanetDto> planetsDtoList = new List<PlanetDto>()
            {
                new PlanetDto() { Id = 147, StarSystemId = 68, Name = "ArcCorp", Code = "ARC", IsAvailable = true, DateAdded = dateAdded, DateModified = DateTime.MinValue },
                new PlanetDto() { Id = 147, StarSystemId = 68, Name = "Crusader", Code = "CRU", IsAvailable = true, DateAdded = dateAdded, DateModified = DateTime.MinValue },
                new PlanetDto() { Id = 147, StarSystemId = 68, Name = "Hurston", Code = "HUR", IsAvailable = true, DateAdded = dateAdded, DateModified = DateTime.MinValue },
                new PlanetDto() { Id = 147, StarSystemId = 68, Name = "microTech", Code = "MIC", IsAvailable = true, DateAdded = dateAdded, DateModified = DateTime.MinValue }
            };
        _SubstituteClient.GetPlanetsAsync(Arg.Any<int>()).Returns(planetsDtoList);

        ICollection<MoonDto> moonsBySystemDtoList = new List<MoonDto>()
            {
                new MoonDto() { Id = 1, StarSystemId = 68, PlanetId = 4, Name = "Aberdeen", NameOrigin = "Stanton 1b", Code = "ABE", IsAvailable = true, IsVisible = true, IsDefault = true, DateAdded = dateAdded, DateModified = DateTime.MinValue },
                new MoonDto() { Id = 2, StarSystemId = 68, PlanetId = 4, Name = "Arial", NameOrigin = "Stanton 1a", Code = "ARI", IsAvailable = true, IsVisible = true, IsDefault = false, DateAdded = dateAdded, DateModified = DateTime.MinValue },
                new MoonDto() { Id = 3, StarSystemId = 68, PlanetId = 5, Name = "Calliope", NameOrigin = "Stanton 4a", Code = "CAL", IsAvailable = true, IsVisible = true, IsDefault = false, DateAdded = dateAdded, DateModified = DateTime.MinValue },
                new MoonDto() { Id = 4, StarSystemId = 68, PlanetId = 2, Name = "Cellin", NameOrigin = "Stanton 2a", Code = "CEL", IsAvailable = true, IsVisible = true, IsDefault = false, DateAdded = dateAdded, DateModified = DateTime.MinValue },
                new MoonDto() { Id = 5, StarSystemId = 68, PlanetId = 5, Name = "Clio", NameOrigin = "Stanton 4b", Code = "CLI", IsAvailable = true, IsVisible = true, IsDefault = false, DateAdded = dateAdded, DateModified = DateTime.MinValue },
                new MoonDto() { Id = 6, StarSystemId = 68, PlanetId = 2, Name = "Daymar", NameOrigin = "Stanton 2b", Code = "DAY", IsAvailable = true, IsVisible = true, IsDefault = false, DateAdded = dateAdded, DateModified = DateTime.MinValue },
                new MoonDto() { Id = 7, StarSystemId = 68, PlanetId = 5, Name = "Euterpe", NameOrigin = "Stanton 4c", Code = "EUT", IsAvailable = true, IsVisible = true, IsDefault = false, DateAdded = dateAdded, DateModified = DateTime.MinValue },
                new MoonDto() { Id = 8, StarSystemId = 68, PlanetId = 4, Name = "Ita", NameOrigin = "Stanton 1d", Code = "ITA", IsAvailable = true, IsVisible = true, IsDefault = true, DateAdded = dateAdded, DateModified = DateTime.MinValue },
                new MoonDto() { Id = 9, StarSystemId = 68, PlanetId = 1, Name = "Lyria", NameOrigin = "Stanton 3a", Code = "LYR", IsAvailable = true, IsVisible = true, IsDefault = false, DateAdded = dateAdded, DateModified = DateTime.MinValue },
                new MoonDto() { Id = 10, StarSystemId = 68, PlanetId = 4, Name = "Magda", NameOrigin = "Stanton 1c", Code = "MAG", IsAvailable = true, IsVisible = true, IsDefault = true, DateAdded = dateAdded, DateModified = DateTime.MinValue },
                new MoonDto() { Id = 11, StarSystemId = 68, PlanetId = 1, Name = "Wala", NameOrigin = "Stanton 3b", Code = "WAL", IsAvailable = true, IsVisible = true, IsDefault = false, DateAdded = dateAdded, DateModified = DateTime.MinValue },
                new MoonDto() { Id = 12, StarSystemId = 68, PlanetId = 2, Name = "Yela", NameOrigin = "Stanton 2c", Code = "YEL", IsAvailable = true, IsVisible = true, IsDefault = false, DateAdded = dateAdded, DateModified = DateTime.MinValue }
            };
        _SubstituteClient.GetMoonsAsync(Arg.Any<int>()).Returns(moonsBySystemDtoList);

        ICollection<MoonDto> moonsByPlanetDtoList = new List<MoonDto>()
            {
                new MoonDto() { Id = 1, StarSystemId = 68, PlanetId = 4, Name = "Aberdeen", NameOrigin = "Stanton 1b", Code = "ABE", IsAvailable = true, IsVisible = true, IsDefault = true, DateAdded = dateAdded, DateModified = DateTime.MinValue },
                new MoonDto() { Id = 2, StarSystemId = 68, PlanetId = 4, Name = "Arial", NameOrigin = "Stanton 1a", Code = "ARI", IsAvailable = true, IsVisible = true, IsDefault = false, DateAdded = dateAdded, DateModified = DateTime.MinValue },
                new MoonDto() { Id = 8, StarSystemId = 68, PlanetId = 4, Name = "Ita", NameOrigin = "Stanton 1d", Code = "ITA", IsAvailable = true, IsVisible = true, IsDefault = true, DateAdded = dateAdded, DateModified = DateTime.MinValue },
                new MoonDto() { Id = 10, StarSystemId = 68, PlanetId = 4, Name = "Magda", NameOrigin = "Stanton 1c", Code = "MAG", IsAvailable = true, IsVisible = true, IsDefault = true, DateAdded = dateAdded, DateModified = DateTime.MinValue }
            };
        _SubstituteClient.GetMoonsAsync(Arg.Any<int>()).Returns(moonsByPlanetDtoList);

        ICollection<CityDto> citiesDtoList = new List<CityDto>()
            {
                new CityDto() { Id = 1, StarSystemId = 68, PlanetId = 1, MoonId = 0, Name = "Area 18", Code = "AR18", IsAvailable = false, IsVisible = true, IsDefault = false, IsMonitored = true, IsArmistice = true, IsLandable = true, IsDecomissioned = false, HasQuantumMarker = true, HasTradeTerminal = true, HasHabitation = true, HasRefinery = false, HasCargoCenter = false, HasClinic = true, HasFood = true, HasShops = true, HasRefuel = true, HasRepair = true, HasGravity = true, DateAdded = dateAdded, DateModified = DateTime.MinValue },
                new CityDto() { Id = 3, StarSystemId = 68, PlanetId = 4, MoonId = 0, Name = "Lorville", Code = "LORV", IsAvailable = false, IsVisible = true, IsDefault = true, IsMonitored = true, IsArmistice = true, IsLandable = true, IsDecomissioned = false, HasQuantumMarker = true, HasTradeTerminal = true, HasHabitation = true, HasRefinery = false, HasCargoCenter = false, HasClinic = true, HasFood = true, HasShops = true, HasRefuel = true, HasRepair = true, HasGravity = true, DateAdded = dateAdded, DateModified = DateTime.MinValue },
                new CityDto() { Id = 4, StarSystemId = 68, PlanetId = 5, MoonId = 0, Name = "New Babbage", Code = "NEWB", IsAvailable = false, IsVisible = true, IsDefault = false, IsMonitored = true, IsArmistice = true, IsLandable = true, IsDecomissioned = false, HasQuantumMarker = true, HasTradeTerminal = true, HasHabitation = true, HasRefinery = false, HasCargoCenter = false, HasClinic = true, HasFood = true, HasShops = true, HasRefuel = true, HasRepair = true, HasGravity = true, DateAdded = dateAdded, DateModified = DateTime.MinValue },
                new CityDto() { Id = 5, StarSystemId = 68, PlanetId = 2, MoonId = 0, Name = "Orison", Code = "ORIS", IsAvailable = false, IsVisible = true, IsDefault = false, IsMonitored = true, IsArmistice = true, IsLandable = true, IsDecomissioned = false, HasQuantumMarker = true, HasTradeTerminal = true, HasHabitation = true, HasRefinery = false, HasCargoCenter = false, HasClinic = true, HasFood = true, HasShops = true, HasRefuel = true, HasRepair = true, HasGravity = true, DateAdded = dateAdded, DateModified = DateTime.MinValue }
            };
        _SubstituteClient.GetCitiesAsync(Arg.Any<int>()).Returns(citiesDtoList);

        ICollection<CommodityDto> commodityDtoList = new List<CommodityDto>()
            {
                new CommodityDto() { Id = 1, ParentId = 0, Name = "Agricultural Supplies", Code = "AGRSU", Kind = "Agricultural", PriceBuy = 99.3023f, PriceSell = 118.918f, IsAvailable  = true, IsVisible = true, IsRaw = false, IsHarvestable = false, IsBuyable = true, IsSellable = true, IsTemporary = false, IsIllegal = false ,DateAdded = dateAdded, DateModified = dateModified },
                new CommodityDto() { Id = 2, ParentId = 43, Name = "Agricium", Code = "AGRIC", Kind = "Metal", PriceBuy = 2198.7f, PriceSell = 2446.44f, IsAvailable  = true, IsVisible = true, IsRaw = false, IsHarvestable = false, IsBuyable = true, IsSellable = true, IsTemporary = false, IsIllegal = false ,DateAdded = dateAdded, DateModified = dateModified },
                new CommodityDto() { Id = 3, ParentId = 33, Name = "Aluminum", Code = "ALUMI", Kind = "Metal", PriceBuy = 245.858f, PriceSell = 322.2f, IsAvailable  = true, IsVisible = true, IsRaw = false, IsHarvestable = false, IsBuyable = true, IsSellable = true, IsTemporary = false, IsIllegal = false ,DateAdded = dateAdded, DateModified = dateModified },
                new CommodityDto() { Id = 4, ParentId = 0, Name = "Astatine", Code = "ASTAT", Kind = "Halogen", PriceBuy = 706.911f, PriceSell = 881.936f, IsAvailable  = true, IsVisible = true, IsRaw = false, IsHarvestable = false, IsBuyable = true, IsSellable = true, IsTemporary = false, IsIllegal = false ,DateAdded = dateAdded, DateModified = dateModified },
                new CommodityDto() { Id = 5, ParentId = 37, Name = "Beryl", Code = "BERYL", Kind = "Mineral", PriceBuy = 2296.09f, PriceSell = 2664.54f, IsAvailable  = true, IsVisible = true, IsRaw = false, IsHarvestable = false, IsBuyable = true, IsSellable = true, IsTemporary = false, IsIllegal = false ,DateAdded = dateAdded, DateModified = dateModified },
                new CommodityDto() { Id = 6, ParentId = 47, Name = "Bexalite", Code = "BEXAL", Kind = "Mineral", PriceBuy = 0f, PriceSell = 6852.14f, IsAvailable  = true, IsVisible = true, IsRaw = false, IsHarvestable = false, IsBuyable = false, IsSellable = true, IsTemporary = false, IsIllegal = false ,DateAdded = dateAdded, DateModified = dateModified },
                new CommodityDto() { Id = 7, ParentId = 45, Name = "Borase", Code = "BORAS", Kind = "Metal", PriceBuy = 0f, PriceSell = 3049.08f, IsAvailable  = true, IsVisible = true, IsRaw = false, IsHarvestable = false, IsBuyable = false, IsSellable = true, IsTemporary = false, IsIllegal = false ,DateAdded = dateAdded, DateModified = dateModified },
                new CommodityDto() { Id = 8, ParentId = 0, Name = "Chlorine", Code = "CHLOR", Kind = "Halogen", PriceBuy = 129.3f, PriceSell = 169.494f, IsAvailable  = true, IsVisible = true, IsRaw = false, IsHarvestable = false, IsBuyable = true, IsSellable = true, IsTemporary = false, IsIllegal = false ,DateAdded = dateAdded, DateModified = dateModified },
                new CommodityDto() { Id = 9, ParentId = 38, Name = "Copper", Code = "COPPE", Kind = "Metal", PriceBuy = 285.565f, PriceSell = 330.69f, IsAvailable  = true, IsVisible = true, IsRaw = false, IsHarvestable = false, IsBuyable = false, IsSellable = true, IsTemporary = false, IsIllegal = false ,DateAdded = dateAdded, DateModified = dateModified },
                new CommodityDto() { Id = 10, ParentId = 35, Name = "Corundum", Code = "CORUN", Kind = "Mineral", PriceBuy = 293.616f, PriceSell = 375.68f, IsAvailable  = true, IsVisible = true, IsRaw = false, IsHarvestable = false, IsBuyable = true, IsSellable = true, IsTemporary = false, IsIllegal = false ,DateAdded = dateAdded, DateModified = dateModified }
            };
        _SubstituteClient.GetCommoditiesAsync().Returns(commodityDtoList);

        ICollection<CommodityPriceDto> commodityPriceByCommodityDtoList = new List<CommodityPriceDto>()
            {
                new CommodityPriceDto() { Id = 6, CommodityId = 4, StarSystemId = 68, PlanetId = 1, MoonId = 11, CityId = 0, OutpostId = 3, TerminalId = 8, PriceBuy = 706, PriceBuyMin = 706, PriceBuyMinWeek = 706, PriceBuyMinMonth = 706, PriceBuyMax = 706, PriceBuyMaxWeek = 706, PriceBuyMaxMonth = 706, PriceBuyAvg = 706, PriceBuyAvgWeek = 706, PriceBuyAvgMonth = 706, PriceSell = 0, PriceSellMin = 0, PriceSellMinWeek = 0, PriceSellMinMonth = 0, PriceSellMax = 0, PriceSellMaxWeek = 0, PriceSellMaxMonth = 0, PriceSellAvg = 0, PriceSellAvgWeek = 0, PriceSellAvgMonth = 0, ScuBuy = 437, ScuBuyMin = 437, ScuBuyMinWeek = 437, ScuBuyMinMonth = 437, ScuBuyMax = 437, ScuBuyMaxWeek = 437, ScuBuyMaxMonth = 437,ScuBuyAvg = 437, ScuBuyAvgWeek = 437, ScuBuyAvgMonth = 437, ScuSell = 0, ScuSellMin = 0, ScuSellMinWeek = 0, ScuSellMinMonth = 0, ScuSellMax = 0, ScuSellMaxWeek = 0, ScuSellMaxMonth = 0, ScuSellAvg = 0, ScuSellAvgWeek  = 0, ScuSellAvgMonth = 0, GameVersion = "3.20", CommodityName = "Astatine", StarSystemName = "Stanton", PlanetName = "ArcCorp", MoonName = "Wala", SpaceStationName = null, OutpostName = "ArcCorp Mining Area 056", CityName = null, DateAdded = dateAdded, DateModified = dateModified },
                new CommodityPriceDto() { Id = 18, CommodityId = 4, StarSystemId = 68, PlanetId = 1, MoonId = 11, CityId = 0, OutpostId = 4, TerminalId = 9, PriceBuy = 693, PriceBuyMin = 693, PriceBuyMinWeek = 693, PriceBuyMinMonth = 693, PriceBuyMax = 693, PriceBuyMaxWeek = 693, PriceBuyMaxMonth = 693, PriceBuyAvg = 693, PriceBuyAvgWeek = 693, PriceBuyAvgMonth = 693, PriceSell = 0, PriceSellMin = 0, PriceSellMinWeek = 0, PriceSellMinMonth = 0, PriceSellMax = 0, PriceSellMaxWeek = 0, PriceSellMaxMonth = 0, PriceSellAvg = 0, PriceSellAvgWeek = 0, PriceSellAvgMonth = 0, ScuBuy = 666, ScuBuyMin = 666, ScuBuyMinWeek = 666, ScuBuyMinMonth = 666, ScuBuyMax = 666, ScuBuyMaxWeek = 666, ScuBuyMaxMonth = 666,ScuBuyAvg = 666, ScuBuyAvgWeek = 666, ScuBuyAvgMonth = 666, ScuSell = 0, ScuSellMin = 0, ScuSellMinWeek = 0, ScuSellMinMonth = 0, ScuSellMax = 0, ScuSellMaxWeek = 0, ScuSellMaxMonth = 0, ScuSellAvg = 0, ScuSellAvgWeek  = 0, ScuSellAvgMonth = 0, GameVersion = "3.20", CommodityName = "Astatine", StarSystemName = "Stanton", PlanetName = "ArcCorp", MoonName = "Wala", SpaceStationName = null, OutpostName = "ArcCorp Mining Area 061", CityName = null, DateAdded = dateAdded, DateModified = dateModified },
                new CommodityPriceDto() { Id = 49, CommodityId = 4, StarSystemId = 68, PlanetId = 2, MoonId = 12, CityId = 0, OutpostId = 6, TerminalId = 11, PriceBuy = 732, PriceBuyMin = 732, PriceBuyMinWeek = 732, PriceBuyMinMonth = 732, PriceBuyMax = 732, PriceBuyMaxWeek = 732, PriceBuyMaxMonth = 732, PriceBuyAvg = 732, PriceBuyAvgWeek = 732, PriceBuyAvgMonth = 732, PriceSell = 0, PriceSellMin = 0, PriceSellMinWeek = 0, PriceSellMinMonth = 0, PriceSellMax = 0, PriceSellMaxWeek = 0, PriceSellMaxMonth = 0, PriceSellAvg = 0, PriceSellAvgWeek = 0, PriceSellAvgMonth = 0, ScuBuy = 375, ScuBuyMin = 375, ScuBuyMinWeek = 375, ScuBuyMinMonth = 375, ScuBuyMax = 375, ScuBuyMaxWeek = 375, ScuBuyMaxMonth = 375,ScuBuyAvg = 375, ScuBuyAvgWeek = 375, ScuBuyAvgMonth = 375, ScuSell = 0, ScuSellMin = 0, ScuSellMinWeek = 0, ScuSellMinMonth = 0, ScuSellMax = 0, ScuSellMaxWeek = 0, ScuSellMaxMonth = 0, ScuSellAvg = 0, ScuSellAvgWeek  = 0, ScuSellAvgMonth = 0, GameVersion = "3.20", CommodityName = "Astatine", StarSystemName = "Stanton", PlanetName = "Crusader", MoonName = "Yela", SpaceStationName = null, OutpostName = "ArcCorp Mining Area 157", CityName = null, DateAdded = dateAdded, DateModified = dateModified },
                new CommodityPriceDto() { Id = 138, CommodityId = 4, StarSystemId = 68, PlanetId = 1, MoonId = 0, CityId = 1, OutpostId = 0, TerminalId = 12, PriceBuy = 0, PriceBuyMin = 0, PriceBuyMinWeek = 0, PriceBuyMinMonth = 0, PriceBuyMax = 0, PriceBuyMaxWeek = 0, PriceBuyMaxMonth = 0, PriceBuyAvg = 0, PriceBuyAvgWeek = 0, PriceBuyAvgMonth = 0, PriceSell = 891, PriceSellMin = 891, PriceSellMinWeek = 891, PriceSellMinMonth = 891, PriceSellMax = 891, PriceSellMaxWeek = 891, PriceSellMaxMonth = 891, PriceSellAvg = 891, PriceSellAvgWeek = 891, PriceSellAvgMonth = 891, ScuBuy = 0, ScuBuyMin = 0, ScuBuyMinWeek = 0, ScuBuyMinMonth = 0, ScuBuyMax = 0, ScuBuyMaxWeek = 0, ScuBuyMaxMonth = 0,ScuBuyAvg = 0, ScuBuyAvgWeek = 0, ScuBuyAvgMonth = 0, ScuSell = 367, ScuSellMin = 367, ScuSellMinWeek = 367, ScuSellMinMonth = 367, ScuSellMax = 367, ScuSellMaxWeek = 367, ScuSellMaxMonth = 367, ScuSellAvg = 367, ScuSellAvgWeek  = 367, ScuSellAvgMonth = 367, GameVersion = "3.20", CommodityName = "Astatine", StarSystemName = "Stanton", PlanetName = "ArcCorp", MoonName = null, SpaceStationName = null, OutpostName = null, CityName = "Area 18", DateAdded = dateAdded, DateModified = dateModified }
            };
        _SubstituteClient.GetCommodityPricesByCommodityIdAsync(Arg.Any<int>()).Returns(commodityPriceByCommodityDtoList);

        ICollection<CommodityPriceDto> commodityPriceByTerminalDtoList = new List<CommodityPriceDto>()
            {
                new CommodityPriceDto() { Id = 6, CommodityId = 4, StarSystemId = 68, PlanetId = 1, MoonId = 11, CityId = 0, OutpostId = 3, TerminalId = 8, PriceBuy = 706, PriceBuyMin = 706, PriceBuyMinWeek = 706, PriceBuyMinMonth = 706, PriceBuyMax = 706, PriceBuyMaxWeek = 706, PriceBuyMaxMonth = 706, PriceBuyAvg = 706, PriceBuyAvgWeek = 706, PriceBuyAvgMonth = 706, PriceSell = 0, PriceSellMin = 0, PriceSellMinWeek = 0, PriceSellMinMonth = 0, PriceSellMax = 0, PriceSellMaxWeek = 0, PriceSellMaxMonth = 0, PriceSellAvg = 0, PriceSellAvgWeek = 0, PriceSellAvgMonth = 0, ScuBuy = 437, ScuBuyMin = 437, ScuBuyMinWeek = 437, ScuBuyMinMonth = 437, ScuBuyMax = 437, ScuBuyMaxWeek = 437, ScuBuyMaxMonth = 437,ScuBuyAvg = 437, ScuBuyAvgWeek = 437, ScuBuyAvgMonth = 437, ScuSell = 0, ScuSellMin = 0, ScuSellMinWeek = 0, ScuSellMinMonth = 0, ScuSellMax = 0, ScuSellMaxWeek = 0, ScuSellMaxMonth = 0, ScuSellAvg = 0, ScuSellAvgWeek  = 0, ScuSellAvgMonth = 0, GameVersion = "3.20", CommodityName = "Astatine", StarSystemName = "Stanton", PlanetName = "ArcCorp", MoonName = "Wala", SpaceStationName = null, OutpostName = "ArcCorp Mining Area 056", CityName = null, DateAdded = dateAdded, DateModified = dateModified },
                new CommodityPriceDto() { Id = 18, CommodityId = 4, StarSystemId = 68, PlanetId = 1, MoonId = 11, CityId = 0, OutpostId = 4, TerminalId = 9, PriceBuy = 693, PriceBuyMin = 693, PriceBuyMinWeek = 693, PriceBuyMinMonth = 693, PriceBuyMax = 693, PriceBuyMaxWeek = 693, PriceBuyMaxMonth = 693, PriceBuyAvg = 693, PriceBuyAvgWeek = 693, PriceBuyAvgMonth = 693, PriceSell = 0, PriceSellMin = 0, PriceSellMinWeek = 0, PriceSellMinMonth = 0, PriceSellMax = 0, PriceSellMaxWeek = 0, PriceSellMaxMonth = 0, PriceSellAvg = 0, PriceSellAvgWeek = 0, PriceSellAvgMonth = 0, ScuBuy = 666, ScuBuyMin = 666, ScuBuyMinWeek = 666, ScuBuyMinMonth = 666, ScuBuyMax = 666, ScuBuyMaxWeek = 666, ScuBuyMaxMonth = 666,ScuBuyAvg = 666, ScuBuyAvgWeek = 666, ScuBuyAvgMonth = 666, ScuSell = 0, ScuSellMin = 0, ScuSellMinWeek = 0, ScuSellMinMonth = 0, ScuSellMax = 0, ScuSellMaxWeek = 0, ScuSellMaxMonth = 0, ScuSellAvg = 0, ScuSellAvgWeek  = 0, ScuSellAvgMonth = 0, GameVersion = "3.20", CommodityName = "Astatine", StarSystemName = "Stanton", PlanetName = "ArcCorp", MoonName = "Wala", SpaceStationName = null, OutpostName = "ArcCorp Mining Area 061", CityName = null, DateAdded = dateAdded, DateModified = dateModified },
                new CommodityPriceDto() { Id = 49, CommodityId = 4, StarSystemId = 68, PlanetId = 2, MoonId = 12, CityId = 0, OutpostId = 6, TerminalId = 11, PriceBuy = 732, PriceBuyMin = 732, PriceBuyMinWeek = 732, PriceBuyMinMonth = 732, PriceBuyMax = 732, PriceBuyMaxWeek = 732, PriceBuyMaxMonth = 732, PriceBuyAvg = 732, PriceBuyAvgWeek = 732, PriceBuyAvgMonth = 732, PriceSell = 0, PriceSellMin = 0, PriceSellMinWeek = 0, PriceSellMinMonth = 0, PriceSellMax = 0, PriceSellMaxWeek = 0, PriceSellMaxMonth = 0, PriceSellAvg = 0, PriceSellAvgWeek = 0, PriceSellAvgMonth = 0, ScuBuy = 375, ScuBuyMin = 375, ScuBuyMinWeek = 375, ScuBuyMinMonth = 375, ScuBuyMax = 375, ScuBuyMaxWeek = 375, ScuBuyMaxMonth = 375,ScuBuyAvg = 375, ScuBuyAvgWeek = 375, ScuBuyAvgMonth = 375, ScuSell = 0, ScuSellMin = 0, ScuSellMinWeek = 0, ScuSellMinMonth = 0, ScuSellMax = 0, ScuSellMaxWeek = 0, ScuSellMaxMonth = 0, ScuSellAvg = 0, ScuSellAvgWeek  = 0, ScuSellAvgMonth = 0, GameVersion = "3.20", CommodityName = "Astatine", StarSystemName = "Stanton", PlanetName = "Crusader", MoonName = "Yela", SpaceStationName = null, OutpostName = "ArcCorp Mining Area 157", CityName = null, DateAdded = dateAdded, DateModified = dateModified }
            };
        _SubstituteClient.GetCommodityPricesByTerminalIdAsync(Arg.Any<int>()).Returns(commodityPriceByTerminalDtoList);

        ICollection<TerminalDto> terminalDtoList = new List<TerminalDto>()
            {
                new TerminalDto() { Id = 1, StarSystemId = 68, PlanetId = 1, MoonId = 0, SpaceStationId = 1, OutpostId = 0, CityId = 0, Name = "ARC-L1 - Admin Office", Nickname = "ARC-L1", Code = "ARCL1", Type = "commodity", Screenshot = null, StarSystemName = "Stanton", PlanetName = "ArcCorp", MoonName = null, SpaceStationName = "ARC-L1 Wide Forest Station", OutpostName = null, CityName = null, DateAdded = dateAdded, DateModified = dateModified },
                new TerminalDto() { Id = 2, StarSystemId = 68, PlanetId = 1, MoonId = 0, SpaceStationId = 2, OutpostId = 0, CityId = 0, Name = "ARC-L2 - Admin Office", Nickname = "ARC-L2", Code = "ARCL2", Type = "commodity", Screenshot = null, StarSystemName = "Stanton", PlanetName = "ArcCorp", MoonName = null, SpaceStationName = "ARC-L2 Liveley Pathway Station", OutpostName = null, CityName = null, DateAdded = dateAdded, DateModified = dateModified },
                new TerminalDto() { Id = 3, StarSystemId = 68, PlanetId = 1, MoonId = 0, SpaceStationId = 3, OutpostId = 0, CityId = 0, Name = "ARC-L3 - Admin Office", Nickname = "ARC-L3", Code = "ARCL3", Type = "commodity", Screenshot = @"http://s3.amazonaws.com/uexcorp.space/v2/data/trade_terminals/3/217f6956f2fac9ded07ae8495b52c76ec39ff378.jpg", StarSystemName = "Stanton", PlanetName = "ArcCorp", MoonName = null, SpaceStationName = "ARC-L3 Modern Express Station", OutpostName = null, CityName = null, DateAdded = dateAdded, DateModified = dateModified },
                new TerminalDto() { Id = 4, StarSystemId = 68, PlanetId = 1, MoonId = 0, SpaceStationId = 4, OutpostId = 0, CityId = 0, Name = "ARC-L4 - Admin Office", Nickname = "ARC-L4", Code = "ARCL4", Type = "commodity", Screenshot = null, StarSystemName = "Stanton", PlanetName = "ArcCorp", MoonName = null, SpaceStationName = "ARC-L4 Faint Glen Station", OutpostName = null, CityName = null, DateAdded = dateAdded, DateModified = dateModified },
                new TerminalDto() { Id = 5, StarSystemId = 68, PlanetId = 1, MoonId = 0, SpaceStationId = 5, OutpostId = 0, CityId = 0, Name = "ARC-L5 - Admin Office", Nickname = "ARC-L5", Code = "ARCL5", Type = "commodity", Screenshot = null, StarSystemName = "Stanton", PlanetName = "ArcCorp", MoonName = null, SpaceStationName = "ARC-L5 Yellow Core Station", OutpostName = null, CityName = null, DateAdded = dateAdded, DateModified = dateModified },
                new TerminalDto() { Id = 6, StarSystemId = 68, PlanetId = 1, MoonId = 11, SpaceStationId = 0, OutpostId = 1, CityId = 0, Name = "ArcCorp Mining Area 045", Nickname = "ArcCorp 045", Code = "AM045", Type = "commodity", Screenshot = null, StarSystemName = "Stanton", PlanetName = "ArcCorp", MoonName = "Wala", SpaceStationName = null, OutpostName = "ArcCorp Mining Area 045", CityName = null, DateAdded = dateAdded, DateModified = dateModified },
            };
        _SubstituteClient.GetTerminalsAsync(Arg.Any<int>()).Returns(terminalDtoList);
    }

    public async Task<GameVersionDto> GetCurrentVersionAsync()
    {
        return await _SubstituteClient.GetCurrentVersionAsync();
    }
    public async Task<ICollection<StarSystemDto>> GetSystemsAsync()
    {
        return await _SubstituteClient.GetSystemsAsync();
    }
    public async Task<ICollection<PlanetDto>> GetPlanetsAsync(int starSystemId)
    {
        return await _SubstituteClient.GetPlanetsAsync(starSystemId);
    }
    public async Task<ICollection<MoonDto>> GetMoonsAsync(int starSystemId)
    {
        return await _SubstituteClient.GetMoonsAsync(starSystemId);
    }
    public async Task<ICollection<MoonDto>> GetMoonsAsync(int starSystemId, int planetId)
    {
        return await _SubstituteClient.GetMoonsAsync(starSystemId, planetId);
    }
    public async Task<ICollection<CityDto>> GetCitiesAsync(int starSystemId)
    {
        return await _SubstituteClient.GetCitiesAsync(starSystemId);
    }
    public async Task<ICollection<CommodityDto>> GetCommoditiesAsync()
    {
        return await _SubstituteClient.GetCommoditiesAsync();
    }
    public async Task<ICollection<CommodityPriceDto>> GetCommodityPricesByCommodityIdAsync(int commodityId)
    {
        return await _SubstituteClient.GetCommodityPricesByCommodityIdAsync(commodityId);
    }
    public async Task<ICollection<CommodityPriceDto>> GetCommodityPricesByTerminalIdAsync(int terminalId)
    {
        return await _SubstituteClient.GetCommodityPricesByCommodityIdAsync(terminalId);
    }
    public async Task<ICollection<TerminalDto>> GetTerminalsAsync(int starSystemId)
    {
        return await _SubstituteClient.GetTerminalsAsync(starSystemId);
    }
}
