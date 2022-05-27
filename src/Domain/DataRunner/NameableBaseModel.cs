using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UexCorpDataRunner.Domain.DataRunner;
public class NameableBaseModel : BaseModel
{
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
}
