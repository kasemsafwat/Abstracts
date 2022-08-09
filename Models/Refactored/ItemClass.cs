using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Abstracts.Models.Refactored
{
    public class ItemClass
    {
        public long ItemClassId { get; set; }
        public string ItemClassName { get; set; }
        public string Locked { get; set; }
        public bool? IsLocked { get; set; }
        public long? UserIdlock { get; set; }
        public DateTime? DateLock { get; set; }
    }
}