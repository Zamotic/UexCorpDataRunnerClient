using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UexCorpDataRunner.Domain.Models;
public class Tradeport : AvailableGeneralModel
{
    public System System { get; set; }
    public Planet Planet { get; set; }
    public Satellite Satellite { get; set; }
    public City City { get; set; }
    public string NameShort { get; set; } = string.Empty;
    public bool IsVisible { get; set; }
    public bool IsArmisticeZone { get; set; }
    public bool HasTrade { get; set; }
    public bool WelcomesOutlaws { get; set; }
    public bool HasRefinery { get; set; }
    public bool HasShops { get; set; }
    public bool IsRestrictedArea { get; set; }
    public bool HasMinables { get; set; }

    public IList<Commodity> Prices { get; set; } = new List<Commodity>();
}
