using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UexCorpDataRunner.Domain.Models;
public class City : AvailableGeneralModel
{
    public System System { get; set; }
    public Planet Planet { get; set; }
    public Satellite Satellite { get; set; }
}
