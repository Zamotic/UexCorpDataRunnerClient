using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace UexCorpDataRunner.Domain.DataRunnerV2;
public class Commodity : AvailableNameableBaseModel
{
    public int ParentId { get; set; }

    public string? Slug { get; set; }

    public string? Kind { get; set; }

    public decimal BuyPrice { get; set; }

    public decimal SellPrice { get; set; }

    public bool IsRaw { get; set; }

    public bool IsHarvestable { get; set; }

    public bool IsBuyable { get; set; }

    public bool IsSellable { get; set; }

    public bool IsTemporary { get; set; }

    public bool IsIllegal { get; set; }
    public bool IsVisible { get; set; }
}
