namespace UexCorpDataRunner.Domain.DataRunnerV2;
public class BaseModel
{
    public int Id { get; set; }
    public DateTimeOffset DateAdded { get; set; }
    public DateTimeOffset DateModified { get; set; }
}
