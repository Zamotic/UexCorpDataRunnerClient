using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UexCorpDataRunner.Domain.DataRunnerV2;
public class DataSubmitPrice
{
    public int CommodityId { get; set; }

    public float SellPrice { get; set; }

    public int SellScu { get; set; }

    public int SellStatus { get; set; }

    public float BuyPrice { get; set; }

    public int BuyScu { get; set; }

    public int BuyStatus { get; set; }
}
