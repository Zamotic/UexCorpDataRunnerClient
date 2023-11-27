namespace UexCorpDataRunner.Persistence.Api.UexV2;
public class UexCorpWebApiConfiguration : IUexCorpWebApiConfiguration
{
    public const string ConfigurationSectionName = "UEXWebApiConfigV2";

    public string? WebApiEndPointUrl { get; set; }
    public string? DataRunnerEndpointPath { get; set; }
    public string? ApiKey { get; set; }
}