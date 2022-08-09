using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Abstracts.Models.Refactored
{
    public class WorkOrderNotes
    {
        public long WorkOrderNoteId { get; set; }
        public string NoteTitle { get; set; }
        public long? NotePercentage { get; set; }
        public string NoteStatus { get; set; }
        public string NoteStatusName { get; set; }
        public long? RequestId { get; set; }
        public string Request { get; set; }

    }
}