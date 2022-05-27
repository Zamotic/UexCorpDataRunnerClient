namespace UexCorpDataRunner.Persistence.Api.Uex;
public class UexCorpWebApiConfiguration : IUexCorpWebApiConfiguration
{
    public const string ConfigurationSectionName = "UEXWebApiConfig";

    public string? WebApiEndPointUrl { get; set; }
    public string? DataRunnerEndpointPath { get; set; }
}