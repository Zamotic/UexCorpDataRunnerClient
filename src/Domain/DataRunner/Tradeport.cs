using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UexCorpDataRunner.Domain.DataRunner;
public class Tradeport : NameableBaseModel
{
    public string? System { get; set; }
    public string? Planet { get; set; }
    public string? Satellite { get; set; }
    public string? City { get; set; }
    public string NameShort { get; set; } = string.Empty;
    public bool IsVisible { get; set; }
    public bool IsArmisticeZone { get; set; }
    public bool HasTrade { get; set; }
    public bool WelcomesOutlaws { get; set; }
    public bool HasRefinery { get; set; }
    public bool HasShops { get; set; }
    public bool IsRestrictedArea { get; set; }
    public bool HasMinables { get; set; }

    public List<TradeListing> Prices { get; set; } = new List<TradeListing>();

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
        if (!string.IsNullOrWhiteSpace(Planet))
        {
            stringBuilder.Append($"{Planet}");
        }
        if (!string.IsNullOrWhiteSpace(Satellite))
        {
            if (stringBuilder.Length > 0)
            {
                stringBuilder.Append(" > ");
            }
            stringBuilder.Append($"{Satellite}");
        }
        if (!string.IsNullOrWhiteSpace(City))
        {
            if (stringBuilder.Length > 0)
            {
                stringBuilder.Append(" > ");
            }
            stringBuilder.Append($"{City}");
        }
        if (!string.IsNullOrWhiteSpace(Name))
        {
            if (stringBuilder.Length > 0)
            {
                stringBuilder.Append(" > ");
            }
            stringBuilder.Append($"{Name}");
        }
        if (!string.IsNullOrWhiteSpace(Code))
        {
            if (stringBuilder.Length > 0)
            {
                stringBuilder.Append(" ");
            }
            stringBuilder.Append($"({Code})");
        }
        _FullName = stringBuilder.ToString();
    }

    public override string ToString()
    {
        return FullName!;
    }
}
