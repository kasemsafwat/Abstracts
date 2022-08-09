using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Abstracts.Models
{
    public partial class RequestLog
    {
        public long RequestLogId { get; set; }
        public long? RequestId { get; set; }
        public long? RequestDetailId { get; set; }
        public int? Type { get; set; }
        public long? NewRequestDuration { get; set; }
        public long? RequestDuration { get; set; }
        public long? NewQty { get; set; }
        public long? Qty { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? NewUnitPrice { get; set; }
        public long? UserIdAdd { get; set; }
        public DateTime? DateAdd { get; set; }
        public long? UserUpdate { get; set; }
        public DateTime? DateUpdate { get; set; }
        public string LogFileName { get; set; }
    }
}
