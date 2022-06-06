using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UexCorpDataRunner.Domain.DataRunner;
public class TradeListing
{
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? Kind { get; set; }
    public OperationType Operation { get; set; }
    public decimal PriceBuy { get; set; }
    public decimal PriceSell { get; set; }
    public DateTimeOffset DateUpdate { get; set; }
    public bool IsUpdated { get; set; }
}
