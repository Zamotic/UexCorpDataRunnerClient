using UexCorpDataRunner.Common.Logging;
using UexCorpDataRunner.Domain.Services;
using UexCorpDataRunner.Persistence.Api.Common;
using UexCorpDataRunner.Persistence.Api.Uex;

namespace UexCorpDataRunner.Persistence.Api.Mock.Uex;
public sealed class UexMockHttpClientFactory : IHttpClientFactory
{
    static object _lock = new object();
    HttpClient? _HttpClient;
    IUexCorpWebApiConfiguration _WebConfiguration;
    ILogger? _Logger;

    public UexMockHttpClientFactory(IUexCorpWebApiConfiguration webConfiguration, ILogger? logger = null)
    {
        _WebConfiguration = webConfiguration ?? throw new ArgumentNullException(nameof(webConfiguration));
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

    private static UexWebApiMockHttpMessageHandler _uexWebApiMockHttpMessageHandler = new UexWebApiMockHttpMessageHandler();
    public UexWebApiMockHttpMessageHandler GetMockHttpMessageHandler()
    {
        return _uexWebApiMockHttpMessageHandler;
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
            httpClient = new HttpClient(GetMockHttpMessageHandler());
        }

        if (logger is not null)
        {
            var loggingClientHandler = new HttpLoggingHandler(GetMockHttpMessageHandler(), logger);
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

        string decryptedApiKey = DecryptApiKey();

        httpClient.DefaultRequestHeaders.Clear();
        httpClient.DefaultRequestHeaders.Add("api_key", decryptedApiKey);

        return httpClient;
    }

    private string DecryptApiKey()
    {
        if (string.IsNullOrEmpty(_WebConfiguration.ApiKey) == true)
        {
            throw new Exception("ApiKey cannot be empty");
        }

        string decryptedKey = UexCorpDataRunner.Common.SimpleCipher.Decrypt(_WebConfiguration.ApiKey, Domain.Globals.SimpleCipherKey);
        return decryptedKey;
    }
}

