using System.Net.Http;
using UexCorpDataRunner.DesktopClient.Core;

namespace UexCorpDataRunner.DesktopClient.WebClient;

public interface IHttpClientFactory
{
    HttpClient GetHttpClient(ILogger? logger = null);
}