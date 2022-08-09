using Abstracts.Models.InterfaceService;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Abstracts.Models.Service
{
    public class WorkOrderService : BaseService, IWorkOrderService
    {
        public long? Create(WorkOrders workOrders)
        {
            using (var db = GetContext())
            {
                var entity = new WorkOrders();
                var wo = db.WorkOrders.Where(x => x.RequestId == workOrders.RequestId).OrderByDescending(x => x.WorkOrderId).FirstOrDefault();
                entity.IsSubmit = false;
                db.WorkOrders.Add(entity);
                db.SaveChanges();
                workOrders.WorkOrderId = (int)entity.WorkOrderId;
                return workOrders.WorkOrderId;
            }
        }

        public IEnumerable<Refactored.WorkOrders> Read()
        {
            using (var db = GetContext())
            {
                var result = db.WorkOrders.ToList()
                             .ToList();

                return result;
            }
        }

        public void Update(WorkOrders workOrders)
        {
            using (var db = GetContext())
            {
                var entity = db.WorkOrders.First(s => s.WorkOrderId == workOrders.WorkOrderId);
                entity.RequestId = workOrders.RequestId == null ? entity.RequestId : workOrders.RequestId;
                db.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}