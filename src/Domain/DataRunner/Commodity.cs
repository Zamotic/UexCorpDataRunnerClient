﻿using System;
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
}