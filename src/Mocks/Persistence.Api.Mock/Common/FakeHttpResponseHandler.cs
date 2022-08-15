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

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
    {
        if(request.RequestUri== null)
        {
            throw new ArgumentNullException(nameof(request.RequestUri));
        }

        if (_FakeResponses.ContainsKey(request.RequestUri))
        {
            return Task.FromResult(_FakeResponses[request.RequestUri]);
        }
        else
        {
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.NotFound) { RequestMessage = request });
        }
    }
}
