using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Abstracts.Models
{
    public partial class VwWorkOrders
    {
        public long WorkOrderId { get; set; }
        public string TaxRecordNo { get; set; }
        public string InsuranceNo { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string WorkOrderCode { get; set; }
        public DateTime? WorkOrderStartDate { get; set; }
        public DateTime WorkOrderEndDate { get; set; }
        public string Notes { get; set; }
        public bool? IsPrinted { get; set; }
        public bool? IsSubmit { get; set; }
        public DateTime DatePrint { get; set; }
        public DateTime DateSubmit { get; set; }
        public string RequestDetailSerial { get; set; }
        public string RequestDetailCode { get; set; }
        public string RequestDetailName { get; set; }
        public string UnitName { get; set; }
        public long? Qty { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? TotalPrice { get; set; }
        public long? QtyIssued { get; set; }
        public long? TotalQtyIssued { get; set; }
        public string InventoryBookNo { get; set; }
        public string InventoryBookPage { get; set; }
        public string NoteTitle { get; set; }
        public long NotePercentage { get; set; }
        public string NoteStatus { get; set; }
        public bool? IsApproved { get; set; }
        public DateTime DateApproved { get; set; }
        public string Engineer { get; set; }
        public string GeneralDirector { get; set; }
        public string GeneralManager { get; set; }
        public string Technical { get; set; }
        public string VicePresident { get; set; }
        public string President { get; set; }
        public long? DepartmentId { get; set; }
        public string RequestNote { get; set; }
        public long WorkOrderNoteId { get; set; }
        public decimal NewQuantity { get; set; }
        public DateTime? NewDate { get; set; }
        public string UserName { get; set; }
        public string ConsultationName { get; set; }
    }
}
