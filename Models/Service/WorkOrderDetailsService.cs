using Abstracts.Controllers;
using Abstracts.Models.InterfaceService;
using Abstracts.Models.MasterLoginModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Abstracts.Models.Service
{
    public class WorkOrderDetailsService : BaseService, IWorkOrderDetailsService
    {
        private readonly ParadiseMasterLogInDBContext db2 = new ParadiseMasterLogInDBContext();
        public int Create(WorkOrderDetails workOrderDetails)
        {
            using (var db = GetContext())
            {

                db.WorkOrderDetails.Add(entity);
                db.SaveChanges();
                workOrderDetails.WorkOrderDetailId = (int)entity.WorkOrderDetailId;
            }
            return 1;
        }

        public IEnumerable<Refactored.WorkOrderDetails> Read(long? workOrderID)
        {
            using (var db = GetContext())
            {
                var workorders = db.WorkOrders.ToList();
                var requests = db.Requests.ToList();
                var requestDetails = db.RequestDetails.ToList();
                var workorderDetails = db.WorkOrderDetails.ToList();
                var workordernotes = db.WorkOrderNotes.ToList();
                var units = db.Units.ToList();
                long userID = HomeController._UserID;
                var result = new List<Refactored.WorkOrderDetails>();
                var Employee = db2.Users.Where(x => x.UserId == userID).FirstOrDefault();
                

                return result;
            }
        }
        public IEnumerable<Refactored.WorkOrderDetails> Read1(long? workOrderID)
        {
            using (var db = GetContext())
            {
                var workorders = db.WorkOrders.ToList();
                var requestDetails = db.RequestDetails.ToList();
                var workorderDetails = db.WorkOrderDetails.ToList();
                var workordernotes = db.WorkOrderNotes.ToList();
                var units = db.Units.ToList();

                return workorders;
            }
        }
        public IEnumerable<Refactored.WorkOrderDetails> ReadFullApproved(long? workOrderID)
        {
            using (var db = GetContext())
            {
                var workorders = db.WorkOrders.ToList();
                var requests = db.Requests.ToList();
                var requestDetails = db.RequestDetails.ToList();
                var workorderDetails = db.WorkOrderDetails.ToList();
                var workordernotes = db.WorkOrderNotes.ToList();
                var units = db.Units.ToList();

                return workorders;
            }
        }

        public IEnumerable<Refactored.WorkOrderDetails> ReadWithDetails(long? workOrderID, long? workOrderDetailsID)
        {
            using (var db = GetContext())
            {
                var workorders = db.WorkOrders.ToList();
                var requests = db.Requests.ToList();
                var requestDetails = db.RequestDetails.ToList();
                var workorderDetails = db.WorkOrderDetails.ToList();
                var workordernotes = db.WorkOrderNotes.ToList();
                var units = db.Units.ToList();

                return workorders;
            }
        }

        public void Update(WorkOrderDetails workOrderDetails)
        {
            using (var db = GetContext())
            {
                var entity = db.WorkOrderDetails.First(s => s.WorkOrderDetailId == workOrderDetails.WorkOrderDetailId);
                entity.WorkOrderId = workOrderDetails.WorkOrderId == null ? entity.WorkOrderId : workOrderDetails.WorkOrderId;
                db.WorkOrderDetails.Attach(entity);
                db.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}