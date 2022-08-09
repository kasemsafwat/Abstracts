using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Abstracts.Models
{
    public partial class RequestDetails
    {
        public RequestDetails()
        {
            WorkOrderDetails = new HashSet<WorkOrderDetails>();
        }

        public long RequestDetailId { get; set; }
        public long? RequestId { get; set; }
        public string RequestDetailSerial { get; set; }
        public string RequestDetailCode { get; set; }
        public string RequestDetailName { get; set; }
        public long? UnitId { get; set; }
        public long? Qty { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? TotalPrice { get; set; }
        public long? DepartmentId { get; set; }
        public bool? IsStored { get; set; }
        public long? UserIdadd { get; set; }
        public DateTime? DateAdd { get; set; }
        public long? AutoNo { get; set; }

        public virtual Requests Request { get; set; }
        public virtual Units Unit { get; set; }
        public virtual ICollection<WorkOrderDetails> WorkOrderDetails { get; set; }
    }
}
