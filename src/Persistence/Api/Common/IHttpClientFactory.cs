namespace UexCorpDataRunner.Persistence.Api.Common;

public interface IHttpClientFactory
{
    HttpClient GetHttpClient();
}