using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Abstracts.Models.Refactored
{
    public class RequestDetails
    {
        public long RequestDetailId { get; set; }
        public long? RequestId { get; set; }
        public string Request { get; set; }
        public long? ItemClassId { get; set; }
        public string ItemClass { get; set; }
        
        public string FileNameDetails { get; set; }
        public string RequestDetailSerial { get; set; }
        public string RequestDetailCode { get; set; }
        public string RequestDetailName { get; set; }
        public long? UnitId { get; set; }
        public string Unit { get; set; }
        public long? Qty { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? TotalPrice { get; set; }
        public long? DepartmentId { get; set; }
        public string Department { get; set; }
        public long? UserIdadd { get; set; }
        public DateTime? DateAdd { get; set; }
        public long? AutoNo { get; set; }
    }
}