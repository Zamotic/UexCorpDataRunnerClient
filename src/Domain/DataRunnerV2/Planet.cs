namespace UexCorpDataRunner.Domain.DataRunnerV2;
public class Planet : AvailableNameableBaseModel
{
    public string? NameOrigin { get; set; }
    public bool IsLagrange { get; set; }
}
