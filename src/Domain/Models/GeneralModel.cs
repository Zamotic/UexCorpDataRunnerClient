using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UexCorpDataRunner.Domain.Models;
public class GeneralModel : BaseModel
{ 
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
}
