using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Abstracts.Models.Refactored
{
    public class WorkOrders
    {
        public long WorkOrderId { get; set; }
        public long? RequestId { get; set; }
        public string Request { get; set; }
        public string WorkOrderCode { get; set; }
        public DateTime? WorkOrderStartDate { get; set; }
        public string WorkOrderStartDateString { get; set; }
        public DateTime? WorkOrderEndDate { get; set; }
        public string WorkOrderEndDateString { get; set; }
        public string Notes { get; set; }
        public List<int> Departments { get; set; }
        public long? UserIdadd { get; set; }
        public DateTime? DateAdd { get; set; }
        public bool? IsPrinted { get; set; }
        public DateTime? DatePrint { get; set; }
        public string FileName { get; set; }
        public string FileNameApproved { get; set; }
        public long? AutoNo { get; set; }
    }
}