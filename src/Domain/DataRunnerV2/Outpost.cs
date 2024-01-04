namespace UexCorpDataRunner.Domain.DataRunnerV2;
public class Outpost : LocationBaseModel
{
    public int PlanetId { get; set; }

    public int MoonId { get; set; }

    public int Nickname { get; set; }
}
