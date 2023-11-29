using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace UexCorpDataRunner.Domain.DataRunnerV2;
public class Terminal : AvailableNameableBaseModel
{
    public int StarSystemId { get; set; }

    public string? StarSystemName { get; set; }

    public int PlanetId { get; set; }

    public string? PlanetName { get; set; }

    public int MoonId { get; set; }

    public string? MoonName { get; set; }

    public int SpaceStationId { get; set; }

    public string? SpaceStationName { get; set; }

    public int OutpostId { get; set; }

    public string? OutpostName { get; set; }

    public int CityId { get; set; }

    public string? CityName { get; set; }

    public string? Nickname { get; set; }

    public string? Type { get; set; }

    private string? _FullName;
    public string? FullName 
    { 
        get
        {
            if (string.IsNullOrWhiteSpace(_FullName))
            {
                GenerateFullName();
            }
            return _FullName;
        }
    }

    private void GenerateFullName()
    {
        StringBuilder stringBuilder = new StringBuilder();
        if (!string.IsNullOrWhiteSpace(PlanetName))
        {
            stringBuilder.Append($"{PlanetName}");
        }
        if (!string.IsNullOrWhiteSpace(MoonName))
        {
            if (stringBuilder.Length > 0)
            {
                stringBuilder.Append(" > ");
            }
            stringBuilder.Append($"{MoonName}");
        }
        if (!string.IsNullOrWhiteSpace(SpaceStationName))
        {
            if (stringBuilder.Length > 0)
            {
                stringBuilder.Append(" > ");
            }
            stringBuilder.Append($"{SpaceStationName}");
        }
        if (!string.IsNullOrWhiteSpace(OutpostName))
        {
            if (stringBuilder.Length > 0)
            {
                stringBuilder.Append(" > ");
            }
            stringBuilder.Append($"{OutpostName}");
        }
        if (!string.IsNullOrWhiteSpace(CityName))
        {
            if (stringBuilder.Length > 0)
            {
                stringBuilder.Append(" > ");
            }
            stringBuilder.Append($"{CityName}");
        }
        if (!string.IsNullOrWhiteSpace(Name))
        {
            if (stringBuilder.Length > 0)
            {
                stringBuilder.Append(" > ");
            }
            stringBuilder.Append($"{Name}");
        }
        _FullName = stringBuilder.ToString();
    }

    public override string ToString()
    {
        return FullName!;
    }
}
