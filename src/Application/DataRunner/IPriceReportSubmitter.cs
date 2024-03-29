﻿using System.Collections.Concurrent;

namespace UexCorpDataRunner.Application.DataRunner;
public interface IPriceReportSubmitter
{
    Task<Dictionary<string, bool>> SubmitReports(IEnumerable<CommodityWrapper> commodities, string tradeportCode, ConcurrentQueue<string> statusBufferQueue);
    Task<Dictionary<string, bool>> SubmitAllReports(IEnumerable<CommodityWrapper> commodities, string tradeportCode, ConcurrentQueue<string> statusBufferQueue);
}