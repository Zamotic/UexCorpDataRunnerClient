using EnumsNET;
using System.ComponentModel;

namespace UexCorpDataRunner.Domain.DataRunnerV2;
public enum TerminalType
{
    [TerminalTypeValue("commodity")]
    Commodity,
    [TerminalTypeValue("item")]
    Item,
    [TerminalTypeValue("vehicle_sell")]
    VehicleSell,
    [TerminalTypeValue("vehicle_rent")]
    VehicleRent,
    [TerminalTypeValue("commodity_raw")]
    CommodityRaw,
    [TerminalTypeValue("refinery")]
    Refinery,
}

[AttributeUsage(AttributeTargets.Field)]
public class TerminalTypeValueAttribute : Attribute
{
    public string TypeValue { get; }
    public TerminalTypeValueAttribute(string typeValue)
    {
        TypeValue = typeValue;
    }
}
