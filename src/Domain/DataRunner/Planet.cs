using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UexCorpDataRunner.Domain.DataRunner;
public class Planet : AvailableNameableBaseModel
{
    public System? System { get; set; }
}
