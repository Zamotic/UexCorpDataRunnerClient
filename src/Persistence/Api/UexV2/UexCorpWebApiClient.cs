using System.Net.Http.Headers;
using System.Text;
using UexCorpDataRunner.Common;
using UexCorpDataRunner.Domain.Services;
using UexCorpDataRunner.Persistence.Api.UexV2.DataTransferObjects;

namespace UexCorpDataRunner.Persistence.Api.UexV2;
public class UexCorpWebApiClient : IUexCorpWebApiClient
{
    readonly ISettingsService _SettingsService;
    readonly IUexCorpWebApiConfiguration _WebApiConfiguration;
    readonly HttpClient _HttpClient;

    public UexCorpWebApiClient(IUexCorpWebApiConfiguration webApiConfiguration,
                                HttpClient httpClient,
                                ISettingsService settingsService)
    {
        _WebApiConfiguration = webApiConfiguration;
        _HttpClient = httpClient;
        _SettingsService = settingsService;

        if (_WebApiConfiguration.WebApiEndPointUrl is null)
        {
            throw new Exception($"{nameof(_WebApiConfiguration.WebApiEndPointUrl)} cannot be null.");
        }

        if (httpClient is null)
        {
            throw new Exception($"{nameof(httpClient)} cannot be null.");
        }

        _HttpClient.BaseAddress = new Uri(_WebApiConfiguration.WebApiEndPointUrl);

        string decryptedApiKey = DecryptApiKey();

        _HttpClient.DefaultRequestHeaders.Clear();

        _HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", decryptedApiKey);
    }

    #region     Generic Methods
    private string DecryptApiKey()
    {
        if (string.IsNullOrEmpty(_WebApiConfiguration.ApiKey) == true)
        {
            throw new Exception("ApiKey cannot be empty");
        }

        AesEncryption aesEncryption = new AesEncryption(Domain.Globals.SimpleCipherKey);
        string decryptedKey = aesEncryption.Decrypt(_WebApiConfiguration.ApiKey);
        return decryptedKey;
    }

    protected async Task<ICollection<T>> GenericGetCollectionAsync<T>(string endPointValue) where T : class
    {
        // Set the full request URI
        string absolutePath = $"{_WebApiConfiguration.DataRunnerEndpointPath}{endPointValue}";
        if (absolutePath.EndsWith("/") == false)
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

        var responseObject = System.Text.Json.JsonSerializer.Deserialize<UexResponseDto<ICollection<T>>>(responseJson);

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

    protected async Task<T> GenericGetSingleAsync<T>(string endPointValue) where T : class, new()
    {
        // Set the full request URI
        string absolutePath = $"{_WebApiConfiguration.DataRunnerEndpointPath}{endPointValue}";
        if (absolutePath.EndsWith("/") == false)
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
            return new T();
        }

        if (responseObject.Data is null)
        {
            return new T();
        }

        if (responseObject.Code.Equals(200) == true)
        {
            return responseObject.Data;
        }

        return new T();
    }
    #endregion  Generic Methods

    /// <summary>
    /// Returns a IList<SystemDto> objects
    /// </summary>
    /// <returns>Collection containing a list of SystemDto records returned from the API</returns>
    public async Task<GameVersionDto> GetCurrentVersionAsync()
    {
        string endPointValue = "game_versions";

        return await GenericGetSingleAsync<GameVersionDto>(endPointValue);
    }

    /// <summary>
    /// Returns a IList<SystemDto> objects
    /// </summary>
    /// <returns>Collection containing a list of SystemDto records returned from the API</returns>
    public async Task<ICollection<StarSystemDto>> GetSystemsAsync()
    {
        string endPointValue = "star_systems";

        return await GenericGetCollectionAsync<StarSystemDto>(endPointValue);
    }

    /// <summary>
    /// Returns a IList<PlanetDto> objects
    /// </summary>
    /// <paramref name="starSystemId">A id representing the system to return PlanetDto objects for</paramref>
    /// <returns>Collection containing a list of PlanetDto records returned from the API</returns>
    public async Task<ICollection<PlanetDto>> GetPlanetsAsync(int starSystemId)
    {
        string endPointValue = $"planets/id_star_system/{starSystemId}";

        return await GenericGetCollectionAsync<PlanetDto>(endPointValue);
    }

    /// <summary>
    /// Returns a IList<MoonDto> objects
    /// </summary>
    /// <paramref name="starSystemId">A id representing the system to return MoonDto objects for</paramref>
    /// <returns>Collection containing a list of MoonDto records returned from the API</returns>
    public async Task<ICollection<MoonDto>> GetMoonsByStarSystemIdAsync(int starSystemId)
    {
        string endPointValue = $"moons/id_star_system/{starSystemId}";

        return await GenericGetCollectionAsync<MoonDto>(endPointValue);
    }

    /// <summary>
    /// Returns a IList<MoonDto> objects
    /// </summary>
    /// <paramref name="planetId">A id representing the planet to return MoonDto objects for</paramref>
    /// <returns>Collection containing a list of MoonDto records returned from the API</returns>
    public async Task<ICollection<MoonDto>> GetMoonsByPlanetIdAsync(int planetId)
    {
        string endPointValue = $"moons/id_planet/{planetId}";

        return await GenericGetCollectionAsync<MoonDto>(endPointValue);
    }

    /// <summary>
    /// Returns a IList<SpaceStationDto> objects
    /// </summary>
    /// <paramref name="starSystemId">An id representing the system to return SpaceStationDto objects for</paramref>
    /// <returns>Collection containing a list of SpaceStationDto records returned from the API</returns>
    public async Task<ICollection<SpaceStationDto>> GetSpaceStationsByStarSystemIdAsync(int starSystemId)
    {
        string endPointValue = $"space_stations/id_star_system/{starSystemId}";

        return await GenericGetCollectionAsync<SpaceStationDto>(endPointValue);
    }

    /// <summary>
    /// Returns a IList<SpaceStationDto> objects
    /// </summary>
    /// <paramref name="planetId">An id representing the planet to return SpaceStationDto objects for</paramref>
    /// <returns>Collection containing a list of SpaceStationDto records returned from the API</returns>
    public async Task<ICollection<SpaceStationDto>> GetSpaceStationsByPlanetIdAsync(int planetId)
    {
        string endPointValue = $"space_stations/id_planet/{planetId}";

        return await GenericGetCollectionAsync<SpaceStationDto>(endPointValue);
    }

    /// <summary>
    /// Returns a IList<SpaceStationDto> objects
    /// </summary>
    /// <paramref name="moonId">An id representing the moon to return SpaceStationDto objects for</paramref>
    /// <returns>Collection containing a list of SpaceStationDto records returned from the API</returns>
    public async Task<ICollection<SpaceStationDto>> GetSpaceStationsByMoonIdAsync(int moonId)
    {
        string endPointValue = $"space_stations/id_moon/{moonId}";

        return await GenericGetCollectionAsync<SpaceStationDto>(endPointValue);
    }

    /// <summary>
    /// Returns a IList<CityDto> objects
    /// </summary>
    /// <paramref name="starSystemId">An id representing the system to return CityDto objects for</paramref>
    /// <returns>Collection containing a list of CityDto records returned from the API</returns>
    public async Task<ICollection<CityDto>> GetCitiesByStarSystemIdAsync(int starSystemId)
    {
        string endPointValue = $"cities/id_star_system/{starSystemId}";

        return await GenericGetCollectionAsync<CityDto>(endPointValue);
    }

    /// <summary>
    /// Returns a IList<CityDto> objects
    /// </summary>
    /// <paramref name="planetId">An id representing the planet to return CityDto objects for</paramref>
    /// <returns>Collection containing a list of CityDto records returned from the API</returns>
    public async Task<ICollection<CityDto>> GetCitiesByPlanetIdAsync(int planetId)
    {
        string endPointValue = $"cities/id_planet/{planetId}";

        return await GenericGetCollectionAsync<CityDto>(endPointValue);
    }

    /// <summary>
    /// Returns a IList<CityDto> objects
    /// </summary>
    /// <paramref name="moonId">An id representing the moon to return CityDto objects for</paramref>
    /// <returns>Collection containing a list of CityDto records returned from the API</returns>
    public async Task<ICollection<CityDto>> GetCitiesByMoonIdAsync(int moonId)
    {
        string endPointValue = $"cities/id_moon/{moonId}";

        return await GenericGetCollectionAsync<CityDto>(endPointValue);
    }

    /// <summary>
    /// Returns a IList<OutpostDto> objects
    /// </summary>
    /// <paramref name="starSystemId">An id representing the Star System to return OutpostDto objects for</paramref>
    /// <returns>Collection containing a list of OutpostDto records returned from the API</returns>
    public async Task<ICollection<OutpostDto>> GetOutpostsByStarSystemIdAsync(int starSystemId)
    {
        string endPointValue = $"outposts/id_star_system/{starSystemId}";

        return await GenericGetCollectionAsync<OutpostDto>(endPointValue);
    }

    /// <summary>
    /// Returns a IList<OutpostDto> objects
    /// </summary>
    /// <paramref name="planetId">An id representing the Planet to return OutpostDto objects for</paramref>
    /// <returns>Collection containing a list of OutpostDto records returned from the API</returns>
    public async Task<ICollection<OutpostDto>> GetOutpostsByPlanetIdAsync(int planetId)
    {
        string endPointValue = $"outposts/id_planet/{planetId}";

        return await GenericGetCollectionAsync<OutpostDto>(endPointValue);
    }

    /// <summary>
    /// Returns a IList<OutpostDto> objects
    /// </summary>
    /// <paramref name="moonId">An id representing the Moon to return OutpostDto objects for</paramref>
    /// <returns>Collection containing a list of OutpostDto records returned from the API</returns>
    public async Task<ICollection<OutpostDto>> GetOutpostsByMoonIdAsync(int moonId)
    {
        string endPointValue = $"outposts/id_moon/{moonId}";

        return await GenericGetCollectionAsync<OutpostDto>(endPointValue);
    }

    /// <summary>
    /// Returns a IList<CommodityDto> objects
    /// </summary>
    /// <returns>Collection containing a list of CommodityDto records returned from the API</returns>
    public async Task<ICollection<CommodityDto>> GetCommoditiesAsync()
    {
        string endPointValue = $"commodities/";

        return await GenericGetCollectionAsync<CommodityDto>(endPointValue);
    }

    /// <summary>
    /// Returns a IList<CommodityPriceDto> objects
    /// </summary>
    /// <paramref name="commodityId"/>An id representing the commodity to return CommodityPriceDto objects for</paramref>
    /// <returns>Collection containing a list of CommodityDto records returned from the API</returns>
    public async Task<ICollection<CommodityPriceDto>> GetCommodityPricesByCommodityIdAsync(int commodityId)
    {
        string endPointValue = $"commodities_prices/id_commodity/{commodityId}";

        return await GenericGetCollectionAsync<CommodityPriceDto>(endPointValue);
    }

    /// <summary>
    /// Returns a IList<CommodityPriceDto> objects
    /// </summary>
    /// <paramref name="commodityId"/>An id representing the terminal to return CommodityPriceDto objects for</paramref>
    /// <returns>Collection containing a list of CommodityDto records returned from the API</returns>
    public async Task<ICollection<CommodityPriceDto>> GetCommodityPricesAsync(int terminalId)
    {
        string endPointValue = $"commodities_prices/id_terminal/{terminalId}";

        return await GenericGetCollectionAsync<CommodityPriceDto>(endPointValue);
    }

    /// <summary>
    /// Returns a IList<TerminalDto> objects
    /// </summary>
    /// <paramref name="starSystemId"/>An id representing the star system id to return TerminalDto objects for</paramref>
    /// <returns>Collection containing a list of TerminalDto records returned from the API</returns>
    public async Task<ICollection<TerminalDto>> GetTerminalsAsync(int starSystemId)
    {
        string endPointValue = $"terminals/id_star_system/{starSystemId}";

        return await GenericGetCollectionAsync<TerminalDto>(endPointValue);
    }

    public async Task<UexResponseDto<DataSubmitResponseDto>> SubmitDataAsync(DataSubmitDto source)
    {
        string endPointValue = $"data_submit/";

        // Set the full request URI
        string absolutePath = $"{_WebApiConfiguration.DataRunnerEndpointPath}{endPointValue}";
        if (absolutePath.EndsWith("/") == false)
        {
            absolutePath += "/";
        }

        string jsonContent = System.Text.Json.JsonSerializer.Serialize(source);
        string jsonResponse = null;

        _HttpClient.DefaultRequestHeaders.Add("secret_key", _SettingsService?.Settings?.UserSecretKey);

        using (var content = new StringContent(jsonContent))
        using (HttpResponseMessage response = await _HttpClient.PostAsync(absolutePath, content).ConfigureAwait(false))
        {
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body.
                jsonResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            }
            else
            {
                response.EnsureSuccessStatusCode();
            }
        }

        UexResponseDto<DataSubmitResponseDto>? responseObject = null;
        try
        {
            responseObject = System.Text.Json.JsonSerializer.Deserialize<UexResponseDto<DataSubmitResponseDto>>(jsonResponse!);
        }
        catch
        {
            try
            {
                var responseObjectTemp = System.Text.Json.JsonSerializer.Deserialize<UexResponseDto<string>>(jsonResponse!);
                //if (responseObjectTemp is not null)
                //{
                //    responseObject = new UexResponseDto<DataSubmitResponseDto>()
                //    {
                //        Status = responseObjectTemp.Status,
                //        Code = responseObjectTemp.Code,
                //        Data = new List<string>() { string.IsNullOrEmpty(responseObjectTemp.Data) ? string.Empty : responseObjectTemp.Data }
                //    };
                //}
            }
            finally
            {

            }
        }

        if (responseObject is null)
        {
            return new UexResponseDto<DataSubmitResponseDto>()
            {
                Status = "No Response Received",
                Code = 400,
                Data = new DataSubmitResponseDto()
            };
        }

        return responseObject;
    }
}
