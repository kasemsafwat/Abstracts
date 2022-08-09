using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Abstracts.Models.Refactored
{
    public class UserRequests
    {
        public long UserRequestId { get; set; }
        public long? UserId { get; set; }
        public long? RequestId { get; set; }
        public string RequestNo { get; set; }
        public string RequestName { get; set; }
        public string RequestCode { get; set; }
        public DateTime? RequestDate { get; set; }
        public string RequestDateString { get; set; }
        public decimal? RequestAmount { get; set; }
        public DateTime? RequestStartDate { get; set; }
        public string RequestStartDateString { get; set; }
        public long? RequestDuration { get; set; }
        public DateTime? RequestEndDate { get; set; }
        public string RequestEndDateString { get; set; }
        public string Notes { get; set; }
        public long? CompanyId { get; set; }
        public string Company { get; set; }
        public long? UserIdadd { get; set; }
        public DateTime? DateAdd { get; set; }
        public bool? IsLocked { get; set; }
        public string Locked { get; set; }
        public long? UserIdlock { get; set; }
        public DateTime? DateLock { get; set; }
        public bool? IsPrinted { get; set; }
        public DateTime? DatePrint { get; set; }
        public long? AutoNo { get; set; }
        public string FileName { get; set; }
        public string FileNameDetails { get; set; }
        public string FileUrl { get; set; }
    }
}