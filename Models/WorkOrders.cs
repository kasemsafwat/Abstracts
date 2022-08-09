using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Abstracts.Models
{
    public partial class WorkOrders
    {
        public WorkOrders()
        {
            WorkOrderDetails = new HashSet<WorkOrderDetails>();
        }

        public long WorkOrderId { get; set; }
        public long? RequestId { get; set; }
        public string FileName { get; set; }
        public string FileNameApproved { get; set; }
        public long? AutoNo { get; set; }
        public long? ConsultationUserId { get; set; }
        public bool? ConsultationSubmit { get; set; }
        public DateTime? ConsultationDateSubmit { get; set; }
        public string ConsultationFileNameApproved { get; set; }
        public long? TechnicalUserId { get; set; }
        public bool? TechnicalSubmit { get; set; }
        public DateTime? TechnicalDateSubmit { get; set; }
        public bool? IsSubmit { get; set; }
        public DateTime? DateSubmit { get; set; }
        public long? UserIdsubmit { get; set; }
        public DateTime? DateAddedCompanyUserId { get; set; }
        public long? CompanyUserIdadd { get; set; }

        public virtual Requests Request { get; set; }
        public virtual ICollection<WorkOrderDetails> WorkOrderDetails { get; set; }
    }
}
