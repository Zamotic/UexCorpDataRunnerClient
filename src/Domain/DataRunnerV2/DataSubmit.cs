using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UexCorpDataRunner.Domain.DataRunnerV2;
public class DataSubmit
{
    public int TerminalId { get; set; }

    public string Type { get; set; } = "commodity";

    public bool IsProduction { get; set; }

    public List<DataSubmitPrice> DataSubmitPrices { get; set; } = new List<DataSubmitPrice>();

    public int FactionAffinity { get; set; }

    public string? Details { get; set; }

    public string? GameVersion { get; set; }
}
