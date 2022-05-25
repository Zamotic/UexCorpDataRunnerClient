namespace UexCorpDataRunner.Domain.Configurations;

public interface IUexCorpWebApiConfiguration
{
    string? DataRunnerEndpointPath { get; }
    string? WebApiEndPointUrl { get; }
}