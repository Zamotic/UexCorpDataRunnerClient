using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UexCorpDataRunner.Domain.DataRunner;
public class Satellite : AvailableNameableBaseModel
{
    public string? System { get; set; }
    public string? Planet { get; set; }
}
