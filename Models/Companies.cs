using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Abstracts.Models
{
    public partial class Companies
    {
        public Companies()
        {
            Requests = new HashSet<Requests>();
        }

        public long CompanyId { get; set; }
        public string Email { get; set; }
        public long? UserIdadd { get; set; }
        public DateTime? DateAdd { get; set; }
        public long? UserIdupdate { get; set; }
        public DateTime? DateUpdate { get; set; }
        public bool? IsLocked { get; set; }
        public long? UserIdlock { get; set; }
        public DateTime? DateLock { get; set; }
        public long? AutoNo { get; set; }

        public virtual ICollection<Requests> Requests { get; set; }
    }
}
