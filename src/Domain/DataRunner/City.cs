using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UexCorpDataRunner.Domain.DataRunner;
public class City : AvailableNameableBaseModel
{
    public string? System { get; set; }
    public string? Planet { get; set; }
    public string? Satellite { get; set; }
}
