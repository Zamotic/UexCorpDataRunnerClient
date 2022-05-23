using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UexCorpDataRunner.Domain.Models;
public class BaseModel
{
    public DateTime DateAdded { get; set; }
    public DateTime DateModified { get; set; }
}
