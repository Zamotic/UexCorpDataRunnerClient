 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UexCorpDataRunner.Domain.DataRunner;
public class Commodity : NameableBaseModel
{
    public string Kind { get; set; } = string.Empty;
    public decimal BuyPrice { get; set; }
    public decimal SellPrice { get; set; }
    public bool Tradeable { get; set; }
    public bool Buyable { get; set; }
    public bool Sellable { get; set; }
    //public bool Minable { get; set; }
    //public bool Harvestable { get; set; }
    public bool Temporary { get; set; }
    public bool Restricted { get; set; }
    //public bool Raw { get; set; }
    public bool Illegal { get; set; }
    public bool Available { get; set; }
}
