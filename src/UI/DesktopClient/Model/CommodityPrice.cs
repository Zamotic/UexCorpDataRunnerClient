using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UexCorpDataRunner.DesktopClient.Model;
public class CommodityPrice
{
    public string? Name { get; set; } = "Uninitialized Commodity";
    public decimal NewPrice { get; set; } = 0m;
    public int AvailableSupply { get; set; } = 0;
    public decimal OldPrice { get; protected internal set; } = 0m;
    public decimal MinPrice { get; protected internal set; } = 0m;
    public decimal MaxPrice { get; protected internal set; } = 0m;
    public decimal BestPrice { get; protected internal set; } = 0m;
    public string BestLocation { get; protected internal set; } = "Unknown";
}
