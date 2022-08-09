using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Abstracts.Models
{
    public partial class WorkOrderNotes
    {
        public WorkOrderNotes()
        {
            WorkOrderDetails = new HashSet<WorkOrderDetails>();
        }

        public long WorkOrderNoteId { get; set; }
        public string NoteTitle { get; set; }
        public long? RequestId { get; set; }

        public virtual ICollection<WorkOrderDetails> WorkOrderDetails { get; set; }
    }
}
