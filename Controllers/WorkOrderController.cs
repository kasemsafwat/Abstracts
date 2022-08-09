using Abstracts.Models;
using Abstracts.Models.InterfaceService;
using Abstracts.Models.Pbo;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Abstracts.Models.MasterLoginModel;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using System.Globalization;

namespace Abstracts.Controllers
{
    public class WorkOrderController : BaseController
    {
        private AbstractsDBContext db = new AbstractsDBContext();
        private readonly ParadiseMasterLogInDBContext db2 = new ParadiseMasterLogInDBContext();
        private readonly IWorkOrderService workOrderService;
        private readonly IWorkOrderDetailsService workOrderDetailsService;
        private readonly IRequestService requestService;
        public static WorkOrders _WorkOrder;
        static long _NoteID = 0;
        public WorkOrderController(IWorkOrderService _workOrderService, IWorkOrderDetailsService _workOrderDetailsService, IRequestService _requestService)
        {
            workOrderService = _workOrderService;
            workOrderDetailsService = _workOrderDetailsService;
            requestService = _requestService;
        }
        public JsonResult GetNotificationItemNum()
        {
            return Json(_NoteID, JsonRequestBehavior.AllowGet);
        }
        [AuthorizedAction]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult WorkOrdersForCompany()
        {
            return View();
        }
        public ActionResult EditWorkOrdersForCompany()
        {
            return View();
        }
        public ActionResult SaveWorkOrder(Models.Refactored.WorkOrders workOrders)
        {

            return Json(_WorkOrder, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveWorkOrderForCompany(Models.Refactored.WorkOrders workOrders)
        {
            if (workOrders != null && workOrders.WorkOrderId == 0)
            {
                var wo = db.WorkOrders.Where(x => x.RequestId == workOrders.RequestId).OrderByDescending(x => x.WorkOrderId).FirstOrDefault();
                var woDetails = new List<WorkOrderDetails>();
                if (wo != null)
                    woDetails = db.WorkOrderDetails.Where(x => x.WorkOrderId == wo.WorkOrderId).ToList();

                var entity = new WorkOrders();
                long? AutoNo;
                if (db.WorkOrders.Where(x => x.RequestId == workOrders.RequestId).Max(x => x.AutoNo) == null) { AutoNo = 0; }
                else { AutoNo = db.WorkOrders.Where(x => x.RequestId == workOrders.RequestId).Max(x => x.AutoNo).Value; }
                AutoNo++;
                entity.AutoNo = AutoNo;
                entity.WorkOrderCode = Models.Pbo.PublicVariables.WorkOrderPrefixCode + AutoNo;
                var wo1 = db.WorkOrders.Where(x => x.RequestId == workOrders.RequestId).OrderByDescending(x => x.WorkOrderId).FirstOrDefault();
                if (wo1 != null && wo1.WorkOrderEndDate != null)
                    entity.WorkOrderStartDate = wo1.WorkOrderEndDate.Value.AddDays(1);
                else
                {
                    var request = db.Requests.Where(x => x.RequestId == workOrders.RequestId).FirstOrDefault();
                    entity.WorkOrderStartDate = request.RequestStartDate;
                }

                entity.WorkOrderEndDate = DateTime.ParseExact(workOrders.WorkOrderEndDateString, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                entity.Notes = workOrders.Notes;
                entity.RequestId = workOrders.RequestId;
                entity.FileName = workOrders.FileName;
                CompanyUsers user = (CompanyUsers)Session["User"];
                entity.CompanyUserIdadd = user.CompanyUsersId;
                entity.DateAddedCompanyUserId = DateTime.Now;
                entity.IsSubmit = false;

                db.WorkOrders.Add(entity);
                db.SaveChanges();
                workOrders.WorkOrderId = (int)entity.WorkOrderId;
                var workorder = db.WorkOrders.FirstOrDefault(x => x.WorkOrderId == entity.WorkOrderId);
                _WorkOrder = workorder;

                if (wo != null)
                {
                    foreach (var woDetail in woDetails)
                    {
                        woDetail.WorkOrderId = workOrders.WorkOrderId;
                        int res = workOrderDetailsService.Create(woDetail);
                        if (res == -3)
                            return Json(-3, JsonRequestBehavior.AllowGet);
                    }
                }

            }
            else
            {
            }
            var WorkOrder = db.WorkOrders.Where(x => x.WorkOrderId == workOrders.WorkOrderId).FirstOrDefault();
            workOrders.WorkOrderCode = WorkOrder.WorkOrderCode;
            Models.Refactored.WorkOrders result = new Models.Refactored.WorkOrders()
            {
                WorkOrderId = workOrders.WorkOrderId,
                WorkOrderCode = workOrders.WorkOrderCode
            };

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [AuthorizedAction]
        public ActionResult WorkOrderDetails()
        {
            return View();
        }
        public ActionResult SaveWorkOrderDetails(WorkOrderDetails workOrderDetails, List<long?> RequestDetailIdList)
        {
            if (RequestDetailIdList == null) return Json(-1, JsonRequestBehavior.AllowGet);
            if (workOrderDetails != null && workOrderDetails.WorkOrderDetailId == 0)
            {
                foreach (var obj in RequestDetailIdList)
                {
                    workOrderDetails.RequestDetailId = obj;
                    int res = workOrderDetailsService.Create(workOrderDetails);
                    if (res == -3)
                    {
                        return Json(-3, JsonRequestBehavior.AllowGet);
                    }
                    else
                        workOrderDetailsService.Update(workOrderDetails);
                }

            }

            return Json(1, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveWorkOrderDetailForGrid([DataSourceRequest] DataSourceRequest request, Models.Refactored.WorkOrderDetails workOrderDetails)
        {
            var entity = db.WorkOrderDetails.First(s => s.WorkOrderDetailId == workOrderDetails.WorkOrderDetailId);
            if (workOrderDetails != null && workOrderDetails.WorkOrderDetailId != 0)
                workOrderDetailsService.Update(entity);

            if (entity == null) { return Json(1, JsonRequestBehavior.AllowGet); }
            return Json(workOrderDetailsService.ReadWithDetails(entity.WorkOrderId, entity.WorkOrderDetailId).ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteWorkOrderDetail([DataSourceRequest] DataSourceRequest request, Models.Refactored.WorkOrderDetails workOrderDetails)
        {
            var entity = db.WorkOrderDetails.First(s => s.WorkOrderDetailId == workOrderDetails.WorkOrderDetailId);

            db.Remove(entity);

            db.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            db.SaveChanges();

            return Json(workOrderDetailsService.ReadWithDetails(entity.WorkOrderId, entity.WorkOrderDetailId).ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        public void SetNoteIDForGrid(long NoteID)
        {
            _NoteID = NoteID;
        }
        public JsonResult GetAllWorkOrders()
        {
            using (var db = GetContext())
            {
                var workOrders = db.WorkOrders.ToList();
                List<Models.Refactored.WorkOrders> list = new List<Models.Refactored.WorkOrders>();
                foreach (var o in workOrders)
                {
                    list.Add(o);

                }
                return Json(list.ToList(), JsonRequestBehavior.AllowGet);
            }
        }
        [AuthorizedAction]
        public ActionResult WorkOrderApproval()
        {
            return View();
        }
        public ActionResult GetWorkOrderDetailList([DataSourceRequest] DataSourceRequest request)
        {
            if (_WorkOrder == null)
            {
                List<Models.Refactored.WorkOrderDetails> s = new List<Models.Refactored.WorkOrderDetails>();
                return Json(s.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
            return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetWorkOrderDetailListForStatus([DataSourceRequest] DataSourceRequest request)
        {
            var resultObj = workorderDetails.Where(x => x.WorkOrderId == _WorkOrder.WorkOrderId).OrderByDescending(x => x.WorkOrderDetailId).ToList();


            return Json(resultObj.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetWorkOrderDetailList1([DataSourceRequest] DataSourceRequest request)
        {
            if (_WorkOrder == null)
            {
                List<Models.Refactored.WorkOrderDetails> s = new List<Models.Refactored.WorkOrderDetails>();

                return Json(s.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
            var result = workOrderDetailsService.Read1(_WorkOrder.WorkOrderId);
            Session["WorkOrderDetails"] = result;
            return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetWorkOrderDetailListForCompany([DataSourceRequest] DataSourceRequest request)
        {
            if (_WorkOrder == null)
            {
                List<Models.Refactored.WorkOrderDetails> s = new List<Models.Refactored.WorkOrderDetails>();

                return Json(s.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
            else
            {

                var result = db.WorkOrderDetails.ToList().Where(x => x.WorkOrderId == _WorkOrder.WorkOrderId).OrderByDescending(x => x.RequestDetailId).ToList();
                Session["WorkOrderDetails"] = result;
                return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetWorkOrderDetailListFullApproved([DataSourceRequest] DataSourceRequest request)
        {
            if (_WorkOrder == null)
            {
                List<Models.Refactored.WorkOrderDetails> s = new List<Models.Refactored.WorkOrderDetails>();
                return Json(s.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }

            return Json(workOrderDetailsService.Read1(_WorkOrder.WorkOrderId).ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Async_Save(IEnumerable<HttpPostedFileBase> FileName)
        {
            // The Name of the Upload component is "files"
            if (FileName != null)
            {
                foreach (var file in FileName)
                {
                    if (_WorkOrder != null)
                    {
                        if (!Directory.Exists(Server.MapPath("~/UploadedFiles"))) Directory.CreateDirectory(Server.MapPath("~/UploadedFiles"));
                        if (!Directory.Exists(Server.MapPath("~/UploadedFiles/WorkOrder"))) Directory.CreateDirectory(Server.MapPath("~/UploadedFiles/WorkOrder"));
                        if (!Directory.Exists(Server.MapPath("~/UploadedFiles/WorkOrder/" + _WorkOrder.WorkOrderId))) Directory.CreateDirectory(Server.MapPath("~/UploadedFiles/WorkOrder/" + _WorkOrder.WorkOrderId));



                        var fileName = Path.GetFileName(file.FileName);
                        var physicalPath = Path.Combine(Server.MapPath("~/UploadedFiles/WorkOrder/" + _WorkOrder.WorkOrderId), fileName);
                        _WorkOrder.FileName = fileName;
                        workOrderService.Update(_WorkOrder);

                        // The files are not actually saved in this demo
                        file.SaveAs(physicalPath);
                    }
                }
            }
            return Content("");
        }
        public ActionResult WorkOrderApproved_Save(IEnumerable<HttpPostedFileBase> FileNameApproved)
        {
            // The Name of the Upload component is "files"
            if (FileNameApproved != null)
            {
                foreach (var file in FileNameApproved)
                {
                    if (_WorkOrder != null)
                    {
                        long UserID = (long)Session["UserID"];
                        var user = db2.Users.FirstOrDefault(x => x.UserId == UserID);
                    }
                }
            }

            // Return an empty string to signify success
            return Content("");
        }
        public JsonResult GetWorkOrderDataByID(int WorkOrderId, int RequestID)
        {
            if (WorkOrderId != 0)
            {
                using (var db = GetContext())
                {
                    var workOrder = db.WorkOrders.Where(x => x.WorkOrderId == WorkOrderId && x.RequestId == RequestID).FirstOrDefault();
                    Models.Refactored.WorkOrders WorkOrder = new Models.Refactored.WorkOrders();
                    var request = db.Requests.Where(x => x.RequestId == RequestID).FirstOrDefault();
                    List<int> departments = new List<int>();
                    var workorderDetails = db.WorkOrderDetails.Where(x => x.WorkOrderId == WorkOrderId).ToList();
                    var requestDetails = db.RequestDetails.ToList();
                    foreach (var item in workorderDetails)
                    {
                        var requestDetail = requestDetails.FirstOrDefault(s => s.RequestDetailId == item.RequestDetailId);
                        departments.Add((int)requestDetail.DepartmentId);
                    }
                    _WorkOrder = workOrder;
                    return Json(WorkOrder, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(0, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetWorkOrderListByRequestID(int RequestID)
        {
            using (var db = GetContext())
            {
                var workOrder = db.WorkOrders.Where(x => x.RequestId == RequestID && (x.IsSubmit == false || x.IsSubmit == null)).Select(r => new { WorkOrderId = r.WorkOrderId, WorkOrderCode = r.WorkOrderCode }).ToList();

                return Json(workOrder, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetWorkOrderListByRequestIDForStatus(int RequestID)
        {
            using (var db = GetContext())
            {
                var workOrder = db.WorkOrders.Where(x => x.RequestId == RequestID).Select(r => new { WorkOrderId = r.WorkOrderId, WorkOrderCode = r.WorkOrderCode }).ToList();

                return Json(workOrder, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetWorkOrderApprovalListByRequestID(int RequestID)
        {
            int userID = int.Parse(Session["UserID"].ToString());
            var Employee = db2.Users.Where(x => x.UserId == userID).FirstOrDefault();
            bool flag = false;
            var workOrders = new List<WorkOrders>();
            WorkOrders _WorkOrder = new WorkOrders();

            var workOrder = db.WorkOrders.Where(x => x.RequestId == RequestID && (x.ConsultationSubmit == true || x.ConsultationSubmit == null)).ToList();

            foreach (var wo in workOrder)
            {
                flag = false;
                var wods = db.WorkOrderDetails.Where(x => x.WorkOrderId == wo.WorkOrderId).OrderByDescending(x => x.IsApproved).ToList();
                foreach (var wod in wods)
                {
                    var requestDetails = db.RequestDetails.Where(x => x.RequestDetailId == wod.RequestDetailId).FirstOrDefault();
                    if (Employee.DepartmentId == requestDetails.DepartmentId)
                    {
                        if (wod.IsApproved == true) { flag = false; }
                        if (wod.IsApproved == false) { flag = true; }
                    }
                }
                if (flag) workOrders.Add(wo);
            }
            var result = workOrders.Select(r => new { WorkOrderId = r.WorkOrderId, WorkOrderCode = r.WorkOrderCode }).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetWorkOrderApprovedListByRequestID(int RequestID)
        {
            using (var db = GetContext())
            {
                bool flag = false;
                var workOrders = new List<WorkOrders>();
                WorkOrders _WorkOrder = new WorkOrders();
                var workOrder = db.WorkOrders.Where(x => x.RequestId == RequestID && x.TechnicalSubmit == true).ToList();

                foreach (var wo in workOrder)
                {
                    flag = false;
                    var wods = db.WorkOrderDetails.Where(x => x.WorkOrderId == wo.WorkOrderId).OrderByDescending(x => x.IsApproved).ToList();
                    foreach (var wod in wods)
                    {
                        if (wod.IsApproved == false) { flag = false; }
                        if (wod.IsApproved == true) { flag = true; }
                    }
                    if (flag) workOrders.Add(wo);
                }
                var result = workOrders.Select(r => new { WorkOrderId = r.WorkOrderId, WorkOrderCode = r.WorkOrderCode }).ToList();

                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetWorkOrderApprovedForTechnicalListByRequestID(int RequestID)
        {
            using (var db = GetContext())
            {
                bool flag = false;
                var workOrders = new List<WorkOrders>();
                WorkOrders _WorkOrder = new WorkOrders();
                var workOrder = db.WorkOrders.Where(x => x.RequestId == RequestID && (x.TechnicalSubmit == false || x.TechnicalSubmit == null)).ToList();
                foreach (var wo in workOrder)
                {
                    flag = false;
                    var wods = db.WorkOrderDetails.Where(x => x.WorkOrderId == wo.WorkOrderId).OrderByDescending(x => x.IsApproved).ToList();
                }
                var result = workOrders.Select(r => new { WorkOrderId = r.WorkOrderId, WorkOrderCode = r.WorkOrderCode }).ToList();

                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetWorkOrderApprovedListByRequestIDForConsultation(int RequestID)
        {
            var workOrders = new List<WorkOrders>();
            var userID = (long)Session["UserID"];
            List<long> assigned = new List<long>();
            var AssignedWorkOrders = db.ConsultationWorkOrder.Where(x => x.RequestId == RequestID && x.ConsultationUserId == userID);
            foreach (var a in AssignedWorkOrders)
            {
                assigned.Add((long)a.WorkOrderNoFrom);

                for (int i = 0; i < a.WorkOrderNoTo; i++)
                {
                    a.WorkOrderNoFrom++;
                    if (a.WorkOrderNoFrom <= a.WorkOrderNoTo) { assigned.Add((long)a.WorkOrderNoFrom); }
                }
            }

            var workOrder = db.WorkOrders.ToList().Select(x =>
            {
                if (assigned.Contains((long)x.AutoNo) && x.RequestId == RequestID)
                {
                    return new WorkOrders()
                    {
                        WorkOrderId = x.WorkOrderId,
                        WorkOrderCode = x.WorkOrderCode
                    };
                }
                return null;
            });

            if (workOrder != null)
            {
                foreach (var wo in workOrder)
                {
                    if (wo != null)
                    {
                        var flag = false;
                        var wods = db.WorkOrderDetails.Where(x => x.WorkOrderId == wo.WorkOrderId).OrderByDescending(x => x.IsApproved).ToList();
                        Session["WorkOrderDetails"] = wods;
                        foreach (var wod in wods)
                        {
                            if (wod.IsApproved == false) { flag = false; }
                            if (wod.IsApproved == true) { flag = true; }
                        }
                        if (!flag) workOrders.Add(wo);
                    }
                }
            }
            var result = workOrders.Select(r => new { WorkOrderId = r.WorkOrderId, WorkOrderCode = r.WorkOrderCode }).ToList();


            return Json(result.Distinct(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetWorkOrderApprovedListByRequestIDForCompany(int RequestID)
        {
            var workOrders = new List<WorkOrders>();

            var workOrder = db.WorkOrders.Where(x => x.RequestId == RequestID).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetWorkOrderApprovedList()
        {

            int userID = int.Parse(Session["UserID"].ToString());
            var Employee = db2.Users.Where(x => x.UserId == userID).FirstOrDefault();
            var workOrders = new List<WorkOrders>();
            WorkOrders _WorkOrder = new WorkOrders();
            IEnumerable<Models.Refactored.WorkOrders> result = new List<Models.Refactored.WorkOrders>();
            bool flag = false;
            if (Employee == null)
            {

            }


            var resultCount = workOrders.Count();
            var ListResult = new { result, resultCount };
            return Json(ListResult, JsonRequestBehavior.AllowGet);

        }
        public ActionResult Async_Remove(string[] FileName)
        {
            if (_WorkOrder != null)
            {

                var physicalPath = Path.Combine(Server.MapPath("~/UploadedFiles/WorkOrder/" + _WorkOrder.WorkOrderCode + "/"), _WorkOrder.FileName);

                if (System.IO.File.Exists(physicalPath))
                {
                    // The files are not actually removed in this demo
                    System.IO.File.Delete(physicalPath);
                }

            }

            // Return an empty string to signify success
            return Content("");
        }
        public ActionResult WorkOrderStatus()
        {
            _WorkOrder = null;
            return View();
        }

        public JsonResult getDataForApprovalBtn(int WorkOrderId)
        {
            if (WorkOrderId != 0)
            {
                int userID = int.Parse(Session["UserID"].ToString());
                var Employee = db2.Users.Where(x => x.UserId == userID).FirstOrDefault();

                var workOrderDetailsList = new List<WorkOrderDetails>();
                var workOrder = db.WorkOrders.FirstOrDefault(x => x.WorkOrderId == WorkOrderId);
                var workOrderDetails = db.WorkOrderDetails.Where(x => x.WorkOrderId == WorkOrderId).ToList();
                foreach (var rd in workOrderDetails)
                {
                    var RequestDetails = db.RequestDetails.Where(x => rd.RequestDetailId == x.RequestDetailId && x.DepartmentId == Employee.DepartmentId).FirstOrDefault();
                    if (rd.IsApproved == false)
                        workOrderDetailsList.Add(rd);
                }

                foreach (var wod in workOrderDetailsList)
                {
                    if (wod.IsApproved == true) return Json(0, JsonRequestBehavior.AllowGet);
                }

                return Json(1, JsonRequestBehavior.AllowGet);

            }
            return Json(1, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getDataForApprovalBtnForTechnical(int WorkOrderId)
        {
            if (WorkOrderId != 0)
            {
                var workOrderDetailsList = new List<WorkOrderDetails>();
                var workOrderDetails = db.WorkOrderDetails.Where(x => x.WorkOrderId == WorkOrderId).FirstOrDefault();
                var workorder = db.WorkOrders.FirstOrDefault(wo => wo.WorkOrderId == WorkOrderId);
                if (workorder.TechnicalSubmit != true)
                    workOrderDetailsList.Add(workOrderDetails);

                foreach (var wod in workOrderDetailsList)
                {
                    if (wod != null && wod.IsApproved == true) return Json(1, JsonRequestBehavior.AllowGet);

                }


                return Json(0, JsonRequestBehavior.AllowGet);

            }
            return Json(0, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getDataForApprovalBtnForConsultation(string WorkOrderId)
        {
            if (WorkOrderId != "")
            {
                var workOrder = db.WorkOrders.FirstOrDefault(x => x.WorkOrderId == long.Parse(WorkOrderId));
                if (workOrder.ConsultationSubmit == true)
                    return Json(0, JsonRequestBehavior.AllowGet);
            }
            return Json(1, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SetWorkOrderApprovedForConsultation(int WorkOrderId)
        {
            if (WorkOrderId != 0)
            {
                ConsultationUser user = (ConsultationUser)Session["User"];
                var entity = db.WorkOrders.Where(x => x.WorkOrderId == WorkOrderId).FirstOrDefault();
                entity.ConsultationSubmit = true;
                entity.ConsultationDateSubmit = DateTime.Now;
                entity.ConsultationUserId = user.ConsultationUserId;
                db.WorkOrders.Attach(entity);
                db.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            return Json(0, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SetWorkOrderApproved(int WorkOrderId)
        {
            if (WorkOrderId != 0)
            {
                var workOrder = db.WorkOrders.FirstOrDefault(x => x.WorkOrderId == WorkOrderId);
                var PreviousWorkOrder = db.WorkOrders.FirstOrDefault(x => x.AutoNo == workOrder.AutoNo - 1 && x.RequestId == workOrder.RequestId);
                bool flag = false;
                if (PreviousWorkOrder != null)
                {
                    var workOrderDetails = db.WorkOrderDetails.Where(x => x.WorkOrderId == PreviousWorkOrder.WorkOrderId).ToList();

                    flag = true;

                    foreach (var wod in workOrderDetails)
                    {

                        if (wod.IsApproved == false) { flag = false; }
                        if (wod.IsApproved == true) { flag = true; }

                    }
                    if (!flag) return Json(-1, JsonRequestBehavior.AllowGet);
                }
                int userID = int.Parse(Session["UserID"].ToString());
                var Employee = db2.Users.Where(x => x.UserId == userID).FirstOrDefault();
                var RequestDetails = db.RequestDetails.Where(x => x.DepartmentId == Employee.DepartmentId).ToList();
              

                return Json(1, JsonRequestBehavior.AllowGet);

            }
            return Json(0, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SetWorkOrderApprovedForTechnical(int WorkOrderId)
        {
            if (WorkOrderId != 0)
            {
                var workOrder = db.WorkOrders.FirstOrDefault(x => x.WorkOrderId == WorkOrderId);
                return Json(1, JsonRequestBehavior.AllowGet);

            }
            return Json(0, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DrawNotifications(List<Models.Refactored.WorkOrders> list)
        {
            string line = "";
            if (list != null)
            {
             

            }
            return Json(line, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetWorkOrderFromNotification(int WorkOrderID)
        {
            if (WorkOrderID != 0)
            {
                return Json("/WorkOrder/WorkOrderApproval", JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SetWorkOrderSubmitted(int WorkOrderId)
        {
            if (WorkOrderId != 0)
            {
                using (var db = GetContext())
                {
                    var workOrder = db.WorkOrders.Where(x => x.WorkOrderId == WorkOrderId).FirstOrDefault();
                    return Json(workOrder, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(0, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetWorkOrderApproved()
        {
            long? AutoNo;
            if (db.WorkOrders.Max(x => x.AutoNo) == null) { AutoNo = 0; }
            else { AutoNo = db.WorkOrders.Max(x => x.AutoNo).Value; }


            AutoNo++;

            ViewBag.WorkOrderCode = PublicVariables.WorkOrderPrefixCode + AutoNo;
            _WorkOrder = null;
            return View();
        }

        static string ResultMsg = "";
        public string getstatus()// Upload Excel
        {
            return ResultMsg;
        }
        public ActionResult Async_Save_WorkOrderExcel(IEnumerable<HttpPostedFileBase> WorkOrderExcelFileName)
        {
            // The Name of the Upload component is "files"
            if (WorkOrderExcelFileName != null)
            {
                foreach (var file in WorkOrderExcelFileName)
                {
                    if (RequestController._Request != null)
                    {

                        if (!Directory.Exists(Server.MapPath("~/UploadedFiles"))) Directory.CreateDirectory(Server.MapPath("~/UploadedFiles"));
                        if (!Directory.Exists(Server.MapPath("~/UploadedFiles/ExcelFile"))) Directory.CreateDirectory(Server.MapPath("~/UploadedFiles/ExcelFile"));

                        var fileName = Path.GetFileName(file.FileName);
                        var physicalPath = Path.Combine(Server.MapPath("~/UploadedFiles/ExcelFile/"), fileName);
                        file.SaveAs(physicalPath);


                    }
                }
            }

            // Return an empty string to signify success
            return Content("");
        }
    }
}