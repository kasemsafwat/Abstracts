using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracts.Models.InterfaceService
{
   public interface IWorkOrderDetailsService
    {
        IEnumerable<Refactored.WorkOrderDetails> Read(long? workOrderID);
        IEnumerable<Refactored.WorkOrderDetails> Read1(long? workOrderID);
        IEnumerable<Refactored.WorkOrderDetails> ReadFullApproved(long? workOrderID);
        IEnumerable<Refactored.WorkOrderDetails> ReadWithDetails(long? workOrderID, long? workOrderDetailsID);

        int Create(WorkOrderDetails workOrderDetails);
        void Update(WorkOrderDetails workOrderDetails);
    }
}
