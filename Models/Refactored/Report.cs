using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Abstracts.Models.Refactored
{
    public class Report
    {
        public long? ReportId { get; set; }
        public string RequestName { get; set; }
        public string RequestCode { get; set; }
        public string RequestDate { get; set; }
        public string RequestStartDate { get; set; }
        public long? RequestDuration { get; set; }
        public string RequestEndDate { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyName { get; set; }
        public string RequestDetailSerial { get; set; }
        public string RequestDetailCode { get; set; }
        public string RequestDetailName { get; set; }
        public long? TotalQty { get; set; }
        public decimal? UnitPrice { get; set; }
        public string WorkOrderCode { get; set; }
        public string WorkOrderStartDate { get; set; }
        public string WorkOrderEndDate { get; set; }
        public long? TotalIssuedQty { get; set; }
        public string ItemClassName { get; set; }
        public string UnitName { get; set; }
        public string InventoryBookNo { get; set; }
        public string InventoryBookPage { get; set; }
        public string NoteTitle { get; set; }
        public long? NotePercentage { get; set; }
        public string NoteStatus { get; set; }
        public bool? IsApproved { get; set; }
        public string DateApproved { get; set; }


    }
}