using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Abstracts.Models;
using Kendo.Mvc.Extensions;
using Abstracts.Models.MasterLoginModel;
using Abstracts.Models.Pbo;

namespace Abstracts.Controllers
{
    public class TechnicalController : Controller
    {
        private  AbstractsDBContext db = new AbstractsDBContext();
        private readonly ParadiseMasterLogInDBContext db2 = new ParadiseMasterLogInDBContext();
        [AuthorizedAction]
        public ActionResult Index()
        {
            long? AutoNo;
            if (db.WorkOrders.Max(x => x.AutoNo) == null) { AutoNo = 0; }
            else { AutoNo = db.WorkOrders.Max(x => x.AutoNo).Value; }

            AutoNo++;

            ViewBag.WorkOrderCode = PublicVariables.WorkOrderPrefixCode + AutoNo;
            if (Session["Request"] != null)
                ViewBag.request = (Requests)Session["Request"];
            if (Session["WorkOrder"] != null)
                ViewBag.workOrder = (WorkOrders)Session["WorkOrder"];

            
            WorkOrderController._WorkOrder = null;
            return View();
        }
        public ActionResult SaveWorkOrderDetailForGrid([DataSourceRequest] DataSourceRequest request, Models.Refactored.WorkOrderDetails workOrderDetails)
        {
            var entity = db.WorkOrderDetails.FirstOrDefault(s => s.WorkOrderDetailId == workOrderDetails.WorkOrderDetailId);

            if (workOrderDetails != null && workOrderDetails.WorkOrderDetailId != 0)
            {
                entity.Notes = workOrderDetails.Notes;
                entity.IsApproved = false;
                db.WorkOrderDetails.Attach(entity);
                db.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
            }


           

            if (entity == null) { return Json(1, JsonRequestBehavior.AllowGet); }
            var result = db.WorkOrderDetails.ToList().Where(x => x.WorkOrderId == WorkOrderController._WorkOrder.WorkOrderId && x.WorkOrderDetailId == workOrderDetails.WorkOrderDetailId)
                           .Select(WorkOrderDetail =>
                           {

                               var workOrders = db.WorkOrders.Where(x => x.WorkOrderId == WorkOrderDetail.WorkOrderId).FirstOrDefault();

                               var requestDetails = db.RequestDetails.Where(x => x.RequestDetailId == WorkOrderDetail.RequestDetailId).FirstOrDefault();

                               var workOrderDetailsTotalQTY = (from b in db.WorkOrders
                                                               join WDetails in db.WorkOrderDetails on b.WorkOrderId equals WDetails.WorkOrderId
                                                               where b.RequestId == workOrders.RequestId && WDetails.RequestDetailId == WorkOrderDetail.RequestDetailId
                                                               select new
                                                               {
                                                                   WDetails.Qty
                                                               }).ToList();

                               var workOrderDetailsPreviousQTY = db.WorkOrderDetails.Where(x =>
                               x.WorkOrderId == WorkOrderDetail.WorkOrderId
                               && x.RequestDetailId == WorkOrderDetail.RequestDetailId).Select(x => x.Qty);

                               var workOrderDetailsNowQty = db.WorkOrderDetails.Where(x => x.WorkOrderDetailId == WorkOrderDetail.WorkOrderDetailId
                               && x.WorkOrderId == WorkOrderDetail.WorkOrderId)
                               .Select(x => x.Qty).FirstOrDefault();
                               long? sumPreviousQty = 0;

                               foreach (var p in workOrderDetailsTotalQTY)
                               {
                                   sumPreviousQty += p.Qty;
                               }
                               var unit = db.Units.Where(x => x.UnitId == requestDetails.UnitId).FirstOrDefault();
                               var Notes = db.WorkOrderNotes.Where(x => x.WorkOrderNoteId == WorkOrderDetail.RequestNoteId).FirstOrDefault();
                               if (Notes == null) Notes = new WorkOrderNotes() { NoteTitle = "", NotePercentage = 0, NoteStatus = "" };
                               return new Models.Refactored.WorkOrderDetails
                               {
                                   RequestDetailCode = requestDetails.RequestDetailCode,
                                   RequestDetailSerial = requestDetails.RequestDetailSerial,
                                   Unit = unit.UnitName,
                                   UnitPrice = requestDetails.UnitPrice,
                                   TotalPrice = requestDetails.TotalPrice,
                                   Qty = requestDetails.Qty,
                                   PreviousQty = sumPreviousQty - workOrderDetailsNowQty,
                                   NowQty = workOrderDetailsNowQty,
                                   NowTotalPrice = sumPreviousQty * requestDetails.UnitPrice,
                                   RequestDetailName = requestDetails.RequestDetailName,
                                   TotalQty = sumPreviousQty,
                                   InventoryBookNo = WorkOrderDetail.InventoryBookNo,
                                   InventoryBookPage = WorkOrderDetail.InventoryBookPage,
                                   NotePercentagePrice = (Notes.NotePercentage * (sumPreviousQty * requestDetails.UnitPrice)) / 100,
                                   NeededPrice = (sumPreviousQty * requestDetails.UnitPrice) - ((Notes.NotePercentage * (sumPreviousQty * requestDetails.UnitPrice)) / 100),
                                   RequestDetailId = WorkOrderDetail.RequestDetailId,
                                   WorkOrderNote = new Models.Refactored.WorkOrderNotes() { WorkOrderNoteId = Notes.WorkOrderNoteId, NoteTitle = Notes.NoteTitle, NotePercentage = Notes.NotePercentage },
                                   RequestNoteString = Notes.NoteTitle,
                                   RequestNoteId = WorkOrderDetail.RequestNoteId,
                                   WorkOrderDetailId = WorkOrderDetail.WorkOrderDetailId,
                                   Notes = workOrderDetails.Notes
                               };

                           }).ToList();
            return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
    }
}