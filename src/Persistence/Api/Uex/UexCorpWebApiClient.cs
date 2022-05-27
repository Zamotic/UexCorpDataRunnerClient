using AutoMapper;
using UexCorpDataRunner.Persistence.Api.Common;
using UexCorpDataRunner.Persistence.Api.Uex.DataTransferObjects;

namespace UexCorpDataRunner.Persistence.Api.Uex;
public class UexCorpWebApiClient : IUexCorpWebApiClient
{
    readonly IUexCorpWebApiConfiguration _WebApiConfiguration;
    readonly HttpClient _HttpClient;
    readonly IMapper _Mapper;

    public UexCorpWebApiClient(IUexCorpWebApiConfiguration webApiConfiguration,
                                IHttpClientFactory httpClientFactory,
                                IMapper mapper)
    {
        _WebApiConfiguration = webApiConfiguration;
        _HttpClient = httpClientFactory.GetHttpClient();
        _Mapper = mapper;
    }

    /// <summary>
    /// Returns a IList<System> objects
    /// </summary>
    /// <returns>TrackingDetailDto containing a list of details and events returned from the API</returns>
    public async Task<IList<Domain.DataRunner.System>> GetSystems()
    {
        // Set the full request URI
        string absolutePath = $"{_WebApiConfiguration.DataRunnerEndpointPath}systems/";

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

        var responseObject = System.Text.Json.JsonSerializer.Deserialize<UexResponseDto<SystemDto>>(responseJson);

        if (responseObject is null)
        {
            return new List<Domain.DataRunner.System>();
        }

        if (responseObject.Code.Equals(200) == true)
        {
            var systems = _Mapper.Map<List<Domain.DataRunner.System>>(responseObject.Data);
            return systems;
        }

        return new List<Domain.DataRunner.System>();
    }

    ///// <summary>
    ///// Returns a collection of TrackingDetailDto objects based on the passed in collection of tracking numbers
    ///// </summary>
    ///// <param name="trackingNumbers">A collection of tracking numbers to get the tracking details for</param>
    ///// <returns>A collection of TrackingDetailDto objects containing a list of details and events returned from the API</returns>
    //public async Task<ICollection<TrackingDetailDto>> GetTrackingDetails(IEnumerable<string> trackingNumbers)
    //{
    //    List<TrackingDetailDto> responseList = new List<TrackingDetailDto>();

    //    int apiCallLength = 10;

    //    for (int i = 0; i < trackingNumbers.Count(); i = i + apiCallLength)
    //    {
    //        var items = trackingNumbers.Skip(i).Take(apiCallLength);
    //        var currentDetails = await GetMultipleTrackingDetails(items);
    //        responseList.AddRange(currentDetails);
    //        //Console.WriteLine($"Current Round: {i}, Calc: {i % (10 * apiCallLength)}");
    //        //if(i % (10 * apiCallLength) == 0)
    //        //{
    //        //    System.Threading.Thread.Sleep(1000);
    //        //}
    //        if (i > 0)
    //        {
    //            if (i % (50 * apiCallLength) == 0)
    //            {
    //                System.Threading.Thread.Sleep(45000);
    //            }
    //        }

    //    }

    //    return responseList;
    //}

    //private async Task<ICollection<TrackingDetailDto>> GetMultipleTrackingDetails(IEnumerable<string> trackingNumbers)
    //{
    //    string concatenatedTrackingNumbers = string.Join(",", trackingNumbers);

    //    // Set the full request URI
    //    string absolutePath = $"{_TrackingConfiguration.TrackingDetailEndpointPath}?trackingNumberVendor={concatenatedTrackingNumbers}";

    //    string responseJson = string.Empty;
    //    using (HttpResponseMessage response = await _HttpClient.GetAsync(absolutePath).ConfigureAwait(false))
    //    {
    //        if (response.IsSuccessStatusCode)
    //        {
    //            // Parse the response body.
    //            responseJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
    //        }
    //        else
    //        {
    //            response.EnsureSuccessStatusCode();
    //        }
    //    }

    //    TrackingDetailResponse responseObject = TrackingDetailResponse.FromJson(responseJson);

    //    if (WasStatusCodeSuccessful(responseObject.ResponseStatus.StatusCode) == true)
    //    {
    //        return responseObject.TrackingDetails;
    //    }

    //    return new List<TrackingDetailDto>();
    //}

    ///// <summary>
    ///// Returns a ITrackingPackage object based on the passed in tracking number
    ///// </summary>
    ///// <param name="trackingNumber">A tracking number to get the tracking details for</param>
    ///// <returns>ITrackingPackage object containing a list of details and events returned from the API</returns>
    //public async Task<ITrackingPackage> GetTrackingPackage(string trackingNumber)
    //{
    //    var responseTrackingDetailDto = await GetTrackingDetail(trackingNumber);
    //    var returnTrackingPackage = _TrackingDetailDtoConverter.ConvertFrom(responseTrackingDetailDto);
    //    return returnTrackingPackage;
    //}

    ///// <summary>
    ///// Returns a collection of ITrackingPackage objects based on the passed in collection of tracking numbers
    ///// </summary>
    ///// <param name="trackingNumbers">A collection of tracking numbers to get the tracking details for</param>
    ///// <returns>A collection of ITrackingPackage objects containing a list of details and events returned from the API</returns>
    //public async Task<ICollection<ITrackingPackage>> GetTrackingPackages(IEnumerable<string> trackingNumbers)
    //{
    //    ICollection<TrackingDetailDto> responseList = await GetTrackingDetails(trackingNumbers);
    //    List<ITrackingPackage> returnList = new List<ITrackingPackage>();

    //    foreach (TrackingDetailDto trackingDetailDto in responseList)
    //    {
    //        if (trackingDetailDto is null)
    //        {
    //            continue;
    //        }

    //        var returnTrackingPackage = _TrackingDetailDtoConverter.ConvertFrom(trackingDetailDto);

    //        if (returnTrackingPackage is null)
    //        {
    //            continue;
    //        }

    //        returnList.Add(returnTrackingPackage);
    //    }

    //    return returnList;
    //}

    //private bool WasStatusCodeSuccessful(int statusCode)
    //{
    //    if (statusCode < 200)
    //    {
    //        return false;
    //    }

    //    if (statusCode > 210)
    //    {
    //        return false;
    //    }

    //    return true;
    //}

}
