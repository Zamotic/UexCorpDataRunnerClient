using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UexCorpDataRunner.Domain.DataRunner;
public class Planet : AvailableNameableBaseModel
{
    public string? System { get; set; } = string.Empty;
}
