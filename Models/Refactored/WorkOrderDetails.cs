using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Abstracts.Models.Refactored
{
    public class WorkOrderDetails
    {
        public long? RequestId { get; set; }
        public string RequestDetailCode { get; set; }
        public string RequestDetailSerial { get; set; }
        public string RequestDetailName { get; set; }
        public string Unit { get; set; }
        public string Notes { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? TotalPrice { get; set; }
        public long? Qty { get; set; }
        public long? PreviousQty { get; set; }
        public string InventoryBookNo { get; set; }
        public string InventoryBookPage { get; set; }
        public long? NowQty { get; set; }
        public long? TotalQty { get; set; }
        public decimal? NowTotalPrice { get; set; }
        public decimal? NotePercentagePrice { get; set; }
        [UIHint("ClientNote")]
        public virtual WorkOrderNotes WorkOrderNote { get; set; }
        public decimal? NeededPrice { get; set; }

        public long WorkOrderDetailId { get; set; }
        public long WorkOrderId { get; set; }
        public long DepartmentId { get; set; }
        public string DepartmentName { get; set; }

        public long? RequestDetailId { get; set; }
        public List<long?> RequestDetailIdList { get; set; }
       
        public long? RequestNoteId { get; set; }
        public string WorkOrderStatus { get; set; }
        public string RequestNoteString { get; set; }

        //public virtual RequestDetails RequestDetail { get; set; }
       

        //public virtual WorkOrders WorkOrder { get; set; }
    }
}