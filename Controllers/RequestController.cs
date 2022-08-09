using Abstracts.Models;
using Abstracts.Models.InterfaceService;
using Abstracts.Models.MasterLoginModel;
using Abstracts.Models.Pbo;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Abstracts.Controllers
{
    public class RequestController : BaseController
    {
        readonly AbstractsDBContext db = new AbstractsDBContext();
        private readonly ParadiseMasterLogInDBContext db2 = new ParadiseMasterLogInDBContext();
        private readonly IRequestService requestService;
        private readonly IDepartmentService departmentService;
        static string _RequestNo;
        static DateTime? _RequestDate;
        public static Requests _Request;
        static RequestDetails _RequestDetails;
        public RequestController(IRequestService _requestService, IDepartmentService _departmentService)
        {
            departmentService = _departmentService;
            requestService = _requestService;
        }
        [AuthorizedAction]
        public ActionResult Index()
        {
            _Request = null;

            return View();
        }
        [AuthorizedAction]
        public ActionResult Edit()
        {
            _Request = null;

            return View();
        }
        [AuthorizedAction]
        public ActionResult EditReport()
        {
            _Request = null;

            return View();
        }

        public void SetRequestID(string RequestID)
        {
            if (RequestID != "")
                _Request = new Requests() { RequestId = long.Parse(RequestID) };
        }
        public void SearchForRequest(DateTime? RequestDate, string RequestNo = "")
        {
            _RequestNo = RequestNo;
            _RequestDate = RequestDate;
        }
        public ActionResult GetRequestList([DataSourceRequest] DataSourceRequest request)
        {
            var result = new List<Models.Refactored.Requests>();
            var LogRequestFile = "";

            if (!string.IsNullOrEmpty(_RequestNo) && _RequestDate != null)
            {
                result = db.Requests.ToList().OrderBy(x => x.IsLocked).OrderByDescending(x => x.RequestId).Where(x => x.RequestNo.Contains(_RequestNo) && x.RequestDate == _RequestDate)
                            .Select(r =>
                            {
                                var requestdetails = db.RequestDetails.Where(x => x.RequestId == r.RequestId).ToList().Select(x => x.RequestDetailId);
                                var _result = db.RequestLog.Where(z => z.RequestId == r.RequestId || requestdetails.Contains((long)z.RequestDetailId)).FirstOrDefault();
                                if (_result != null) LogRequestFile = _result.LogFileName;
                                var Company = new Companies();
                                if (r.CompanyId != null)
                                {
                                    Company = db.Companies.Where(x => x.CompanyId == r.CompanyId).FirstOrDefault();
                                }
                                var _lock = "";
                                if ((bool)r.IsLocked) _lock = "نعم";
                                else _lock = "لا";
                                return new Models.Refactored.Requests
                                {
                                    RequestId = r.RequestId,
                                    RequestCode = r.RequestCode,
                                    LogFileName = LogRequestFile,
                                    RequestName = r.RequestName,
                                    IsLocked = r.IsLocked,
                                    RequestDate = r.RequestDate,
                                    CompanyId = r.CompanyId,
                                    Company = Company == null ? null : Company.CompanyName,
                                    RequestAmount = r.RequestAmount,
                                    RequestDuration = r.RequestDuration,
                                    Notes = r.Notes,
                                    Locked = _lock,
                                    FileNameDetails = r.RequestDetailsFileName,
                                    RequestStartDate = r.RequestStartDate,
                                    RequestEndDate = r.RequestEndDate,
                                    RequestDateString = string.Format("{0:dd/MM/yyyy}", r.RequestDate),
                                    RequestStartDateString = string.Format("{0:dd/MM/yyyy}", r.RequestStartDate),
                                    RequestEndDateString = string.Format("{0:dd/MM/yyyy}", r.RequestEndDate),
                                    FileName = r.RequestFileName,
                                    RequestNo = r.RequestNo
                                };

                            }).ToList();
            }
            else if (!string.IsNullOrEmpty(_RequestNo))
            {
                result = db.Requests.ToList().OrderBy(x => x.IsLocked).OrderByDescending(x => x.RequestId).Where(x => x.RequestNo.Contains(_RequestNo))
                            .Select(r =>
                            {
                                var requestdetails = db.RequestDetails.Where(x => x.RequestId == r.RequestId).ToList().Select(x => x.RequestDetailId);
                                var _result = db.RequestLog.Where(z => z.RequestId == r.RequestId || requestdetails.Contains((long)z.RequestDetailId)).FirstOrDefault();
                                if (_result != null) LogRequestFile = _result.LogFileName;
                                var Company = new Companies();
                                if (r.CompanyId != null)
                                {
                                    Company = db.Companies.Where(x => x.CompanyId == r.CompanyId).FirstOrDefault();
                                }
                                var _lock = "";
                                if ((bool)r.IsLocked) _lock = "نعم";
                                else _lock = "لا";
                                return new Models.Refactored.Requests
                                {
                                    RequestId = r.RequestId,
                                    RequestCode = r.RequestCode,
                                    RequestName = r.RequestName,
                                    IsLocked = r.IsLocked,
                                    LogFileName = LogRequestFile,
                                    RequestDate = r.RequestDate,
                                    CompanyId = r.CompanyId,
                                    Company = Company == null ? null : Company.CompanyName,
                                    RequestAmount = r.RequestAmount,
                                    RequestDuration = r.RequestDuration,
                                    Notes = r.Notes,
                                    Locked = _lock,
                                    FileNameDetails = r.RequestDetailsFileName,
                                    RequestStartDate = r.RequestStartDate,
                                    RequestEndDate = r.RequestEndDate,
                                    RequestDateString = string.Format("{0:dd/MM/yyyy}", r.RequestDate),
                                    RequestStartDateString = string.Format("{0:dd/MM/yyyy}", r.RequestStartDate),
                                    RequestEndDateString = string.Format("{0:dd/MM/yyyy}", r.RequestEndDate),
                                    FileName = r.RequestFileName,
                                    RequestNo = r.RequestNo
                                };

                            }).ToList();
            }
            else if (_RequestDate != null)
            {
                result = db.Requests.ToList().OrderBy(x => x.IsLocked).OrderByDescending(x => x.RequestId).Where(x => x.RequestDate == _RequestDate)
                          .Select(r =>
                          {
                              var requestdetails = db.RequestDetails.Where(x => x.RequestId == r.RequestId).ToList().Select(x => x.RequestDetailId);
                              var _result = db.RequestLog.Where(z => z.RequestId == r.RequestId || requestdetails.Contains((long)z.RequestDetailId)).FirstOrDefault();
                              if (_result != null) LogRequestFile = _result.LogFileName;
                              var Company = new Companies();
                              if (r.CompanyId != null)
                              {
                                  Company = db.Companies.Where(x => x.CompanyId == r.CompanyId).FirstOrDefault();
                              }
                              var _lock = "";
                              if ((bool)r.IsLocked) _lock = "نعم";
                              else _lock = "لا";
                              return new Models.Refactored.Requests
                              {
                                  RequestId = r.RequestId,
                                  RequestCode = r.RequestCode,
                                  RequestName = r.RequestName,
                                  IsLocked = r.IsLocked,
                                  LogFileName = LogRequestFile,
                                  RequestDate = r.RequestDate,
                                  CompanyId = r.CompanyId,
                                  Company = Company == null ? null : Company.CompanyName,
                                  RequestAmount = r.RequestAmount,
                                  RequestDuration = r.RequestDuration,
                                  Notes = r.Notes,
                                  FileNameDetails = r.RequestDetailsFileName,
                                  Locked = _lock,
                                  RequestStartDate = r.RequestStartDate,
                                  RequestEndDate = r.RequestEndDate,
                                  RequestDateString = string.Format("{0:dd/MM/yyyy}", r.RequestDate),
                                  RequestStartDateString = string.Format("{0:dd/MM/yyyy}", r.RequestStartDate),
                                  RequestEndDateString = string.Format("{0:dd/MM/yyyy}", r.RequestEndDate),
                                  FileName = r.RequestFileName,
                                  RequestNo = r.RequestNo
                              };

                          }).ToList();
            }
            else
            {
                result = (List<Models.Refactored.Requests>)requestService.Read();
            }

            return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetRequestLogList([DataSourceRequest] DataSourceRequest request)
        {
            if (_Request != null)
            {
                var requestdetails = db.RequestDetails.Where(x => x.RequestId == _Request.RequestId).ToList().Select(x => x.RequestDetailId);
                var result = db.RequestLog.Where(z => z.RequestId == _Request.RequestId || requestdetails.Contains((long)z.RequestDetailId)).ToList().Select(x =>
                    {
                        var Request = db.Requests.FirstOrDefault(r => r.RequestId == x.RequestId);
                        if (Request == null) Request = new Requests();
                        var RequestDetails = db.RequestDetails.FirstOrDefault(r => r.RequestDetailId == x.RequestDetailId);
                        if (RequestDetails == null) RequestDetails = new RequestDetails();
                        string TypeName = "";
                        if (x.Type == 1) TypeName = "مد مدة";
                        else TypeName = " تعديل سعر - كمية - بنود مستجدة";
                        var userAdd = db2.Users.FirstOrDefault(u => u.UserId == x.UserIdAdd);
                        return new Models.Refactored.RequestLog
                        {
                            LogFileName = x.LogFileName ?? "",
                            NewQty = x.NewQty ?? 0,
                            NewRequestDuration = x.NewRequestDuration ?? 0,
                            NewUnitPrice = x.NewUnitPrice ?? 0,
                            Qty = x.Qty ?? 0,
                            RequestCode = Request.RequestCode ?? "",
                            RequestDetailCode = RequestDetails.RequestDetailCode ?? "",
                            RequestDetailId = x.RequestDetailId ?? 0,
                            RequestDuration = x.RequestDuration ?? 0,
                            RequestId = x.RequestId ?? 0,
                            RequestLogId = x.RequestLogId,
                            DateAdd = x.DateAdd.Value.ToShortDateString(),
                            UserAddName = userAdd.UserFullName,
                            TypeName = TypeName,
                            Type = x.Type,
                            UnitPrice = x.UnitPrice ?? 0
                        };
                    });
                return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
            return Json(new List<Models.Refactored.RequestLog>().ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetRequestByUserIDList([DataSourceRequest] DataSourceRequest request)
        {
            int userID = int.Parse(Session["UserID"].ToString());
            var user = db2.Users.FirstOrDefault(x => x.UserId == userID);
            var result = new List<Models.Refactored.Requests>();
            using (var db = GetContext())
            {
                var ReqList = db.UserRequests.Where(x => x.UserId == userID).Select(x => x.RequestId).ToList();
                result = db.Requests.ToList().OrderBy(x => x.IsLocked).OrderByDescending(x => x.RequestId).Where(x => ReqList.Contains(x.RequestId))
                            .Select(r =>
                            {
                                var Company = new Companies();
                                if (r.CompanyId != null)
                                {
                                    Company = db.Companies.Where(x => x.CompanyId == r.CompanyId).FirstOrDefault();
                                }
                                var _lock = "";
                                if ((bool)r.IsLocked) _lock = "نعم";
                                else _lock = "لا";
                                return new Models.Refactored.Requests
                                {
                                    UserDepartment = user.DepartmentId,
                                    RequestId = r.RequestId,
                                    RequestCode = r.RequestCode,
                                    RequestName = r.RequestName,
                                    IsLocked = r.IsLocked,
                                    RequestDate = r.RequestDate,
                                    CompanyId = r.CompanyId,
                                    Company = Company == null ? null : Company.CompanyName,
                                    RequestAmount = r.RequestAmount,
                                    RequestDuration = r.RequestDuration,
                                    Notes = r.Notes,
                                    Locked = _lock,
                                    FileNameDetails = r.RequestDetailsFileName,
                                    RequestStartDate = r.RequestStartDate,
                                    RequestEndDate = r.RequestEndDate,
                                    RequestDateString = string.Format("{0:dd/MM/yyyy}", r.RequestDate),
                                    RequestStartDateString = string.Format("{0:dd/MM/yyyy}", r.RequestStartDate),
                                    RequestEndDateString = string.Format("{0:dd/MM/yyyy}", r.RequestEndDate),
                                    FileName = r.RequestFileName,
                                    RequestNo = r.RequestNo
                                };

                            }).ToList();
            }
            return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetRequestByCompanyUserIDList([DataSourceRequest] DataSourceRequest request)
        {
            CompanyUsers user = (CompanyUsers)Session["User"];
            var requestIds = db.CompanyUsers.Where(x => x.UserName == user.UserName).Select(x => x.RequestId).ToList();

            var result = new List<Models.Refactored.Requests>();


            result = db.Requests.ToList().OrderBy(x => x.IsLocked).OrderByDescending(x => x.RequestId).Where(x => requestIds.Contains(x.RequestId))
                        .Select(r =>
                        {
                            var Company = new Companies();
                            if (r.CompanyId != null)
                            {
                                Company = db.Companies.Where(x => x.CompanyId == r.CompanyId).FirstOrDefault();
                            }
                            var _lock = "";
                            if ((bool)r.IsLocked) _lock = "نعم";
                            else _lock = "لا";
                            return new Models.Refactored.Requests
                            {
                                RequestId = r.RequestId,
                                RequestCode = r.RequestCode,
                                RequestName = r.RequestName,
                                IsLocked = r.IsLocked,
                                RequestDate = r.RequestDate,
                                CompanyId = r.CompanyId,
                                Company = Company == null ? null : Company.CompanyName,
                                RequestAmount = r.RequestAmount,
                                RequestDuration = r.RequestDuration,
                                Notes = r.Notes,
                                Locked = _lock,
                                FileNameDetails = r.RequestDetailsFileName,
                                RequestStartDate = r.RequestStartDate,
                                RequestEndDate = r.RequestEndDate,
                                RequestDateString = r.RequestDate == null ? null : r.RequestDate.Value.ToShortDateString().ToString(),
                                RequestStartDateString = r.RequestStartDate == null ? null : r.RequestStartDate.Value.ToShortDateString(),
                                RequestEndDateString = r.RequestEndDate == null ? null : r.RequestEndDate.Value.ToShortDateString(),
                                FileName = r.RequestFileName,
                                RequestNo = r.RequestNo
                            };

                        }).ToList();

            return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetRequestByConsultationUserIDList([DataSourceRequest] DataSourceRequest request)
        {
            ConsultationUser user = (ConsultationUser)Session["User"];

            var requestIds = db.ConsultationWorkOrder.Where(x => x.ConsultationUserId == user.ConsultationUserId).Select(x => x.RequestId).ToList();

            var result = new List<Models.Refactored.Requests>();

            result = db.Requests.ToList().OrderBy(x => x.IsLocked).OrderByDescending(x => x.RequestId).Where(x => requestIds.Contains(x.RequestId))
                        .Select(r =>
                        {
                            var Company = new Companies();
                            if (r.CompanyId != null)
                                Company = db.Companies.Where(x => x.CompanyId == r.CompanyId).FirstOrDefault();
                            var _lock = "";
                            if ((bool)r.IsLocked) _lock = "نعم";
                            else _lock = "لا";
                            return new Models.Refactored.Requests
                            {
                                RequestId = r.RequestId,
                                RequestCode = r.RequestCode,
                                RequestName = r.RequestName,
                                IsLocked = r.IsLocked,
                                RequestDate = r.RequestDate,
                                CompanyId = r.CompanyId,
                                Company = Company == null ? null : Company.CompanyName,
                                RequestAmount = r.RequestAmount,
                                RequestDuration = r.RequestDuration,
                                Notes = r.Notes,
                                Locked = _lock,
                                FileNameDetails = r.RequestDetailsFileName,
                                RequestStartDate = r.RequestStartDate,
                                RequestEndDate = r.RequestEndDate,
                                RequestDateString = r.RequestDate == null ? null : r.RequestDate.Value.ToShortDateString().ToString(),
                                RequestStartDateString = r.RequestStartDate == null ? null : r.RequestStartDate.Value.ToShortDateString(),
                                RequestEndDateString = r.RequestEndDate == null ? null : r.RequestEndDate.Value.ToShortDateString(),
                                FileName = r.RequestFileName,
                                RequestNo = r.RequestNo
                            };
                        }).ToList();

            return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetRequestEndDate(int Duration, string StartRequestDate)
        {
            if (StartRequestDate != null)
            {
                DateTime requestDateTime;
                try { requestDateTime = DateTime.ParseExact(StartRequestDate, "dd-MM-yyyy", CultureInfo.InvariantCulture); }
                catch
                {
                    try { requestDateTime = DateTime.ParseExact(StartRequestDate, "dd/MM/yyyy", CultureInfo.InvariantCulture); }
                    catch
                    {
                        try { requestDateTime = DateTime.ParseExact(StartRequestDate, "yyyy/MM/dd", CultureInfo.InvariantCulture); }
                        catch { requestDateTime = DateTime.Parse(StartRequestDate); }
                    }
                }
                //var Date = StartRequestDate.Replace('-', '/');

                //DateTime result = DateTime.ParseExact(Date, "dd/MM/yyyy", null).AddMonths(Duration);
                //return Json(result.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture), JsonRequestBehavior.AllowGet);
                DateTime result = requestDateTime.AddMonths(Duration);
                return Json(result.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture), JsonRequestBehavior.AllowGet);

            }

            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Async_Save(IEnumerable<HttpPostedFileBase> FileName)
        {
            // The Name of the Upload component is "files"
            if (FileName != null)
            {
                foreach (var file in FileName)
                {
                    if (_Request != null)
                    {
                        if (!Directory.Exists(Server.MapPath("~/UploadedFiles"))) Directory.CreateDirectory(Server.MapPath("~/UploadedFiles"));
                        if (!Directory.Exists(Server.MapPath("~/UploadedFiles/Requests"))) Directory.CreateDirectory(Server.MapPath("~/UploadedFiles/Requests"));
                        if (!Directory.Exists(Server.MapPath("~/UploadedFiles/Requests/" + _Request.RequestCode))) Directory.CreateDirectory(Server.MapPath("~/UploadedFiles/Requests/" + _Request.RequestCode));

                        var fileName = Path.GetFileName(file.FileName);
                        var physicalPath = Path.Combine(Server.MapPath("~/UploadedFiles/Requests/" + _Request.RequestCode), fileName);
                        _Request.RequestFileName = fileName;
                        requestService.Update(_Request);

                        // The files are not actually saved in this demo
                        file.SaveAs(physicalPath);
                    }
                }
            }

            // Return an empty string to signify success
            return Content("");
        }

        public ActionResult Async_Remove(string[] fileNames)
        {
            // The parameter of the Remove action must be called "fileNames"

            if (fileNames != null)
            {
                foreach (var fullName in fileNames)
                {
                    var fileName = Path.GetFileName(fullName);
                    var physicalPath = Path.Combine(Server.MapPath("~/App_Data"), fileName);

                    // TODO: Verify user permissions

                    if (System.IO.File.Exists(physicalPath))
                    {
                        // The files are not actually removed in this demo
                        // System.IO.File.Delete(physicalPath);
                    }
                }
            }

            // Return an empty string to signify success
            return Content("");
        }
        public ActionResult Async_Save_Details(IEnumerable<HttpPostedFileBase> FileName)
        {
            // The Name of the Upload component is "files"
            if (FileName != null)
            {
                foreach (var file in FileName)
                {
                    if (_Request != null)
                    {
                        if (!Directory.Exists(Server.MapPath("~/UploadedFiles"))) Directory.CreateDirectory(Server.MapPath("~/UploadedFiles"));
                        if (!Directory.Exists(Server.MapPath("~/UploadedFiles/RequestDetails"))) Directory.CreateDirectory(Server.MapPath("~/UploadedFiles/RequestDetails"));
                        if (!Directory.Exists(Server.MapPath("~/UploadedFiles/RequestDetails/" + _Request.RequestCode))) Directory.CreateDirectory(Server.MapPath("~/UploadedFiles/RequestDetails/" + _Request.RequestCode));

                        var fileName = Path.GetFileName(file.FileName);
                        var physicalPath = Path.Combine(Server.MapPath("~/UploadedFiles/RequestDetails/" + _Request.RequestCode), fileName);

                        _Request.RequestDetailsFileName = fileName;
                        requestService.Update(_Request);

                        // The files are not actually saved in this demo
                        file.SaveAs(physicalPath);
                    }
                }
            }

            // Return an empty string to signify success
            return Content("");
        }
        static string ResultMsg = "";
        public string getstatus()// Upload Excel
        {
            return ResultMsg;
        }
        public ActionResult Async_Save_RequestExcel(IEnumerable<HttpPostedFileBase> RequestExcelFileName)
        {
            // The Name of the Upload component is "files"
            if (RequestExcelFileName != null)
            {
                foreach (var file in RequestExcelFileName)
                {
                    if (_Request != null)
                    {

                        if (!Directory.Exists(Server.MapPath("~/UploadedFiles"))) Directory.CreateDirectory(Server.MapPath("~/UploadedFiles"));
                        if (!Directory.Exists(Server.MapPath("~/UploadedFiles/ExcelFile"))) Directory.CreateDirectory(Server.MapPath("~/UploadedFiles/ExcelFile"));

                        var fileName = Path.GetFileName(file.FileName);
                        var physicalPath = Path.Combine(Server.MapPath("~/UploadedFiles/ExcelFile/"), fileName);
                        file.SaveAs(physicalPath);


                        if (RequestExcelFileName.FirstOrDefault().ContentType == "application/vnd.ms-excel" || RequestExcelFileName.FirstOrDefault().ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                        {
                            string targetpath = Server.MapPath("~/UploadedFiles/ExcelFile/");

                            string pathToExcelFile = targetpath + fileName;
                            var connectionString = "";
                            if (fileName.ToLower().EndsWith(".xls"))
                            {
                                connectionString = ConfigurationManager.ConnectionStrings["ExcelXlsConnectionString"].ConnectionString;
                            }
                            else if (fileName.ToLower().EndsWith(".xlsx"))
                            {
                                connectionString = ConfigurationManager.ConnectionStrings["ExcelXlsxConnectionString"].ConnectionString;
                            }

                            connectionString = string.Format(connectionString, pathToExcelFile);
                            using (OleDbConnection Excel_Con = new OleDbConnection(connectionString))
                            {
                                try
                                {
                                    Excel_Con.Open();
                                }
                                catch (Exception ex)
                                {
                                    ResultMsg = ex.Message;
                                }
                                string sheet1 = Excel_Con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();
                                DataTable ExcelDT = new DataTable();
                                using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [" + sheet1 + "]", Excel_Con))
                                {
                                    oda.Fill(ExcelDT);
                                }
                                Excel_Con.Close();


                                if (ExcelDT.Rows.Count <= 0)
                                {
                                    ResultMsg = " لا يوجد بيانات فى ملف الإكسل ";
                                }
                                else
                                {
                                    int i = 0;
                                    foreach (var headers in ExcelDT.Columns)
                                    {
                                        if (headers.ToString() == "إسم البند" && i == 0) { }
                                        else if (headers.ToString() == "الوصف" && i == 1) { }
                                        else if (headers.ToString() == "الوحدة" && i == 2) { }
                                        else if (headers.ToString() == "الكمية المقدرة" && i == 3) { }
                                        else if (headers.ToString() == "سعر الوحدة بالجنية" && i == 4) { }
                                        else if (headers.ToString() == "الفرع المختص" && i == 5) { }
                                        else if (headers.ToString() == "فئة البند" && i == 6) { }
                                        else { return Content("يوجد خطأ في مسميات النموذج"); }

                                        i++;
                                    }
                                    var itemClass = "";
                                    int inc = 0;
                                    RequestDetails obj = new RequestDetails();
                                    foreach (DataRow dtRow in ExcelDT.Rows)
                                    {
                                        // On all tables' columns
                                        foreach (DataColumn dc in ExcelDT.Columns)
                                        {
                                            var field1 = dtRow[dc].ToString();
                                            if (inc == 0) { obj.RequestDetailSerial = field1; }
                                            else if (inc == 1) { obj.RequestDetailName = field1; }
                                            else if (inc == 2)
                                            {
                                                var unit = db.Units.Where(x => x.UnitName.Contains(field1)).FirstOrDefault();
                                                obj.UnitId = unit.UnitId;
                                            }
                                            else if (inc == 3) { obj.Qty = long.Parse(field1); }
                                            else if (inc == 4) { obj.UnitPrice = decimal.Parse(field1); }
                                            else if (inc == 5)
                                            {
                                                var department = db2.Departments.Where(x => x.DepartmentName.Contains(field1)).FirstOrDefault();
                                                obj.DepartmentId = department.DepartmentId;
                                            }
                                            else if (inc == 6)
                                            {
                                                itemClass = field1;
                                            }

                                            inc++;
                                        }
                                        long requestDetailID = requestService.CreateDetails(obj, _Request);
                                        if (itemClass != "")
                                        {
                                            var entity = new RequestDetailClass();

                                            entity.RequestDetailId = requestDetailID;
                                            entity.ItemClassId = db.ItemClass.Where(x => x.ItemClassName == itemClass).FirstOrDefault().ItemClassId;

                                            db.RequestDetailClass.Add(entity);
                                            db.SaveChanges();
                                            itemClass = "";
                                        }
                                        obj = new RequestDetails();
                                        inc = 0;
                                    }
                                }
                            }
                            ResultMsg = "done";

                            if (System.IO.File.Exists(physicalPath))
                            {

                                System.IO.File.Delete(physicalPath);
                            }
                        }
                        else
                        {
                            ResultMsg = "يجب إختيار ملف إكسل من نوع (xlsx) أو (xls)";
                            return Json(new { responseText = "يجب إختيار ملف إكسل من نوع (xlsx) أو (xls)" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
            }

            // Return an empty string to signify success
            return Content("");
        }
        public ActionResult Async_SaveLog(IEnumerable<HttpPostedFileBase> FileName)
        {
            // The Name of the Upload component is "files"
            if (FileName != null)
            {
                foreach (var file in FileName)
                {
                    if (Session["RequestLogID"] != null)
                    {
                        long requestLogID = (long)Session["RequestLogID"];
                        var RequestLog = db.RequestLog.FirstOrDefault(x => x.RequestLogId == requestLogID);
                        string code = "";
                        if (RequestLog != null)
                        {
                            var request = new Requests();
                            var requestDetails = new RequestDetails();
                            if (RequestLog.Type == 1)
                            {
                                request = db.Requests.FirstOrDefault(x => x.RequestId == RequestLog.RequestId);
                                code = request.RequestCode;
                            }
                            else
                            {
                                requestDetails = db.RequestDetails.FirstOrDefault(x => x.RequestDetailId == RequestLog.RequestDetailId);
                                code = requestDetails.RequestDetailCode;
                            }
                            if (!Directory.Exists(Server.MapPath("~/UploadedFiles"))) Directory.CreateDirectory(Server.MapPath("~/UploadedFiles"));
                            if (!Directory.Exists(Server.MapPath("~/UploadedFiles/RequestLog"))) Directory.CreateDirectory(Server.MapPath("~/UploadedFiles/RequestLog"));
                            if (!Directory.Exists(Server.MapPath("~/UploadedFiles/RequestLog/" + code))) Directory.CreateDirectory(Server.MapPath("~/UploadedFiles/RequestLog/" + code));

                            var fileName = Path.GetFileName(file.FileName);
                            var physicalPath = Path.Combine(Server.MapPath("~/UploadedFiles/RequestLog/" + code), fileName);

                            var entity = db.RequestLog.FirstOrDefault(x => x.RequestLogId == requestLogID);
                            entity.LogFileName = fileName;
                            db.RequestLog.Attach(entity);
                            db.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            db.SaveChanges();


                            // The files are not actually saved in this demo
                            file.SaveAs(physicalPath);
                        }
                    }
                }
            }

            // Return an empty string to signify success
            return Content("");
        }
        public ActionResult Async_Remove_Details(string[] fileNames)
        {
            // The parameter of the Remove action must be called "fileNames"

            if (fileNames != null)
            {
                foreach (var fullName in fileNames)
                {
                    var fileName = Path.GetFileName(fullName);
                    var physicalPath = Path.Combine(Server.MapPath("~/App_Data"), fileName);

                    // TODO: Verify user permissions

                    if (System.IO.File.Exists(physicalPath))
                    {
                        // The files are not actually removed in this demo
                        // System.IO.File.Delete(physicalPath);
                    }
                }
            }

            // Return an empty string to signify success
            return Content("");
        }
        public ActionResult SaveRequest(Requests Request, string RequestDate)
        {
            if (Request != null && Request.RequestId == 0)
                try
                {
                    DateTime? requestDateTime;
                    try { requestDateTime = DateTime.ParseExact(RequestDate, "dd-MM-yyyy", CultureInfo.InvariantCulture); }
                    catch
                    {
                        try { requestDateTime = DateTime.ParseExact(RequestDate, "dd/MM/yyyy", CultureInfo.InvariantCulture); }
                        catch
                        {
                            try { requestDateTime = DateTime.ParseExact(RequestDate, "yyyy/MM/dd", CultureInfo.InvariantCulture); }
                            catch { requestDateTime = DateTime.Parse(RequestDate); }
                        }
                    }
                    Request.RequestDate = requestDateTime;
                    if (requestService.Create(Request) == -1)
                    {
                        return Json("رقم أمر الإسناد موجود مسبقا", JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json("تمت الإضافة بنجاح", JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception ex) { return Json(ex.Message, JsonRequestBehavior.AllowGet); }
            else
            {
                DateTime? requestDateTime;
                try { requestDateTime = DateTime.ParseExact(RequestDate, "dd-MM-yyyy", CultureInfo.InvariantCulture); }
                catch
                {
                    try { requestDateTime = DateTime.ParseExact(RequestDate, "dd/MM/yyyy", CultureInfo.InvariantCulture); }
                    catch
                    {
                        try { requestDateTime = DateTime.ParseExact(RequestDate, "yyyy/MM/dd", CultureInfo.InvariantCulture); }
                        catch { requestDateTime = DateTime.Parse(RequestDate); }
                    }
                }
                Request.RequestStartDate = requestDateTime;
                requestService.Update(Request);
            }
            var AutoNo = db.Requests.Max(x => x.AutoNo).Value;
            AutoNo++;
            _Request = Request;
            return Json("تمت التعديل بنجاح", JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveRequestLog(Requests Request)
        {
            if (Request != null)
                try
                {
                    var request = db.Requests.Where(x => x.RequestId == Request.RequestId).FirstOrDefault();
                    var entity = new RequestLog();
                    entity.DateAdd = DateTime.Now;
                    entity.UserIdAdd = (long)Session["UserID"];
                    entity.RequestDuration = request.RequestDuration;
                    entity.NewRequestDuration = Request.RequestDuration;
                    entity.RequestId = request.RequestId;
                    entity.Type = 1;
                    db.RequestLog.Add(entity);
                    db.SaveChanges();
                    Session["RequestLogID"] = entity.RequestLogId;
                    requestService.Update(Request);
                }
                catch (Exception ex) { return Json(ex.Message, JsonRequestBehavior.AllowGet); }


            var AutoNo = db.Requests.Max(x => x.AutoNo).Value;
            AutoNo++;
            _Request = Request;
            return Json("تمت التعديل بنجاح", JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllRequests()
        {
            using (var db = GetContext())
            {
                var requests = db.Requests.Where(x => x.IsLocked == false).ToList();
                return Json(requests.ToList(), JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetAllRequestsForCompanyUsers(string CompanyID)
        {
            using (var db = GetContext())
            {
                var requests = db.Requests.Where(x => x.IsLocked == false && x.CompanyId == long.Parse(CompanyID)).ToList();
                return Json(requests.ToList(), JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetRequestDataByID(string RequestNo, string requestDate)
        {
            DateTime? requestDateTime = DateTime.Now;
            using (var db = GetContext())
            {
                if (requestDate != "")
                {
                    try { requestDateTime = DateTime.ParseExact(requestDate, "dd-MM-yyyy", CultureInfo.InvariantCulture); }
                    catch
                    {
                        try { requestDateTime = DateTime.ParseExact(requestDate, "dd/MM/yyyy", CultureInfo.InvariantCulture); }
                        catch
                        {
                            try { requestDateTime = DateTime.ParseExact(requestDate, "yyyy/MM/dd", CultureInfo.InvariantCulture); }
                            catch
                            {
                                requestDateTime = DateTime.Parse(requestDate);
                            }
                        }
                    }
                }
            }
            var result = new Requests();
            if (requestDate != "")

                result = db.Requests.Where(x => x.IsLocked == false && x.RequestNo == RequestNo && x.RequestDate == requestDateTime).ToList().FirstOrDefault();

            else
                result = db.Requests.Where(x => x.IsLocked == false && x.RequestNo == RequestNo).ToList().FirstOrDefault();
            if (result != null)
            {
                Session["RequestIDForNote"] = result.RequestId;
                var comp = db.Companies.Where(x => x.CompanyId == result.CompanyId).FirstOrDefault();
                Models.Refactored.Requests requests1 = new Models.Refactored.Requests()
                {
                    RequestAmount = result.RequestAmount,
                    RequestDateString = string.Format("{0:dd/MM/yyyy}", result.RequestDate),
                    RequestCode = result.RequestCode,
                    Notes = result.Notes,
                    RequestNo = result.RequestNo,
                    RequestDuration = result.RequestDuration,
                    RequestId = result.RequestId,
                    RequestName = result.RequestName,
                    IsLocked = result.IsLocked,
                    Company = comp.CompanyName,
                    FileName = result.RequestFileName,
                    FileNameDetails = result.RequestDetailsFileName,
                    CompanyId = result.CompanyId
                };
                if (result.RequestEndDate != null)
                    requests1.RequestEndDateString = string.Format("{0:dd/MM/yyyy}", result.RequestEndDate);
                if (result.RequestStartDate != null)
                    requests1.RequestStartDateString = string.Format("{0:dd/MM/yyyy}", result.RequestStartDate);
                _Request = result;
                return Json(requests1, JsonRequestBehavior.AllowGet);
            }
            WorkOrderController._WorkOrder = null;
            return Json(-1, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetRequestDataByIDForCompany(long RequestId)
        {

            var result = db.Requests.Where(x => x.IsLocked == false && x.RequestId == RequestId).ToList().FirstOrDefault();
            if (result != null)
            {
                Session["RequestIDForNote"] = result.RequestId;
                var comp = db.Companies.Where(x => x.CompanyId == result.CompanyId).FirstOrDefault();
                Models.Refactored.Requests requests1 = new Models.Refactored.Requests()
                {
                    RequestAmount = result.RequestAmount,
                    RequestDateString = string.Format("{0:dd/MM/yyyy}", result.RequestDate),
                    RequestCode = result.RequestCode,
                    Notes = result.Notes,
                    RequestNo = result.RequestNo,
                    RequestDuration = result.RequestDuration,
                    RequestId = result.RequestId,
                    RequestName = result.RequestName,
                    IsLocked = result.IsLocked,
                    Company = comp.CompanyName,
                    FileName = result.RequestFileName,
                    FileNameDetails = result.RequestDetailsFileName,
                    CompanyId = result.CompanyId
                };
                if (result.RequestEndDate != null)
                    requests1.RequestEndDateString = string.Format("{0:dd/MM/yyyy}", result.RequestEndDate);
                if (result.RequestStartDate != null)
                    requests1.RequestStartDateString = string.Format("{0:dd/MM/yyyy}", result.RequestStartDate);
                _Request = result;
                return Json(requests1, JsonRequestBehavior.AllowGet);
            }
            WorkOrderController._WorkOrder = null;
            return Json(-1, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetRequestDataByUserID(string RequestNo, string requestDate)
        {
            int userID = int.Parse(Session["UserID"].ToString());

            DateTime? requestDateTime;
            using (var db = GetContext())
            {
                try { requestDateTime = DateTime.ParseExact(requestDate, "dd-MM-yyyy", CultureInfo.InvariantCulture); }
                catch
                {
                    try { requestDateTime = DateTime.ParseExact(requestDate, "dd/MM/yyyy", CultureInfo.InvariantCulture); }
                    catch
                    {
                        try { requestDateTime = DateTime.ParseExact(requestDate, "yyyy/MM/dd", CultureInfo.InvariantCulture); }
                        catch
                        {
                            requestDateTime = DateTime.Parse(requestDate);
                        }
                    }
                }
                var ListRequest = db.UserRequests.Where(x => x.UserId == userID).Select(x => x.RequestId).ToList();
                var result = db.Requests.Where(x => x.IsLocked == false && ListRequest.Contains(x.RequestId) && x.RequestNo == RequestNo && x.RequestDate == requestDateTime).ToList().FirstOrDefault();
                if (result != null)
                {
                    Session["RequestIDForNote"] = result.RequestId;
                    var comp = db.Companies.Where(x => x.CompanyId == result.CompanyId).FirstOrDefault();
                    Models.Refactored.Requests requests1 = new Models.Refactored.Requests()
                    {
                        RequestAmount = result.RequestAmount,
                        RequestDateString = string.Format("{0:dd/MM/yyyy}", result.RequestDate),
                        RequestCode = result.RequestCode,
                        Notes = result.Notes,
                        RequestNo = result.RequestNo,
                        RequestDuration = result.RequestDuration,
                        RequestId = result.RequestId,
                        RequestName = result.RequestName,
                        IsLocked = result.IsLocked,
                        Company = comp.CompanyName,
                        FileName = result.RequestFileName,
                        FileNameDetails = result.RequestDetailsFileName,
                        CompanyId = result.CompanyId
                    };
                    if (result.RequestEndDate != null)
                        requests1.RequestEndDateString = string.Format("{0:dd/MM/yyyy}", result.RequestEndDate);
                    if (result.RequestStartDate != null)
                        requests1.RequestStartDateString = string.Format("{0:dd/MM/yyyy}", result.RequestStartDate);
                    _Request = result;
                    WorkOrderController._WorkOrder = null;
                    return Json(requests1, JsonRequestBehavior.AllowGet);
                }
                WorkOrderController._WorkOrder = null;
                return Json(-1, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetRequestDataByTechnical(string RequestNo, string requestDate)
        {

            DateTime? requestDateTime;
            using (var db = GetContext())
            {
                try { requestDateTime = DateTime.ParseExact(requestDate, "dd-MM-yyyy", CultureInfo.InvariantCulture); }
                catch
                {
                    try { requestDateTime = DateTime.ParseExact(requestDate, "dd/MM/yyyy", CultureInfo.InvariantCulture); }
                    catch
                    {
                        try { requestDateTime = DateTime.ParseExact(requestDate, "yyyy/MM/dd", CultureInfo.InvariantCulture); }
                        catch
                        {
                            requestDateTime = DateTime.Parse(requestDate);
                        }
                    }
                }
                var result = db.Requests.Where(x => x.IsLocked == false && x.RequestNo == RequestNo && x.RequestDate == requestDateTime).ToList().FirstOrDefault();
                if (result != null)
                {
                    Session["RequestIDForNote"] = result.RequestId;
                    var comp = db.Companies.Where(x => x.CompanyId == result.CompanyId).FirstOrDefault();
                    Models.Refactored.Requests requests1 = new Models.Refactored.Requests()
                    {
                        RequestAmount = result.RequestAmount,
                        RequestDateString = string.Format("{0:dd/MM/yyyy}", result.RequestDate),
                        RequestCode = result.RequestCode,
                        Notes = result.Notes,
                        RequestNo = result.RequestNo,
                        RequestDuration = result.RequestDuration,
                        RequestId = result.RequestId,
                        RequestName = result.RequestName,
                        IsLocked = result.IsLocked,
                        Company = comp.CompanyName,
                        FileName = result.RequestFileName,
                        FileNameDetails = result.RequestDetailsFileName,
                        CompanyId = result.CompanyId
                    };
                    if (result.RequestEndDate != null)
                        requests1.RequestEndDateString = string.Format("{0:dd/MM/yyyy}", result.RequestEndDate);
                    if (result.RequestStartDate != null)
                        requests1.RequestStartDateString = string.Format("{0:dd/MM/yyyy}", result.RequestStartDate);
                    _Request = result;
                    return Json(requests1, JsonRequestBehavior.AllowGet);
                }
                WorkOrderController._WorkOrder = null;
                return Json(-1, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetRequestDataByCompanyUserID(string RequestNo, string requestDate)
        {
            CompanyUsers user = (CompanyUsers)Session["User"];
            var requestIds = db.CompanyUsers.Where(x => x.UserName == user.UserName).Select(x => x.RequestId).ToList();
            DateTime? requestDateTime = DateTime.Now;
            using (var db = GetContext())
            {
                try { requestDateTime = DateTime.ParseExact(requestDate, "dd-MM-yyyy", CultureInfo.InvariantCulture); }
                catch
                {
                    try { requestDateTime = DateTime.ParseExact(requestDate, "dd/MM/yyyy", CultureInfo.InvariantCulture); }
                    catch
                    {
                        try { requestDateTime = DateTime.ParseExact(requestDate, "yyyy/MM/dd", CultureInfo.InvariantCulture); }
                        catch
                        {
                            if (requestDate != "")
                                requestDateTime = DateTime.Parse(requestDate);
                        }
                    }
                }

                var result = db.Requests.Where(x => x.IsLocked == false && requestIds.Contains(x.RequestId) && x.RequestNo == RequestNo && x.RequestDate == requestDateTime).ToList().FirstOrDefault();
                if (result != null)
                {
                    var comp = db.Companies.Where(x => x.CompanyId == result.CompanyId).FirstOrDefault();
                    Models.Refactored.Requests requests1 = new Models.Refactored.Requests()
                    {
                        RequestAmount = result.RequestAmount,
                        RequestDateString = string.Format("{0:dd/MM/yyyy}", result.RequestDate),
                        RequestCode = result.RequestCode,
                        Notes = result.Notes,
                        RequestNo = result.RequestNo,
                        RequestDuration = result.RequestDuration,
                        RequestId = result.RequestId,
                        RequestName = result.RequestName,
                        IsLocked = result.IsLocked,
                        Company = comp.CompanyName,
                        FileName = result.RequestFileName,
                        FileNameDetails = result.RequestDetailsFileName,
                        CompanyId = result.CompanyId
                    };
                    if (result.RequestEndDate != null)
                        requests1.RequestEndDateString = string.Format("{0:dd/MM/yyyy}", result.RequestEndDate);
                    if (result.RequestStartDate != null)
                        requests1.RequestStartDateString = string.Format("{0:dd/MM/yyyy}", result.RequestStartDate);
                    _Request = result;
                    return Json(requests1, JsonRequestBehavior.AllowGet);
                }
                WorkOrderController._WorkOrder = null;
                return Json(-1, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetRequestByIDForWorkOrderData(long? RequestId)
        {
            using (var db = GetContext())
            {
                WorkOrders orders = db.WorkOrders.ToList().Where(x => x.IsSubmit != true && x.RequestId == RequestId).OrderByDescending(x => x.WorkOrderId).FirstOrDefault();
                WorkOrderController._WorkOrder = orders;
                Models.Refactored.WorkOrders workOrder = new Models.Refactored.WorkOrders();
                if (orders != null)
                {
                    if (orders.WorkOrderEndDate == null) orders.WorkOrderEndDate = DateTime.Now;
                    if (orders.WorkOrderStartDate == null) orders.WorkOrderStartDate = DateTime.Now;
                    workOrder = new Models.Refactored.WorkOrders()
                    {
                        WorkOrderId = orders.WorkOrderId,
                        WorkOrderStartDateString = string.Format("{0:dd/MM/yyyy}", orders.WorkOrderStartDate),
                        WorkOrderEndDateString = string.Format("{0:dd/MM/yyyy}", orders.WorkOrderEndDate),
                        FileName = orders.FileName,
                        FileNameApproved = orders.FileNameApproved,
                        Notes = orders.Notes,
                        WorkOrderCode = orders.WorkOrderCode
                    };

                    return Json(workOrder, JsonRequestBehavior.AllowGet);
                }
                var wo = db.WorkOrders.Where(x => x.RequestId == RequestId).OrderByDescending(x => x.WorkOrderId).FirstOrDefault();
                if (wo != null)
                {
                    if (wo.WorkOrderEndDate != null)
                        workOrder = new Models.Refactored.WorkOrders() { WorkOrderStartDateString = string.Format("{0:dd/MM/yyyy}", wo.WorkOrderEndDate.Value.AddDays(1)) };
                    return Json(workOrder, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var request = db.Requests.Where(x => x.RequestId == RequestId).FirstOrDefault();
                    if (request.RequestStartDate == null) return Json(-2, JsonRequestBehavior.AllowGet);

                    workOrder = new Models.Refactored.WorkOrders() { WorkOrderStartDateString = string.Format("{0:dd/MM/yyyy}", request.RequestStartDate) };
                    return Json(workOrder, JsonRequestBehavior.AllowGet);
                }

                //return Json(-1, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetRequestByIDForWorkOrderDataForCompany(long? RequestId)
        {
            using (var db = GetContext())
            {
                WorkOrders orders = db.WorkOrders.ToList().Where(x => x.IsSubmit != true && x.RequestId == RequestId).OrderByDescending(x => x.WorkOrderId).FirstOrDefault();
                WorkOrderController._WorkOrder = orders;
                Models.Refactored.WorkOrders workOrder = new Models.Refactored.WorkOrders();
                if (orders != null)
                {
                    if (orders.WorkOrderEndDate == null) orders.WorkOrderEndDate = DateTime.Now;
                    if (orders.WorkOrderStartDate == null) orders.WorkOrderStartDate = DateTime.Now;
                    workOrder = new Models.Refactored.WorkOrders()
                    {
                        WorkOrderId = orders.WorkOrderId,
                        WorkOrderStartDateString = orders.WorkOrderStartDate.Value.ToShortDateString(),
                        WorkOrderEndDateString = orders.WorkOrderEndDate.Value.ToShortDateString(),
                        FileName = orders.FileName,
                        FileNameApproved = orders.FileNameApproved,
                        Notes = orders.Notes,
                        WorkOrderCode = orders.WorkOrderCode
                    };

                    return Json(workOrder, JsonRequestBehavior.AllowGet);
                }
                var wo = db.WorkOrders.Where(x => x.RequestId == RequestId).OrderByDescending(x => x.WorkOrderId).FirstOrDefault();
                if (wo != null)
                {
                    workOrder = new Models.Refactored.WorkOrders() { WorkOrderStartDateString = wo.WorkOrderEndDate.Value.AddDays(1).ToShortDateString() };
                    return Json(workOrder, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var request = db.Requests.Where(x => x.RequestId == RequestId).FirstOrDefault();
                    workOrder = new Models.Refactored.WorkOrders() { WorkOrderStartDateString = request.RequestStartDate.Value.ToShortDateString() };
                    return Json(workOrder, JsonRequestBehavior.AllowGet);
                }

               // return Json(-1, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetRequestByIDForWorkOrderDataForWorkOrder(long? RequestId)
        {
            using (var db = GetContext())
            {
                WorkOrders orders = db.WorkOrders.ToList().Where(x => x.IsSubmit != true && x.RequestId == RequestId).OrderByDescending(x => x.WorkOrderId).FirstOrDefault();
                WorkOrderController._WorkOrder = orders;
                if (orders != null)
                {
                    if (orders.WorkOrderEndDate == null) orders.WorkOrderEndDate = DateTime.Now;
                    if (orders.WorkOrderStartDate == null) orders.WorkOrderStartDate = DateTime.Now;
                    Models.Refactored.WorkOrders workOrder = new Models.Refactored.WorkOrders()
                    {
                        WorkOrderId = orders.WorkOrderId,
                        WorkOrderStartDateString = orders.WorkOrderStartDate.Value.ToShortDateString(),
                        WorkOrderEndDateString = orders.WorkOrderEndDate.Value.ToShortDateString(),
                        FileName = orders.FileName,
                        FileNameApproved = orders.FileNameApproved,
                        Notes = orders.Notes,
                        WorkOrderCode = orders.WorkOrderCode
                    };
                    return Json(workOrder, JsonRequestBehavior.AllowGet);
                }

                return Json(-1, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetRequestData(long? RequestId)
        {
            Session["RequestIDForNote"] = RequestId;
            using (var db = GetContext())
            {
                var result = db.Requests.Where(x => x.IsLocked == false && x.RequestId == RequestId).ToList().FirstOrDefault();

                //if (wo != null)
                //{

                //    WorkOrderStartDate = wo.WorkOrderEndDate.Value.AddDays(1).ToShortDateString();
                //}
                //else
                //{

                //    WorkOrderStartDate = result.RequestStartDate.Value.ToShortDateString();
                //}
                if (result != null)
                {
                    var comp = db.Companies.Where(x => x.CompanyId == result.CompanyId).FirstOrDefault();
                    Models.Refactored.Requests requests1 = new Models.Refactored.Requests()
                    {
                        RequestAmount = result.RequestAmount,
                        RequestDateString = string.Format("{0:dd/MM/yyyy}", result.RequestDate),
                        RequestCode = result.RequestCode,
                        Notes = result.Notes,
                        RequestNo = result.RequestNo,
                        RequestDuration = result.RequestDuration,
                        RequestId = result.RequestId,
                        RequestName = result.RequestName,

                        IsLocked = result.IsLocked,
                        Company = comp.CompanyName,
                        FileName = result.RequestFileName,
                        FileNameDetails = result.RequestDetailsFileName,
                        CompanyId = result.CompanyId
                    };
                    if (result.RequestEndDate != null)
                        requests1.RequestEndDateString = string.Format("{0:dd/MM/yyyy}", result.RequestEndDate);
                    if (result.RequestStartDate != null)
                        requests1.RequestStartDateString = string.Format("{0:dd/MM/yyyy}", result.RequestStartDate);
                    _Request = result;
                    return Json(requests1, JsonRequestBehavior.AllowGet);
                }
                return Json(-1, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetAllRequestsByUserCompanyID()
        {
            CompanyUsers user = (CompanyUsers)Session["User"];
            var requestIds = db.CompanyUsers.Where(x => x.UserName == user.UserName).Select(x => x.RequestId).ToList();
            var requests = db.Requests.Where(x => requestIds.Contains(x.RequestId)).ToList();
            if (requests != null) return Json(requests, JsonRequestBehavior.AllowGet);
            else
                return Json(0, JsonRequestBehavior.AllowGet);
        }
        [AuthorizedAction]
        public ActionResult DeliverRequest()
        {
            return View();
        }
        [AuthorizedAction]
        public ActionResult RequestDetails()
        {
            long? AutoNo;
            if (db.RequestDetails.Max(x => x.AutoNo) == null) { AutoNo = 0; }
            else { AutoNo = db.RequestDetails.Max(x => x.AutoNo).Value; }


            AutoNo++;

            ViewBag.RequestDetailsCode = PublicVariables.RequestDetailsPrefixCode + AutoNo;
            _Request = null;
            return View();
        }
        public ActionResult GetRequestDetailList([DataSourceRequest] DataSourceRequest request)
        {
            if (_Request == null) { return Json(1, JsonRequestBehavior.AllowGet); }
            var result = requestService.ReadDetails(_Request.RequestId);
            Session["RequestDetailList"] = result;
            return Json(result.ToDataSourceResult(request));
        }
        public ActionResult SaveRequestDetails(Requests request, RequestDetails requestDetails, List<int> ItemClasses)
        {
            if (requestDetails != null && requestDetails.RequestDetailId == 0 && ModelState.IsValid)
            {
                long requestDetailsID = requestService.CreateDetails(requestDetails, request);

                var result = db.RequestDetailClass.Where(cp => cp.RequestDetailId == requestDetailsID).ToList();

                foreach (var rdc in result)
                {
                    var entity = db.RequestDetailClass.First(s => s.RequestDetailsClassId == rdc.RequestDetailsClassId);
                    db.Remove(entity);

                    db.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                    db.SaveChanges();
                }

                foreach (var item in ItemClasses)
                {
                    var entity = new RequestDetailClass();

                    entity.RequestDetailId = requestDetailsID;
                    entity.ItemClassId = item;

                    db.RequestDetailClass.Add(entity);
                    db.SaveChanges();
                }
            }
            else
            {
                requestService.UpdateDetails(requestDetails);

                var result = db.RequestDetailClass.Where(cp => cp.RequestDetailId == requestDetails.RequestDetailId).ToList();

                foreach (var rdc in result)
                {
                    var entity = db.RequestDetailClass.First(s => s.RequestDetailsClassId == rdc.RequestDetailsClassId);
                    db.RequestDetailClass.Add(entity);
                    db.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                    db.SaveChanges();
                }

                foreach (var item in ItemClasses)
                {
                    var entity = new RequestDetailClass();

                    entity.RequestDetailId = requestDetails.RequestDetailId;
                    entity.ItemClassId = item;

                    db.RequestDetailClass.Add(entity);
                    db.SaveChanges();
                }

            }
            var AutoNo = db.Requests.Max(x => x.AutoNo).Value;
            AutoNo++;
            _RequestDetails = requestDetails;
            return Json(AutoNo);
        }
        public ActionResult SaveRequestDetailsLog(RequestDetails requestDetails)
        {
            try
            {
                var rd = db.RequestDetails.Where(x => x.RequestDetailId == requestDetails.RequestDetailId).FirstOrDefault();
                var entity = new RequestLog();
                entity.DateAdd = DateTime.Now;
                entity.UserIdAdd = (long)Session["UserID"];
                entity.RequestDetailId = requestDetails.RequestDetailId;
                entity.Qty = rd.Qty;
                entity.NewQty = requestDetails.Qty;
                entity.NewUnitPrice = requestDetails.UnitPrice;
                entity.UnitPrice = rd.UnitPrice;
                entity.Type = 2;
                db.RequestLog.Add(entity);
                db.SaveChanges();
                requestService.UpdateDetails(requestDetails);
                Session["RequestLogID"] = entity.RequestLogId;
            }
            catch (Exception ex) { return Json(ex.Message, JsonRequestBehavior.AllowGet); }

            var AutoNo = db.Requests.Max(x => x.AutoNo).Value;
            AutoNo++;
            _RequestDetails = requestDetails;
            return Json(AutoNo);
        }
        public ActionResult RequestDetailForCompany()
        {
            _Request = null;
            //CompanyUsers companyuser = (CompanyUsers)Session["User"];
            //var request = db.Requests.Where(x => x.CompanyId == companyuser.CompanyId && x.RequestId == companyuser.RequestId).FirstOrDefault();

            //_Request = request;
            return View();
        }
        public ActionResult GetRequestDetailForCompanyList([DataSourceRequest] DataSourceRequest request)
        {
            if (_Request == null) { return Json(1, JsonRequestBehavior.AllowGet); }
            return Json(requestService.ReadDetails(_Request.RequestId).ToDataSourceResult(request));
        }
    }
}