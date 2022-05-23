using System.Net.Http;
using UexCorpDataRunner.Business.Common;

namespace UexCorpDataRunner.DesktopClient.WebClient;

public interface IHttpClientFactory
{
    HttpClient GetHttpClient(ILogger? logger = null);
}