using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Abstracts.Models
{
    public partial class VwRequests
    {
        public long RequestId { get; set; }
        public long RequestDetailId { get; set; }
        public string RequestNo { get; set; }
        public string RequestName { get; set; }
        public string RequestCode { get; set; }
        public DateTime? RequestDate { get; set; }
        public decimal? RequestAmount { get; set; }
        public DateTime? RequestStartDate { get; set; }
        public long? RequestDuration { get; set; }
        public DateTime? RequestEndDate { get; set; }
        public string Notes { get; set; }
        public bool? IsPrinted { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyName { get; set; }
        public string TaxRecordNo { get; set; }
        public string InsuranceNo { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string RequestDetailSerial { get; set; }
        public string RequestDetailCode { get; set; }
        public string RequestDetailName { get; set; }
        public string UnitName { get; set; }
        public long? Qty { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? TotalPrice { get; set; }
    }
}
