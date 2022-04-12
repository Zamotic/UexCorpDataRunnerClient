using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace UexCorpDataRunner.DesktopClient.WebClient;
public class UexCorpWebApiClient
{
    readonly IUexCorpWebApiConfiguration _WebApiConfiguration;
    readonly HttpClient _HttpClient;

    public UexCorpWebApiClient(IUexCorpWebApiConfiguration webApiConfiguration,
                                IHttpClientFactory httpClientFactory)
    {
        _WebApiConfiguration = webApiConfiguration;
        _HttpClient = httpClientFactory.GetHttpClient();
    }

    ///// <summary>
    ///// Returns a TrackingDetailDto object based on the passed in tracking number
    ///// </summary>
    ///// <param name="trackingNumber">A tracking number to get the tracking details for</param>
    ///// <returns>TrackingDetailDto containing a list of details and events returned from the API</returns>
    //public async Task<TrackingDetailDto> GetTrackingDetail(string trackingNumber)
    //{
    //    // Set the full request URI
    //    string absolutePath = $"{_TrackingConfiguration.TrackingDetailEndpointPath}?trackingNumberVendor={trackingNumber}";

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

    //    if (responseObject.ResponseStatus.StatusCode.Equals(200) == true)
    //    {
    //        return responseObject.TrackingDetails.First();
    //    }

    //    return null;
    //}

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
