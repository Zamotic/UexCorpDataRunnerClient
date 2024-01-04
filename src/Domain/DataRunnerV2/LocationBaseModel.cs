namespace UexCorpDataRunner.Domain.DataRunnerV2;
public class LocationBaseModel : AvailableNameableBaseModel
{
    public int StarSystemId { get; set; }

    public int FactionId { get; set; }

    public string? Nickname { get; set; }

    //public bool IsMonitored { get; set; }

    //public bool IsArmistice { get; set; }

    //public bool IsLandable { get; set; }

    //public bool IsDecomissioned { get; set; }

    //public bool HasQuantumMarker { get; set; }

    public bool HasTradeTerminal { get; set; }

    //public bool HasHabitation { get; set; }

    //public bool HasRefinery { get; set; }

    //public bool HasCargoCenter { get; set; }

    //public bool HasClinic { get; set; }

    //public bool HasFood { get; set; }

    public bool HasShops { get; set; }

    //public bool HasRefuel { get; set; }

    //public bool HasRepair { get; set; }

    //public bool HasGravity { get; set; }
}
