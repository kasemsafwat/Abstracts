using Abstracts.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Abstracts.Controllers
{
    public class ConsultationController : Controller
    {
        readonly AbstractsDBContext db = new AbstractsDBContext();
        [AuthorizedAction]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SaveConsultationUsers(ConsultationUser consultationUser)
        {
            if (consultationUser != null && consultationUser.ConsultationUserId == 0 && ModelState.IsValid)
            {
                var entity = new ConsultationUser();
                entity.ConsultationName = consultationUser.ConsultationName;
                entity.Password = consultationUser.Password;
                entity.Email = consultationUser.Email;
                entity.PhoneNumber = consultationUser.PhoneNumber;
                entity.UserAdd = (long)Session["UserID"];
                entity.DateAdded = DateTime.Now;
                db.ConsultationUser.Add(entity);
                db.SaveChanges();
                consultationUser.ConsultationUserId = (int)entity.ConsultationUserId;

            }
            else
            {
                var entity = db.ConsultationUser.First(s => s.ConsultationUserId == consultationUser.ConsultationUserId);
                entity.ConsultationName = consultationUser.ConsultationName;
                entity.Password = consultationUser.Password;
                entity.Email = consultationUser.Email;
                entity.PhoneNumber = consultationUser.PhoneNumber;
                entity.UserUpdate = (long)Session["UserID"];
                entity.DateUpdated = DateTime.Now;
                db.ConsultationUser.Attach(entity);
                db.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();

            }

            return Json(1, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetConsultationUsersList([DataSourceRequest] DataSourceRequest request)
        {
            var lst = db.ConsultationUser.ToList()
                             .Select(cp =>
                             {
                                 return new Models.Refactored.ConsultationUser
                                 {
                                     ConsultationUserId = cp.ConsultationUserId,
                                     ConsultationName = cp.ConsultationName??"",
                                     Password = cp.Password??"",
                                     Email = cp.Email??"",
                                     PhoneNumber = cp.PhoneNumber??""
                                 };
                             }).ToList();

            return Json(lst.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllConsultationUsers()
        {
            var companies = db.ConsultationUser.ToList().Select(x => new { ID = x.ConsultationUserId, Name = x.ConsultationName });

            return Json(companies.ToList(), JsonRequestBehavior.AllowGet);
        }
        [AuthorizedAction]
        public ActionResult SetConsultation()
        {
            RequestController._Request = null;
            return View();
        }
        [HttpPost]
        public ActionResult SaveConsultationWorkOrder(Models.Refactored.ConsultationWorkOrder consultationWorkOrder)
        {
            if (consultationWorkOrder != null && consultationWorkOrder.ConsultationWorkOrderId == 0 && ModelState.IsValid && consultationWorkOrder.WorkOrderNoFrom!=null)
            {
                var entity = new ConsultationWorkOrder();
                var requestFromConsultation = db.ConsultationWorkOrder.ToList().OrderByDescending(a => a.ConsultationWorkOrderId).FirstOrDefault(x => x.RequestId == consultationWorkOrder.RequestId);
                if (requestFromConsultation?.WorkOrderNoTo != null && consultationWorkOrder.WorkOrderNoFrom <= requestFromConsultation.WorkOrderNoTo)
                    return Json(-1, JsonRequestBehavior.AllowGet);
                entity.RequestId = consultationWorkOrder.RequestId;
                entity.WorkOrderNoFrom = consultationWorkOrder.WorkOrderNoFrom;
                if (consultationWorkOrder.IsHaveFinal)
                    entity.WorkOrderNoTo = consultationWorkOrder.WorkOrderNoTo;
                entity.ConsultationUserId = consultationWorkOrder.ConsultationUserId;
                entity.UserAdd = (long)Session["UserID"];
                entity.DateAdded = DateTime.Now;
                db.ConsultationWorkOrder.Add(entity);
                if (requestFromConsultation?.WorkOrderNoTo != null && requestFromConsultation.WorkOrderNoTo == null)
                    return Json(-2, JsonRequestBehavior.AllowGet);
                db.SaveChanges();
                consultationWorkOrder.ConsultationWorkOrderId = (int)entity.ConsultationWorkOrderId;
                return Json(1, JsonRequestBehavior.AllowGet);
            }

            return Json(0, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult DeleteConsultationWorkOrder(Models.Refactored.ConsultationWorkOrder consultationWorkOrder)
        {

            var entity = db.ConsultationWorkOrder.First(s => s.ConsultationWorkOrderId == consultationWorkOrder.ConsultationWorkOrderId);

            db.ConsultationWorkOrder.Attach(entity);
            db.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            db.SaveChanges();


            return Json(1, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ConsultationWorkOrderApproved_Save(IEnumerable<HttpPostedFileBase> FileNameApproved)
        {
            if (FileNameApproved != null)
            {
                foreach (var file in FileNameApproved)
                {
                    if (WorkOrderController._WorkOrder != null)
                    {
                        if (!Directory.Exists(Server.MapPath("~/UploadedFiles"))) Directory.CreateDirectory(Server.MapPath("~/UploadedFiles"));
                        if (!Directory.Exists(Server.MapPath("~/UploadedFiles/ConsultationApprovedWorkOrder"))) Directory.CreateDirectory(Server.MapPath("~/UploadedFiles/ConsultationApprovedWorkOrder"));
                        if (!Directory.Exists(Server.MapPath("~/UploadedFiles/ConsultationApprovedWorkOrder/" + WorkOrderController._WorkOrder.WorkOrderCode))) Directory.CreateDirectory(Server.MapPath("~/UploadedFiles/ConsultationApprovedWorkOrder/" + WorkOrderController._WorkOrder.WorkOrderCode));

                        var fileName = Path.GetFileName(file.FileName);
                        var physicalPath = Path.Combine(Server.MapPath("~/UploadedFiles/ConsultationApprovedWorkOrder/" + WorkOrderController._WorkOrder.WorkOrderCode), fileName);
                        WorkOrderController._WorkOrder.ConsultationFileNameApproved = fileName;

                        var entity = db.WorkOrders.First(s => s.WorkOrderId == WorkOrderController._WorkOrder.WorkOrderId);

                        entity.ConsultationFileNameApproved = WorkOrderController._WorkOrder.ConsultationFileNameApproved;
                        db.WorkOrders.Attach(entity);
                        db.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        db.SaveChanges();

                        // The files are not actually saved in this demo
                        file.SaveAs(physicalPath);
                    }
                }
            }

            return Content("");
        }

        public ActionResult GetConsultationWorkOrdersList([DataSourceRequest] DataSourceRequest request)
        {
            if (RequestController._Request != null)
            {
                var lst = db.ConsultationWorkOrder.Where(x => x.RequestId == RequestController._Request.RequestId).ToList()
                                 .Select(cp =>
                                 {
                                     var consultationUser = db.ConsultationUser.FirstOrDefault(x => x.ConsultationUserId == cp.ConsultationUserId);
                                     return new Models.Refactored.ConsultationWorkOrder
                                     {
                                         ConsultationWorkOrderId = cp.ConsultationWorkOrderId,
                                         ConsultationUserId = cp.ConsultationUserId,
                                         ConsultationUserName = consultationUser.ConsultationName,
                                         WorkOrderNoFrom = cp.WorkOrderNoFrom,
                                         WorkOrderNoTo = cp.WorkOrderNoTo ?? 0
                                     };
                                 }).ToList();

                return Json(lst.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
            return Json(new List<ConsultationUser>().ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        public ActionResult ConsultationApproval()
        {
            WorkOrderController._WorkOrder = null;
            RequestController._Request = null;
            return View();
        }
    }
}