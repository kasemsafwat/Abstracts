using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Abstracts.Models.Refactored
{
    public class ConsultationWorkOrder
    {
        public long ConsultationWorkOrderId { get; set; }
        public long? RequestId { get; set; }
        public long? WorkOrderNoFrom { get; set; }
        public long? WorkOrderNoTo { get; set; }
        public bool IsHaveFinal { get; set; }

        public long? ConsultationUserId { get; set; }
        public string ConsultationUserName { get; set; }
        public long? UserAdd { get; set; }
        public DateTime? DateAdded { get; set; }
        public long? UserUpdate { get; set; }
        public DateTime? DateUpdated { get; set; }
    }
}