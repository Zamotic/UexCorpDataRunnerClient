using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using UexCorpDataRunner.Business.Common;

namespace UexCorpDataRunner.Application.Common;
public class HttpLoggingHandler : DelegatingHandler
{
    private ILogger _Logger;

    public HttpLoggingHandler(ILogger logger) 
        : base()
    {
        _Logger = logger;
        InnerHandler = new HttpClientHandler();
    }

    public HttpLoggingHandler(HttpMessageHandler innerHandler, ILogger logger)
        : base(innerHandler)
    {
        _Logger = logger;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        try
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            _Logger.Debug("Request:");
            _Logger.Debug(request.ToString());
            if (request.Content != null)
            {
                _Logger.Debug(await request.Content.ReadAsStringAsync().ConfigureAwait(false));
            }

            HttpResponseMessage response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);

            _Logger.Debug("Response:");
            _Logger.Debug(response.ToString());
            if (response.Content != null)
            {
                _Logger.Debug(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
            }

            return response;
        }
        catch(Exception ex)
        {
            _Logger.Warning(ex, $"Error in {nameof(HttpLoggingHandler)}: {0}");
            return null;
        }
    }

}
