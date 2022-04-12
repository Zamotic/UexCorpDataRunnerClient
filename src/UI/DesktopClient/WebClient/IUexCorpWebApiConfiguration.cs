namespace UexCorpDataRunner.DesktopClient.WebClient;

public interface IUexCorpWebApiConfiguration
{
    string? ApiKey { get; }
    string? DataRunnerEndpointPath { get; }
    string? WebApiEndPointUrl { get; }
}