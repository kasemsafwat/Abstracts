using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracts.Models.InterfaceService
{
    public interface IWorkOrderNoteService
    {
        IEnumerable<Refactored.WorkOrderNotes> Read(long RequestID);
        IEnumerable<WorkOrderNotes> ReadByID(long workOrderNoteID);
        void Create(WorkOrderNotes workOrderNotes);
        void Update(WorkOrderNotes workOrderNotes);
    }
}
