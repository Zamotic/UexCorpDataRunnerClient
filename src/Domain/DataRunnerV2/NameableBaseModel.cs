namespace UexCorpDataRunner.Domain.DataRunnerV2;
public class NameableBaseModel : BaseModel
{
    public string? Name { get; set; } = string.Empty;
    public string? Code { get; set; } = string.Empty;
}
