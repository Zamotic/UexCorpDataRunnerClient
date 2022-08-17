using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace UexCorpDataRunner.Persistence.Api.Mock.Common;
public class FakeResponseHandler : DelegatingHandler
{
    public FakeResponseHandler()
        : base()
    {
    }

    public FakeResponseHandler(HttpMessageHandler innerHandler)
    : base(innerHandler)
    {
    }

    private readonly Dictionary<Uri, HttpResponseMessage> _FakeResponses = new Dictionary<Uri, HttpResponseMessage>();

    public void AddFakeResponse(Uri uri, HttpResponseMessage responseMessage)
    {
        _FakeResponses.Add(uri, responseMessage);
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
    {
        if(request.RequestUri is null)
        {
            throw new ArgumentNullException(nameof(request.RequestUri));
        }

        if (_FakeResponses.ContainsKey(request.RequestUri))
        {
            var clonedResponse = await CloneHttpRequestMessageAsync(_FakeResponses[request.RequestUri]);

            if(clonedResponse is not null)
            {
                return clonedResponse;
            }
        }
        return new HttpResponseMessage(HttpStatusCode.NotFound) { RequestMessage = request };
    }

    private static async Task<HttpResponseMessage> CloneHttpRequestMessageAsync(HttpResponseMessage response)
    {
        HttpResponseMessage clone = new HttpResponseMessage(response.StatusCode);

        // Copy the request's content (via a MemoryStream) into the cloned object
        var ms = new MemoryStream();
        if (response.Content != null)
        {
            await response.Content.CopyToAsync(ms).ConfigureAwait(false);
            ms.Position = 0;
            clone.Content = new StreamContent(ms);

            // Copy the content headers
            if (response.Content.Headers != null)
                foreach (var h in response.Content.Headers)
                    clone.Content.Headers.Add(h.Key, h.Value);
        }

        clone.Version = response.Version;

        foreach (KeyValuePair<string, IEnumerable<string>> header in response.Headers)
            clone.Headers.TryAddWithoutValidation(header.Key, header.Value);

        return clone;
    }
}
