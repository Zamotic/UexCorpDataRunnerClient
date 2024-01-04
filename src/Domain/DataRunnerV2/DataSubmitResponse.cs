namespace UexCorpDataRunner.Domain.DataRunnerV2;

public class DataSubmitResponse
{
    public bool Response { get; set; }
    public string? StatusMessage { get; set; } = string.Empty;
    //public List<string> ReturnedDataList { get; set; } = new List<string>();
}