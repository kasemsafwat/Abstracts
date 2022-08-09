using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Abstracts.Models.Refactored
{
    public class RequestLog
    {
        public long RequestLogId { get; set; }
        public long? RequestId { get; set; }
        public string RequestCode { get; set; }
        public long RequestDetailId { get; set; }
        public string RequestDetailCode { get; set; }
        public int? Type { get; set; }
        public string TypeName { get; set; }
        public long? NewRequestDuration { get; set; }
        public long? RequestDuration { get; set; }
        public long? NewQty { get; set; }
        public long? Qty { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? NewUnitPrice { get; set; }
        public string LogFileName { get; set; }
        public string UserAddName { get; set; }
        public string DateAdd { get; set; }
    }
}