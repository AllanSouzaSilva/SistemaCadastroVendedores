using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Models.Enums
{
    public enum SalesStatus : int
    {
        Pendding = 0,
        Billed  = 1,
        Cancelled = 2
    }
}
