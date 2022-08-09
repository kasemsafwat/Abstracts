using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracts.Models.InterfaceService
{
    public interface IWorkOrderService
    {
        IEnumerable<Refactored.WorkOrders> Read();
        long? Create(WorkOrders workOrders);
        void Update(WorkOrders workOrders);
    }
}
