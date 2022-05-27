namespace UexCorpDataRunner.Persistence.Api.Uex;

public interface IUexCorpWebApiConfiguration
{
    string? DataRunnerEndpointPath { get; }
    string? WebApiEndPointUrl { get; }
}