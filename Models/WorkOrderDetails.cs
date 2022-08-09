using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Abstracts.Models
{
    public partial class WorkOrderDetails
    {
        public long WorkOrderDetailId { get; set; }
        public long? WorkOrderId { get; set; }
        public long? RequestDetailId { get; set; }
        public long? Qty { get; set; }
        public string InventoryBookNo { get; set; }
        public string InventoryBookPage { get; set; }
        public long? RequestNoteId { get; set; }
        public bool? IsApproved { get; set; }
        public DateTime? DateApproved { get; set; }
        public long? UserApproved { get; set; }
        public string Engineer { get; set; }
        public string GeneralDirector { get; set; }
        public string GeneralManager { get; set; }
        public string Technical { get; set; }
        public string VicePresident { get; set; }
        public string President { get; set; }
        public bool? IsStored { get; set; }
        public string Notes { get; set; }

        public virtual RequestDetails RequestDetail { get; set; }
        public virtual WorkOrderNotes RequestNote { get; set; }
        public virtual WorkOrders WorkOrder { get; set; }
    }
}
