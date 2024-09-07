namespace UexCorpDataRunner.Persistence.Api.UexV2;

public interface IUexCorpWebApiConfiguration
{
    string? WebApiEndPointUrl { get; }
    string? DataRunnerEndpointPath { get; }
    string? ApiKey { get; }
}