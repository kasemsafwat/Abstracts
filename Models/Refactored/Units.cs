using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Abstracts.Models.Refactored
{
    public class Units
    {
        public long UnitId { get; set; }
        public string UnitName { get; set; }
        public bool? IsLocked { get; set; }
        public string Locked { set; get; }
    }
}