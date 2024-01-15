namespace UexCorpDataRunner.Domain.DataRunnerV2;

public class DataSubmitResponse
{
    public bool Response { get; set; }
    public string? Username { get; set; } = string.Empty;
    public DateTimeOffset DateAdded { get; set; }
    public List<string?> ReportIds{ get; set; } = new List<string?>();
}