using AutoMapper;
using UexCorpDataRunner.Domain.DataRunner;
using UexCorpDataRunner.Persistence.Api.Uex.DataTransferObjects;

namespace UexCorpDataRunner.Persistence.Api.Uex;
public class UexCorpWebApiClientAdapter : IUexCorpWebApiClientAdapter
{
    private readonly IUexCorpWebApiClient _WebClient;
    private readonly IMapper _Mapper;

    public UexCorpWebApiClientAdapter(IUexCorpWebApiClient webClient, IMapper mapper)
    {
        _WebClient = webClient;
        _Mapper = mapper;
    }

    public async Task<GameVersion> GetCurrentVersionAsync()
    {
        GameVersionDto versionDTO = await _WebClient.GetCurrentVersionAsync();

        var version = _Mapper.Map<GameVersion> (versionDTO);
        return version;
    }

    public async Task<IReadOnlyCollection<Domain.DataRunner.StarSystem>> GetSystemsAsync()
    {
        ICollection<StarSystemDto> systemDtos = await _WebClient.GetSystemsAsync();

        var systems = _Mapper.Map<List<Domain.DataRunner.StarSystem>>(systemDtos);
        return systems;
    }

    public async Task<IReadOnlyCollection<Domain.DataRunner.Planet>> GetPlanetsAsync(string systemCode)
    {
        ICollection<PlanetDto> planetDtos = await _WebClient.GetPlanetsAsync(systemCode);

        var planets = _Mapper.Map<List<Domain.DataRunner.Planet>>(planetDtos);
        return planets;
    }

    public async Task<IReadOnlyCollection<Domain.DataRunner.Satellite>> GetSatellitesAsync(string systemCode)
    {
        ICollection<SatelliteDto> satelliteDtos = await _WebClient.GetSatellitesAsync(systemCode);

        var satellites = _Mapper.Map<List<Domain.DataRunner.Satellite>>(satelliteDtos);
        return satellites;
    }

    public async Task<IReadOnlyCollection<Domain.DataRunner.City>> GetCitiesAsync(string systemCode)
    {
        ICollection<CityDto> citiesDtos = await _WebClient.GetCitiesAsync(systemCode);

        var cities = _Mapper.Map<List<Domain.DataRunner.City>>(citiesDtos);
        return cities;
    }

    public async Task<IReadOnlyCollection<Domain.DataRunner.Tradeport>> GetTradeportsAsync(string systemCode)
    {
        ICollection<TradeportDto> tradeportsDtos = await _WebClient.GetTradeportsAsync(systemCode);

        var tradeports = _Mapper.Map<List<Domain.DataRunner.Tradeport>>(tradeportsDtos);
        return tradeports;
    }

    public async Task<Domain.DataRunner.Tradeport> GetTradeportAsync(string tradeportCode)
    {
        TradeportDto tradeportDto = await _WebClient.GetTradeportAsync(tradeportCode);

        var tradeport = _Mapper.Map<Domain.DataRunner.Tradeport>(tradeportDto);
        return tradeport;
    }

    public async Task<IReadOnlyCollection<Domain.DataRunner.Commodity>> GetCommoditiesAsync()
    {
        ICollection<CommodityDto> commoditiesDtos = await _WebClient.GetCommoditiesAsync();

        var commodities = _Mapper.Map<List<Domain.DataRunner.Commodity>>(commoditiesDtos);
        return commodities;
    }

    public async Task<PriceReportResponse> SubmitPriceReportAsync(PriceReport priceReport)
    {
        var priceReportDto = _Mapper.Map<PriceReportDto>(priceReport);

        UexResponseDto<string> responseDto = await _WebClient.SubmitPriceReportAsync(priceReportDto);

        var priceReportResponse = _Mapper.Map<PriceReportResponse>(responseDto);

        return priceReportResponse;
    }
}
