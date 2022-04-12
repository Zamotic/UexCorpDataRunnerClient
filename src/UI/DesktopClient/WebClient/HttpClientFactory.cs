using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UexCorpDataRunner.DesktopClient.Core;

namespace UexCorpDataRunner.DesktopClient.WebClient;
public sealed class HttpClientFactory : IHttpClientFactory
{
    static object _lock = new object();
    static HttpClient _HttpClient;

    public HttpClient GetHttpClient(ILogger? logger = null)
    {
        lock (_lock)
        {
            if (_HttpClient != null)
            {
                return _HttpClient;
            }

            _HttpClient = InitializeHttpClient(logger);
        }

        return _HttpClient;
    }

    static HttpClient InitializeHttpClient(ILogger? logger = null)
    {
        var configuration = UexCorpWebApiConfiguration.GetConfig();

        if (configuration is null)
        {
            throw new Exception($"{nameof(configuration)} cannot be null.");
        }

        HttpClient? httpClient = null;

        if (logger is null)
        {
            httpClient = new HttpClient();
        }

        if (logger is not null)
        {
            var loggingClientHandler = new HttpLoggingHandler(logger);
            httpClient = new HttpClient(loggingClientHandler);
        }

        if (configuration.WebApiEndPointUrl is null)
        {
            throw new Exception($"{nameof(configuration.WebApiEndPointUrl)} cannot be null.");
        }

        if (httpClient is null)
        {
            throw new Exception($"{nameof(httpClient)} cannot be null.");
        }

        httpClient.BaseAddress = new Uri(configuration.WebApiEndPointUrl);

        httpClient.DefaultRequestHeaders.Add(Globals.WebApiApiKeyHeaderName, configuration.ApiKey);

        return httpClient;
    }
}

