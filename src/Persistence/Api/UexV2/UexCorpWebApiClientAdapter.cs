using UexCorpDataRunner.Domain.DataRunnerV2;
using UexCorpDataRunner.Persistence.Api.UexV2.DataTransferObjects;
using UexCorpDataRunner.Persistence.Api.UexV2.Mappers;

namespace UexCorpDataRunner.Persistence.Api.UexV2;
public class UexCorpWebApiClientAdapter : IUexCorpWebApiClientAdapter
{
    private readonly IUexCorpWebApiClient _WebClient;

    public UexCorpWebApiClientAdapter(IUexCorpWebApiClient webClient)
    {
        _WebClient = webClient;
    }

    public async Task<GameVersion> GetCurrentVersionAsync()
    {
        GameVersionDto gameVersionDTO = await _WebClient.GetCurrentVersionAsync();

        var gameVersion = gameVersionDTO.ConvertFromDto();
        return gameVersion;
    }

    public async Task<IReadOnlyCollection<StarSystem>> GetSystemsAsync()
    {
        ICollection<StarSystemDto> systemDtos = await _WebClient.GetSystemsAsync();

        var systems = systemDtos.ConvertFromDto();
        return systems;
    }

    public async Task<IReadOnlyCollection<Terminal>> GetTerminalsAsync(int starSystemId)
    {
        ICollection<TerminalDto> terminalDtos = await _WebClient.GetTerminalsAsync(starSystemId);

        var terminals = terminalDtos.ConvertFromDto();


        return terminals;
    }

    public async Task<IReadOnlyCollection<Commodity>> GetCommoditiesAsync()
    {
        ICollection<CommodityDto> commoditiesDtos = await _WebClient.GetCommoditiesAsync();

        var commodities = commoditiesDtos.ConvertFromDto();

        return commodities;
    }

    public async Task<IReadOnlyCollection<CommodityPrice>> GetCommodityPricesAsync(int terminalId)
    {
        ICollection<CommodityPriceDto> commodityPriceDtos = await _WebClient.GetCommodityPricesByTerminalIdAsync(terminalId);

        var commodityPrices = commodityPriceDtos.ConvertFromDto();

        return commodityPrices;
    }

    //public async Task<IReadOnlyCollection<Planet>> GetPlanetsAsync(string systemCode)
    //{
    //    ICollection<PlanetDto> planetDtos = await _WebClient.GetPlanetsAsync(systemCode);

    //    var planets = _Mapper.Map<List<Planet>>(planetDtos);
    //    return planets;
    //}

    //public async Task<IReadOnlyCollection<Satellite>> GetSatellitesAsync(string systemCode)
    //{
    //    ICollection<SatelliteDto> satelliteDtos = await _WebClient.GetSatellitesAsync(systemCode);

    //    var satellites = _Mapper.Map<List<Satellite>>(satelliteDtos);
    //    return satellites;
    //}

    //public async Task<IReadOnlyCollection<City>> GetCitiesAsync(string systemCode)
    //{
    //    ICollection<CityDto> citiesDtos = await _WebClient.GetCitiesAsync(systemCode);

    //    var cities = _Mapper.Map<List<City>>(citiesDtos);
    //    return cities;
    //}

    //public async Task<IReadOnlyCollection<Tradeport>> GetTradeportsAsync(string systemCode)
    //{
    //    ICollection<TradeportDto> tradeportsDtos = await _WebClient.GetTradeportsAsync(systemCode);

    //    var tradeports = _Mapper.Map<List<Tradeport>>(tradeportsDtos);
    //    return tradeports;
    //}

    //public async Task<Tradeport> GetTradeportAsync(string tradeportCode)
    //{
    //    TradeportDto tradeportDto = await _WebClient.GetTradeportAsync(tradeportCode);

    //    var tradeport = _Mapper.Map<Tradeport>(tradeportDto);
    //    return tradeport;
    //}

    //public async Task<PriceReportResponse> SubmitPriceReportAsync(PriceReport priceReport)
    //{
    //    var priceReportDto = _Mapper.Map<PriceReportDto>(priceReport);

    //    UexResponseDto<string> responseDto = await _WebClient.SubmitPriceReportAsync(priceReportDto);

    //    var priceReportResponse = _Mapper.Map<PriceReportResponse>(responseDto);

    //    return priceReportResponse;
    //}

    //public async Task<PriceReportsResponse> SubmitPriceReportsAsync(PriceReport[] priceReports)
    //{
    //    var priceReportDtos = _Mapper.Map<PriceReportDto[]>(priceReports);

    //    if (priceReportDtos is null)
    //    {
    //        return new PriceReportsResponse();
    //    }

    //    UexResponseDto<ICollection<string>> responseDtos = await _WebClient.SubmitPriceReportsAsync(priceReportDtos);

    //    var priceReportsResponse = _Mapper.Map<PriceReportsResponse>(responseDtos);

    //    return priceReportsResponse;
    //}
}
