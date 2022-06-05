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

    public Dictionary<string, TradeListing> Prices { get; set; } = new Dictionary<string, TradeListing>();
}
