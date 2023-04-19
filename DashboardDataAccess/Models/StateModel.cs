using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzDataAccess.Models;

public class StateModel
{
    public int? id { get; set; }
    public string state { get; set; }
    public string? state_abbrev { get; set; }
}
