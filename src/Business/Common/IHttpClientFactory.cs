using System.Net.Http;
using UexCorpDataRunner.Business.Common;

namespace UexCorpDataRunner.Business.Common;

public interface IHttpClientFactory
{
    HttpClient GetHttpClient();
}