namespace UexCorpDataRunner.Persistence.Api.Uex;

public interface IUexCorpWebApiConfiguration
{
    string? WebApiEndPointUrl { get; }
    string? DataRunnerEndpointPath { get; }
    string? ApiKey { get; }
}