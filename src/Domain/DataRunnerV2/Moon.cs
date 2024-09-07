namespace UexCorpDataRunner.Domain.DataRunnerV2;
public class Moon : AvailableNameableBaseModel
{
    public int StarSystemId { get; set; }

    public int PlanetId { get; set; }

    public string? NameOrigin { get; set; }
}
