using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Abstracts.Models
{
    public partial class Requests
    {
        public Requests()
        {
            RequestDetails = new HashSet<RequestDetails>();
            WorkOrders = new HashSet<WorkOrders>();
        }

        public long RequestId { get; set; }
        public string RequestName { get; set; }
        public string RequestCode { get; set; }
        public DateTime? RequestDate { get; set; }
        public decimal? RequestAmount { get; set; }
        public DateTime? RequestStartDate { get; set; }
        public long? RequestDuration { get; set; }
        public DateTime? RequestEndDate { get; set; }
        public string Notes { get; set; }
        public long? CompanyId { get; set; }
        public long? UserIdadd { get; set; }
        public DateTime? DateAdd { get; set; }
        public bool? IsLocked { get; set; }
        public long? UserIdlock { get; set; }
        public DateTime? DateLock { get; set; }
        public bool? IsPrinted { get; set; }
        public DateTime? DatePrint { get; set; }
        public long? AutoNo { get; set; }
        public string RequestFileName { get; set; }
        public string RequestDetailsFileName { get; set; }
        public string RequestNo { get; set; }

        public virtual Companies Company { get; set; }
        public virtual ICollection<RequestDetails> RequestDetails { get; set; }
        public virtual ICollection<WorkOrders> WorkOrders { get; set; }
    }
}
