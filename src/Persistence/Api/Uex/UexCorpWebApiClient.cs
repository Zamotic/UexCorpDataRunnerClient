﻿using UexCorpDataRunner.Persistence.Api.Common;
using UexCorpDataRunner.Persistence.Api.Uex.DataTransferObjects;

namespace UexCorpDataRunner.Persistence.Api.Uex;
public class UexCorpWebApiClient : IUexCorpWebApiClient
{
    readonly IUexCorpWebApiConfiguration _WebApiConfiguration;
    readonly HttpClient _HttpClient;

    public UexCorpWebApiClient(IUexCorpWebApiConfiguration webApiConfiguration,
                                HttpClient httpClient)
    {
        _WebApiConfiguration = webApiConfiguration;
        _HttpClient = httpClient;

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
        _HttpClient.DefaultRequestHeaders.Add("api_key", decryptedApiKey);
    }

    private string DecryptApiKey()
    {
        if (string.IsNullOrEmpty(_WebApiConfiguration.ApiKey) == true)
        {
            throw new Exception("ApiKey cannot be empty");
        }

        string decryptedKey = UexCorpDataRunner.Common.SimpleCipher.Decrypt(_WebApiConfiguration.ApiKey, Domain.Globals.SimpleCipherKey);
        return decryptedKey;
    }

    /// <summary>
    /// Returns a IList<SystemDto> objects
    /// </summary>
    /// <returns>Collection containing a list of SystemDto records returned from the API</returns>
    public async Task<ICollection<SystemDto>> GetSystemsAsync()
    {
        string endPointValue = "star_systems";

        return await GenericGetCollectionAsync<SystemDto>(endPointValue);
    }

    /// <summary>
    /// Returns a IList<PlanetDto> objects
    /// </summary>
    /// <paramref name="systemCode">A string code representing the system to return PlanetDto objects for</paramref>
    /// <returns>Collection containing a list of PlanetDto records returned from the API</returns>
    public async Task<ICollection<PlanetDto>> GetPlanetsAsync(string systemCode)
    {
        string endPointValue = $"planets/system/{systemCode}";

        return await GenericGetCollectionAsync<PlanetDto>(endPointValue);
    }

    /// <summary>
    /// Returns a IList<SatelliteDto> objects
    /// </summary>
    /// <paramref name="systemCode">A string code representing the system to return SatelliteDto objects for</paramref>
    /// <returns>Collection containing a list of SatelliteDto records returned from the API</returns>
    public async Task<ICollection<SatelliteDto>> GetSatellitesAsync(string systemCode)
    {
        string endPointValue = $"satellites/system/{systemCode}";

        return await GenericGetCollectionAsync<SatelliteDto>(endPointValue);
    }

    /// <summary>
    /// Returns a IList<CityDto> objects
    /// </summary>
    /// <paramref name="systemCode">A string code representing the system to return CityDto objects for</paramref>
    /// <returns>Collection containing a list of CityDto records returned from the API</returns>
    public async Task<ICollection<CityDto>> GetCitiesAsync(string systemCode)
    {
        string endPointValue = $"cities/system/{systemCode}";

        return await GenericGetCollectionAsync<CityDto>(endPointValue);
    }

    /// <summary>
    /// Returns a IList<TradeportDto> objects
    /// </summary>
    /// <paramref name="systemCode">A string code representing the system to return TradeportDto objects for</paramref>
    /// <returns>Collection containing a list of TradeportDto records returned from the API</returns>
    public async Task<ICollection<TradeportDto>> GetTradeportsAsync(string systemCode)
    {
        string endPointValue = $"tradeports/system/{systemCode}";

        return await GenericGetCollectionAsync<TradeportDto>(endPointValue);
    }

    /// <summary>
    /// Returns a TradeportDto object
    /// </summary>
    /// <paramref name="tradeportCode">A string code representing the tradeport to return a specific TradeportDto object for</paramref>
    /// <returns>A TradeportDto record returned from the API</returns>
    public async Task<TradeportDto> GetTradeportAsync(string tradeportCode)
    {
        string endPointValue = $"tradeport/code/{tradeportCode}";

        return await GenericGetSingleAsync<TradeportDto>(endPointValue);
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

    public async Task<UexResponseDto<string>> SubmitPriceReportAsync(PriceReportDto priceReport)
    {
        string endPointValue = $"sr/";

        // Set the full request URI
        string absolutePath = $"{_WebApiConfiguration.DataRunnerEndpointPath}{endPointValue}";
        if (absolutePath.EndsWith("/") == false)
        {
            absolutePath += "/";
        }

        string contentString = System.Text.Json.JsonSerializer.Serialize(priceReport);

        string responseJson = string.Empty;
        //using (var content = new StringContent(contentString))

        var contentDictionary = GetPriceReportContent(priceReport);

        using (var content = new FormUrlEncodedContent(contentDictionary))
        using (HttpResponseMessage response = await _HttpClient.PostAsync(absolutePath, content).ConfigureAwait(false))
        {
            //if (response.IsSuccessStatusCode)
            //{
                // Parse the response body.
                responseJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            //}
            //else
            //{
            //    response.EnsureSuccessStatusCode();
            //}
        }

        var responseObject = System.Text.Json.JsonSerializer.Deserialize<UexResponseDto<string>>(responseJson);

        if (responseObject is null)
        {
            return new UexResponseDto<string>()
            {
                Status = "No Response Received",
                Code = 400,
                Data = "0"
            };
        }

        return responseObject;
    }

    private Dictionary<string,string> GetPriceReportContent(PriceReportDto priceReport)
    {
        var json = System.Text.Json.JsonSerializer.Serialize(priceReport);
        var contentDictionary = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string,string>>(json);

        if(contentDictionary is null)
        {
            return new Dictionary<string, string>();
        }

        return contentDictionary;
    }

    protected async Task<ICollection<T>> GenericGetCollectionAsync<T>(string endPointValue) where T : class
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

}
