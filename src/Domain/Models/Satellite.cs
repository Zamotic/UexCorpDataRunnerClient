using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UexCorpDataRunner.Domain.Models;
public class Satellite : AvailableGeneralModel
{
    public System? System { get; set; }
    public Planet? Planet { get; set; }
}
