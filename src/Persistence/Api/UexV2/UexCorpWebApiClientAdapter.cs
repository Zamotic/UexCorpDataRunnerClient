using EnumsNET;
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

        var filteredTerimals = terminals.Where(x => x.Type?.Equals(TerminalType.Commodity.GetAttributes()?.Get<TerminalTypeValueAttribute>()?.TypeValue) == true).ToList();

        return filteredTerimals;
    }

    public async Task<IReadOnlyCollection<Commodity>> GetCommoditiesAsync()
    {
        ICollection<CommodityDto> commoditiesDtos = await _WebClient.GetCommoditiesAsync();

        var commodities = commoditiesDtos.ConvertFromDto();

        return commodities;
    }

    public async Task<IReadOnlyCollection<CommodityPrice>> GetCommodityPricesAsync(int terminalId)
    {
        ICollection<CommodityPriceDto> commodityPriceDtos = await _WebClient.GetCommodityPricesAsync(terminalId);

        var commodityPrices = commodityPriceDtos.ConvertFromDto();

        return commodityPrices;
    }

    public async Task<IReadOnlyCollection<Planet>> GetPlanetsAsync(int starSystemId)
    {
        ICollection<PlanetDto> planetDtos = await _WebClient.GetPlanetsAsync(starSystemId);

        var planets = planetDtos.ConvertFromDto();
        return planets;
    }

    public async Task<IReadOnlyCollection<Moon>> GetMoonsByStarSystemIdAsync(int starSystemId)
    {
        ICollection<MoonDto> moonDtos = await _WebClient.GetMoonsByStarSystemIdAsync(starSystemId);

        var moons = moonDtos.ConvertFromDto();
        return moons;
    }

    public async Task<IReadOnlyCollection<Moon>> GetMoonsByPlanetIdAsync(int planetId)
    {
        ICollection<MoonDto> moonDtos = await _WebClient.GetMoonsByPlanetIdAsync(planetId);

        var moons = moonDtos.ConvertFromDto();
        return moons;
    }

    public async Task<IReadOnlyCollection<Outpost>> GetOutpostsByStarSystemIdAsync(int starSystemId)
    {
        ICollection<OutpostDto> outpostDtos = await _WebClient.GetOutpostsByStarSystemIdAsync(starSystemId);

        var outposts = outpostDtos.ConvertFromDto();
        return outposts;
    }

    public async Task<IReadOnlyCollection<Outpost>> GetOutpostsByPlanetIdAsync(int planetId)
    {
        ICollection<OutpostDto> outpostDtos = await _WebClient.GetOutpostsByPlanetIdAsync(planetId);

        var outposts = outpostDtos.ConvertFromDto();
        return outposts;
    }

    public async Task<IReadOnlyCollection<Outpost>> GetOutpostsByMoonIdAsync(int moonId)
    {
        ICollection<OutpostDto> outpostDtos = await _WebClient.GetOutpostsByMoonIdAsync(moonId);

        var outposts = outpostDtos.ConvertFromDto();
        return outposts;
    }

    public async Task<IReadOnlyCollection<City>> GetCitiesByStarSystemIdAsync(int starSystemId)
    {
        ICollection<CityDto> cityDtos = await _WebClient.GetCitiesByStarSystemIdAsync(starSystemId);

        var cities = cityDtos.ConvertFromDto();
        return cities;
    }

    public async Task<IReadOnlyCollection<City>> GetCitiesByPlanetIdAsync(int planetId)
    {
        ICollection<CityDto> cityDtos = await _WebClient.GetCitiesByPlanetIdAsync(planetId);

        var cities = cityDtos.ConvertFromDto();
        return cities;
    }

    public async Task<IReadOnlyCollection<City>> GetCitiesByMoonIdAsync(int moonId)
    {
        ICollection<CityDto> cityDtos = await _WebClient.GetCitiesByMoonIdAsync(moonId);

        var cities = cityDtos.ConvertFromDto();
        return cities;
    }

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
