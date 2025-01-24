using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UexCorpDataRunner.Domain.DataRunnerV2;
public class DataParameters
{
    public DataParametersGlobal Global { get; set; } = new();

    public class DataParametersGlobal
    {
        public bool IsAcceptingReports { get; set; }
        public string? GameVersion { get; set; }
        public string? GameVersionPtu { get; set; }
        public int EvaluationPeriodDays { get; set; }
    }
}
