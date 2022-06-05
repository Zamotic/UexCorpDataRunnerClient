using UexCorpDataRunner.Persistence.Api.Common;
using UexCorpDataRunner.Persistence.Api.Uex.DataTransferObjects;

namespace UexCorpDataRunner.Persistence.Api.Uex;
public class UexCorpWebApiClient : IUexCorpWebApiClient
{
    readonly IUexCorpWebApiConfiguration _WebApiConfiguration;
    readonly HttpClient _HttpClient;

    public UexCorpWebApiClient(IUexCorpWebApiConfiguration webApiConfiguration,
                                IHttpClientFactory httpClientFactory)
    {
        _WebApiConfiguration = webApiConfiguration;
        _HttpClient = httpClientFactory.GetHttpClient();
    }

    /// <summary>
    /// Returns a IList<SystemDto> objects
    /// </summary>
    /// <returns>Collection containing a list of SystemDto records returned from the API</returns>
    public async Task<ICollection<SystemDto>> GetSystemsAsync()
    {
        string endPointValue = "systems";

        return await GenericGetAsync<SystemDto>(endPointValue);
    }

    /// <summary>
    /// Returns a IList<PlanetDto> objects
    /// </summary>
    /// <paramref name="systemCode">A string code representing the system to return PlanetDto objects for</paramref>
    /// <returns>Collection containing a list of PlanetDto records returned from the API</returns>
    public async Task<ICollection<PlanetDto>> GetPlanetsAsync(string systemCode)
    {
        string endPointValue = $"planets/{systemCode}";

        return await GenericGetAsync<PlanetDto>(endPointValue);
    }

    /// <summary>
    /// Returns a IList<SatelliteDto> objects
    /// </summary>
    /// <paramref name="systemCode">A string code representing the system to return SatelliteDto objects for</paramref>
    /// <returns>Collection containing a list of SatelliteDto records returned from the API</returns>
    public async Task<ICollection<SatelliteDto>> GetSatellitesAsync(string systemCode)
    {
        string endPointValue = $"satellites/{systemCode}";

        return await GenericGetAsync<SatelliteDto>(endPointValue);
    }

    /// <summary>
    /// Returns a IList<CityDto> objects
    /// </summary>
    /// <paramref name="systemCode">A string code representing the system to return CityDto objects for</paramref>
    /// <returns>Collection containing a list of CityDto records returned from the API</returns>
    public async Task<ICollection<CityDto>> GetCitiesAsync(string systemCode)
    {
        string endPointValue = $"cities/{systemCode}";

        return await GenericGetAsync<CityDto>(endPointValue);
    }

    /// <summary>
    /// Returns a IList<TradeportDto> objects
    /// </summary>
    /// <paramref name="systemCode">A string code representing the system to return TradeportDto objects for</paramref>
    /// <returns>Collection containing a list of TradeportDto records returned from the API</returns>
    public async Task<ICollection<TradeportDto>> GetTradeportsAsync(string systemCode)
    {
        string endPointValue = $"tradeports/{systemCode}";

        return await GenericGetAsync<TradeportDto>(endPointValue);
    }

    /// <summary>
    /// Returns a IList<CommodityDto> objects
    /// </summary>
    /// <returns>Collection containing a list of CommodityDto records returned from the API</returns>
    public async Task<ICollection<CommodityDto>> GetCommoditiesAsync()
    {
        string endPointValue = $"commodities/";

        return await GenericGetAsync<CommodityDto>(endPointValue);
    }

    protected async Task<ICollection<T>> GenericGetAsync<T>(string endPointValue) where T : class
    {
        // Set the full request URI
        string absolutePath = $"{_WebApiConfiguration.DataRunnerEndpointPath}{endPointValue}";
        if(absolutePath.EndsWith("/") == false)
        {
            absolutePath += "/";
        }

        string responseJson = string.Empty;
        using (HttpResponseMessage response = await _HttpClient.GetAsync(absolutePath).ConfigureAwait(false))
        {
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body.
                responseJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            }
            else
            {
                response.EnsureSuccessStatusCode();
            }
        }

        var responseObject = System.Text.Json.JsonSerializer.Deserialize<UexResponseDto<T>>(responseJson);

        if (responseObject is null)
        {
            return new List<T>();
        }

        if (responseObject.Data is null)
        {
            return new List<T>();
        }

        if (responseObject.Code.Equals(200) == true)
        {
            return responseObject.Data;
        }

        return new List<T>();
    }

}
