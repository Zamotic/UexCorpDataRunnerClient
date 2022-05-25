using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UexCorpDataRunner.Application.Common;
using UexCorpDataRunner.Business.Common;
using UexCorpDataRunner.Business.Settings;
using UexCorpDataRunner.Domain.Configurations;

namespace UexCorpDataRunner.DesktopClient.WebClient;
public sealed class HttpClientFactory : IHttpClientFactory
{
    static object _lock = new object();
    HttpClient? _HttpClient;
    IUexCorpWebApiConfiguration _WebConfiguration;
    ISettingsService _SettingsService;
    ILogger? _Logger;

    public HttpClientFactory(IUexCorpWebApiConfiguration webConfiguration, ISettingsService settingsService, ILogger? logger = null)
    {
        _WebConfiguration = webConfiguration ?? throw new ArgumentNullException(nameof(webConfiguration));
        _SettingsService = settingsService ?? throw new ArgumentNullException(nameof(settingsService));
        _Logger = logger;
    }

    public HttpClient GetHttpClient()
    {
        lock (_lock)
        {
            if (_HttpClient != null)
            {
                return _HttpClient;
            }

            _HttpClient = InitializeHttpClient(_Logger);
        }

        return _HttpClient;
    }

    private HttpClient InitializeHttpClient(ILogger? logger = null)
    {
        if (_WebConfiguration is null)
        {
            throw new Exception($"{nameof(_WebConfiguration)} cannot be null.");
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

        if (_WebConfiguration.WebApiEndPointUrl is null)
        {
            throw new Exception($"{nameof(_WebConfiguration.WebApiEndPointUrl)} cannot be null.");
        }

        if (httpClient is null)
        {
            throw new Exception($"{nameof(httpClient)} cannot be null.");
        }

        httpClient.BaseAddress = new Uri(_WebConfiguration.WebApiEndPointUrl);

        httpClient.DefaultRequestHeaders.Add("api_key", _SettingsService.Settings?.UserApiKey);

        return httpClient;
    }
}

