using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Abstracts.Models
{
    public partial class ConsultationWorkOrder
    {
        public long ConsultationWorkOrderId { get; set; }
        public DateTime? DateAdded { get; set; }
        public long? UserUpdate { get; set; }
        public DateTime? DateUpdated { get; set; }
    }
}
