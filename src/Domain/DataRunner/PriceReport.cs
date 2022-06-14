﻿namespace UexCorpDataRunner.Domain.DataRunner;

public class PriceReport
{
    public string CommodityCode { get; set; } = string.Empty;
    public string TradeportCode { get; set; } = string.Empty;
    public string Operation { get; set; } = string.Empty;
    public string Price { get; set; } = string.Empty;
    public string UserHash { get; set; } = string.Empty;
    public bool Confirm { get; set; }
}