using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Abstracts.Models
{
    public partial class Units
    {
        public Units()
        {
            RequestDetails = new HashSet<RequestDetails>();
        }

        public long UnitId { get; set; }
        public string UnitName { get; set; }
        public bool? IsLocked { get; set; }
        public long? UserIdlock { get; set; }
        public DateTime? DateLock { get; set; }

        public virtual ICollection<RequestDetails> RequestDetails { get; set; }
    }
}
