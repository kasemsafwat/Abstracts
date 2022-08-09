using Abstracts.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Abstracts.Models.MasterLoginModel;

namespace Abstracts.Controllers
{
    public class ReportController : Controller
    {
        readonly AbstractsDBContext db = new AbstractsDBContext();
        private readonly ParadiseMasterLogInDBContext db2 = new ParadiseMasterLogInDBContext();
        // GET: Report
        static int reportID = 1;
        static int? _ItemClassId = -1;
        static DateTime? _RequestStartDate = DateTime.Now;
        static DateTime? _RequestEndDate = DateTime.Now;
        [AuthorizedAction]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetReport([DataSourceRequest] DataSourceRequest request, int? ItemClassId, DateTime? RequestStartDate, DateTime? RequestEndDate)
        {
            List<Models.Refactored.Report> reports = new List<Models.Refactored.Report>();
            Models.Refactored.Report report = new Models.Refactored.Report();
            if (ItemClassId != null)
            {
                _ItemClassId = ItemClassId;
                _RequestStartDate = RequestStartDate;
                _RequestEndDate = RequestEndDate;
            }
            else
            {
                ItemClassId = _ItemClassId;
                RequestStartDate = _RequestStartDate;
                RequestEndDate = _RequestEndDate;
            }
            if (ItemClassId != null && _ItemClassId != null)
            {
                _ItemClassId = ItemClassId;
                _RequestEndDate = RequestEndDate;
                _RequestStartDate = RequestStartDate;
                var RequestDetailClassList = db.RequestDetailClass.Where(x => x.ItemClassId == ItemClassId).Select(x => x.RequestDetailId);

                var requests = db.Requests.Where(x => x.RequestStartDate >= RequestStartDate && x.RequestStartDate <= RequestEndDate).ToList();
                var itemClass = db.ItemClass.Where(x => x.ItemClassId == ItemClassId).FirstOrDefault();
                foreach (var _request in requests)
                {
                    var requestDetails = db.RequestDetails.Where(x => RequestDetailClassList.Contains(x.RequestDetailId) && x.RequestId == _request.RequestId).ToList();
                    foreach (var requestDetail in requestDetails)
                    {
                        var workorderDetails = db.WorkOrderDetails.Where(x => x.RequestDetailId == requestDetail.RequestDetailId).ToList();
                        foreach (var wod in workorderDetails)
                        {
                            var reqDetail = db.RequestDetails.Where(x => x.RequestDetailId == wod.RequestDetailId).FirstOrDefault();
                            var unit = db.Units.Where(x => x.UnitId == reqDetail.UnitId).FirstOrDefault();
                            var req = db.Requests.Where(x => x.RequestId == reqDetail.RequestId).FirstOrDefault();
                            var company = db.Companies.Where(x => x.CompanyId == req.CompanyId).FirstOrDefault();
                            var workorder = db.WorkOrders.Where(x => x.RequestId == req.RequestId).FirstOrDefault();
                            var note = db.WorkOrderNotes.Where(x => x.WorkOrderNoteId == wod.RequestNoteId).FirstOrDefault();
                            report.RequestCode = req.RequestCode;
                            report.RequestName = req.RequestName;
                            report.RequestDate = req.RequestDate.Value.ToShortDateString();
                            report.RequestDuration = req.RequestDuration;
                            report.RequestStartDate = req.RequestStartDate.Value.ToShortDateString();
                            report.RequestEndDate = req.RequestEndDate.Value.ToShortDateString();
                            report.CompanyCode = company.CompanyCode;
                            report.CompanyName = company.CompanyName;
                            report.RequestDetailSerial = reqDetail.RequestDetailSerial;
                            report.RequestDetailName = reqDetail.RequestDetailName;
                            report.RequestDetailCode = reqDetail.RequestDetailCode;
                            report.ItemClassName = itemClass.ItemClassName;
                            report.UnitName = unit.UnitName;
                            report.TotalQty = reqDetail.Qty;
                            report.UnitPrice = reqDetail.UnitPrice;
                            report.WorkOrderCode = workorder.WorkOrderCode;
                            report.WorkOrderStartDate = workorder.WorkOrderStartDate.Value.ToShortDateString();
                            report.WorkOrderEndDate = workorder.WorkOrderEndDate.Value.ToShortDateString();
                            report.TotalIssuedQty = workorderDetails.Sum(x => x.Qty);
                            report.InventoryBookNo = wod.InventoryBookNo;
                            report.InventoryBookPage = wod.InventoryBookPage;
                            if (note != null)
                            {
                                report.NotePercentage = note.NotePercentage;
                                report.NoteStatus = note.NoteStatus;
                                report.NoteTitle = note.NoteTitle;
                            }
                            report.IsApproved = wod.IsApproved;
                            if (wod.DateApproved != null)
                                report.DateApproved = wod.DateApproved.Value.ToShortDateString();
                            else
                                report.DateApproved = "0-0-0";
                            report.ReportId = reportID;
                            reports.Add(report);
                            report = new Models.Refactored.Report();
                            reportID++;

                        }
                    }

                }
            }
            return Json(reports.ToList().ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        private void CloseReports(ReportDocument reportDocument)
        {
            Sections sections = reportDocument.ReportDefinition.Sections;
            foreach (Section section in sections)
            {
                ReportObjects reportObjects = section.ReportObjects;
                foreach (ReportObject reportObject in reportObjects)
                {
                    if (reportObject.Kind == ReportObjectKind.SubreportObject)
                    {
                        SubreportObject subreportObject = (SubreportObject)reportObject;
                        ReportDocument subReportDocument = subreportObject.OpenSubreport(subreportObject.SubreportName);
                        subReportDocument.Close();
                    }
                }
            }
            reportDocument.Close();
        }
        public ActionResult PrintRequestReport()
        {
            if (RequestController._Request != null)
            {
                Requests requests = RequestController._Request;
                ReportDocument rd = new ReportDocument();

                rd.Load(Path.Combine(Server.MapPath("~/Reports/"), "RepRequets.rpt"));
                var s = db.VwRequests.Where(c => c.RequestId == requests.RequestId).Select(x => new
                {
                    RequestName = x.RequestName ?? "لا يوجد",
                    CompanyName = x.CompanyName ?? "لا يوجد",
                    RequestAmount = x.RequestAmount ?? 0,
                    RequestCode = x.RequestCode ?? "لا يوجد",
                    RequestDate = x.RequestDate ?? DateTime.Now,
                    RequestDuration = x.RequestDuration ?? 0,
                    RequestEndDate = x.RequestEndDate ?? DateTime.Now,
                    RequestNo = x.RequestNo ?? "لا يوجد",
                    RequestStartDate = x.RequestStartDate ?? DateTime.Now,
                    Qty = x.Qty ?? 0,
                    RequestDetailCode = x.RequestDetailCode ?? "لا يوجد",
                    x.RequestDetailId,
                    RequestDetailName = x.RequestDetailName ?? "لا يوجد",
                    RequestDetailSerial = x.RequestDetailSerial ?? "لا يوجد",
                    TotalPrice = x.TotalPrice ?? 0,
                    UnitName = x.UnitName ?? "لا يوجد",
                    UnitPrice = x.UnitPrice ?? 0,
                }).ToList();

                rd.SetDataSource(s);

                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();

                Stream stream = rd.ExportToStream(ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                CloseReports(rd);
                rd.Dispose();


                return File(stream, "application/pdf", "RepRequets.pdf");
            }
            else return Redirect("/Request/RequestDetails");
        }
        public ActionResult PrintWorkOrderReport()
        {
            if (RequestController._Request != null)
            {
                Requests requests = RequestController._Request;
                var wo = WorkOrderController._WorkOrder.WorkOrderId;
                ReportDocument rd = new ReportDocument();
                int userID = int.Parse(Session["UserID"].ToString());
                var Employee = db2.Employees.Where(x => x.UserId == userID).FirstOrDefault();

                rd.Load(Path.Combine(Server.MapPath("~/Reports/"), "RepWorkOrders.rpt"));
                var s = db.VwWorkOrders.Where(x => wo == x.WorkOrderId).Select(x => new
                {
                    RequestNo = x.RequestNo ?? "لا يوجد",
                    RequestName = x.RequestName ?? "لا يوجد",
                    RequestDate = x.RequestDate ?? DateTime.Now,
                    CompanyName = x.CompanyName ?? "لا يوجد",
                    RequestEndDate = x.RequestEndDate ?? DateTime.Now,
                    RequestStartDate = x.RequestStartDate ?? DateTime.Now,
                    WorkOrderCode = x.WorkOrderCode ?? "لا يوجد",
                    //WorkOrderEndDate = x.WorkOrderEndDate ?? DateTime.Now,
                    WorkOrderStartDate = x.WorkOrderStartDate ?? DateTime.Now,
                    RequestDetailCode = x.RequestDetailCode ?? "لا يوجد",
                    NotePercentage = x.NotePercentage,

                    RequestDetailName = x.RequestDetailName ?? "لا يوجد",
                    RequestDetailSerial = x.RequestDetailSerial ?? "لا يوجد",
                    Qty = x.Qty ?? 0,
                    TotalQtyIssued = x.TotalQtyIssued ?? 0,
                    UnitName = x.UnitName ?? "لا يوجد",
                    UnitPrice = x.UnitPrice ?? 0,
                    QtyIssued = x.QtyIssued ?? 0,
                    InventoryBookNo = x.InventoryBookNo ?? "لا يوجد",
                    InventoryBookPage = x.InventoryBookPage ?? "لا يوجد",
                    Engineer = x.Engineer ?? "لا يوجد",
                    President = x.President ?? "لا يوجد",
                    GeneralDirector = x.GeneralDirector ?? "لا يوجد",
                    Technical = x.Technical ?? "لا يوجد",
                    VicePresident = x.VicePresident ?? "لا يوجد",
                    RequestNote = x.RequestNote ?? "لا يوجد",
                    NoteTitle = x.NoteTitle ?? "لا يوجد",
                    NoteStatus = x.NoteStatus ?? "لا يوجد",
                    WorkOrderNoteId = x.WorkOrderNoteId.ToString() ?? "لا يوجد",
                    TotalPrice = x.TotalPrice ?? 0,
                }).Distinct().ToList();

                rd.SetDataSource(s);

                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                if (s.Count > 1)
                {
                    Stream stream = rd.ExportToStream(ExportFormatType.PortableDocFormat);
                    stream.Seek(0, SeekOrigin.Begin);
                    CloseReports(rd);
                    rd.Dispose();

                    return File(stream, "application/pdf", "RepWorkOrders.pdf");
                }
                if (Employee == null) return Redirect("/Consultation/ConsultationApproval");
                return Redirect("/WorkOrder/WorkOrderApproval");
            }
            else return Redirect("/WorkOrder/WorkOrderApproval");
        }
    }
}