using Abstracts.Models;
using Abstracts.Models.MasterLoginModel;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Abstracts.Controllers
{
    public class ImportDataController : Controller
    {
        private readonly AbstractsDBContext db = new AbstractsDBContext();
        private readonly ParadiseMasterLogInDBContext db2 = new ParadiseMasterLogInDBContext();
        [AuthorizedAction]
        public ActionResult Index()
        {
            return View();
        }
        public void ExportWorkOrderExcelData()
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add(new DataColumn("رقم البند"));
            dataTable.Columns.Add(new DataColumn("كود البند"));
            dataTable.Columns.Add(new DataColumn("البند"));
            dataTable.Columns.Add(new DataColumn("الوصف"));
            dataTable.Columns.Add(new DataColumn("الوحدة"));
            dataTable.Columns.Add(new DataColumn("الكمية المقدرة"));
            dataTable.Columns.Add(new DataColumn("سعر الوحدة بالجنية"));
            dataTable.Columns.Add(new DataColumn("كمية الأعمال التى تمت حتى المستخلص السابق"));
            dataTable.Columns.Add(new DataColumn("كمية الأعمال التى تمت خلال الفترة"));


            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Sheet1");

            workSheet.Cells[1, 1].LoadFromDataTable(dataTable, true);
            int row = 2;
            long? RequestID = 0;
            if (Session["WorkOrderDetails"] != null)
            {
                List<Models.Refactored.WorkOrderDetails> result = (List<Models.Refactored.WorkOrderDetails>)Session["WorkOrderDetails"];
                RequestID = result.FirstOrDefault().RequestId;
                foreach (var item in result)
                {
                    workSheet.Cells[string.Format("A{0}", row)].Value = item.WorkOrderDetailId;
                    workSheet.Cells[string.Format("B{0}", row)].Value = item.RequestDetailCode;

                    workSheet.Cells[string.Format("C{0}", row)].Value = item.RequestDetailSerial;
                    workSheet.Cells[string.Format("D{0}", row)].Value = item.RequestDetailName;
                    workSheet.Cells[string.Format("E{0}", row)].Value = item.Unit;
                    workSheet.Cells[string.Format("F{0}", row)].Value = item.Qty;
                    workSheet.Cells[string.Format("G{0}", row)].Value = item.UnitPrice;
                    workSheet.Cells[string.Format("H{0}", row)].Value = item.PreviousQty;
                    workSheet.Cells[string.Format("I{0}", row)].Value = item.NowQty;

                    row++;
                }
            }

            using (var memoryStream = new MemoryStream())
            {
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;  filename=نموذج المستخلص.xlsx");
                excel.SaveAs(memoryStream);
                memoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }
        public void ExportWorkOrderExcelData1()
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add(new DataColumn("رقم البند"));
            dataTable.Columns.Add(new DataColumn("كود البند"));
            dataTable.Columns.Add(new DataColumn("البند"));
            dataTable.Columns.Add(new DataColumn("الوصف"));
            dataTable.Columns.Add(new DataColumn("الوحدة"));
            dataTable.Columns.Add(new DataColumn("الكمية المقدرة"));
            dataTable.Columns.Add(new DataColumn("سعر الوحدة بالجنية"));
            dataTable.Columns.Add(new DataColumn("كمية الأعمال التى تمت حتى المستخلص السابق"));
            dataTable.Columns.Add(new DataColumn("كمية الأعمال التى تمت خلال الفترة"));


            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Sheet1");

            workSheet.Cells[1, 1].LoadFromDataTable(dataTable, true);
            int row = 2;
            long? RequestID = 0;
            if (Session["WorkOrderDetails"] != null)
            {
                List<WorkOrderDetails> result = (List<WorkOrderDetails>)Session["WorkOrderDetails"];
                var requestDetails = db.RequestDetails.FirstOrDefault(x => x.RequestDetailId == result.FirstOrDefault().RequestDetailId);
                RequestID = requestDetails.RequestId;
                foreach (var item in result)
                {
                    var _requestDetails = db.RequestDetails.FirstOrDefault(x => x.RequestDetailId == item.RequestDetailId);
                    
                    workSheet.Cells[string.Format("A{0}", row)].Value = item.WorkOrderDetailId;
                    workSheet.Cells[string.Format("B{0}", row)].Value = _requestDetails.RequestDetailCode;

                    workSheet.Cells[string.Format("C{0}", row)].Value = _requestDetails.RequestDetailSerial;
                    workSheet.Cells[string.Format("D{0}", row)].Value = _requestDetails.RequestDetailName;
                    workSheet.Cells[string.Format("E{0}", row)].Value = _requestDetails.Unit;
                    workSheet.Cells[string.Format("F{0}", row)].Value = item.Qty;
                    workSheet.Cells[string.Format("G{0}", row)].Value = _requestDetails.UnitPrice;
                    workSheet.Cells[string.Format("H{0}", row)].Value = _requestDetails.Qty;
                    workSheet.Cells[string.Format("I{0}", row)].Value = item.Qty;

                    row++;
                }
            }




            using (var memoryStream = new MemoryStream())
            {
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;  filename=نموذج المستخلص.xlsx");
                excel.SaveAs(memoryStream);
                memoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }
        public void ExportWorkOrderWithDepartmentExcelData()
        {
            var dataTable = new DataTable();

            dataTable.Columns.Add(new DataColumn("رقم البند"));
            dataTable.Columns.Add(new DataColumn("كود البند"));
            dataTable.Columns.Add(new DataColumn("البند"));
            dataTable.Columns.Add(new DataColumn("الوصف"));
            dataTable.Columns.Add(new DataColumn("الوحدة"));
            dataTable.Columns.Add(new DataColumn("الكمية المقدرة"));
            dataTable.Columns.Add(new DataColumn("سعر الوحدة بالجنية"));
            dataTable.Columns.Add(new DataColumn("كمية الأعمال التى تمت حتى المستخلص السابق"));
            dataTable.Columns.Add(new DataColumn("كمية الأعمال التى تمت خلال الفترة"));
            dataTable.Columns.Add(new DataColumn("الفرع المختص"));

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Sheet1");

            workSheet.Cells[1, 1].LoadFromDataTable(dataTable, true);
            int row = 2;
            var workOrders = new List<WorkOrders>();
            if (WorkOrderController._WorkOrder != null)
            {
                List<WorkOrderDetails> result = new List<WorkOrderDetails>();

                var workOrder = db.WorkOrders.Where(x => x.WorkOrderId == WorkOrderController._WorkOrder.WorkOrderId).ToList();
                foreach (var wo in workOrder)
                {
                    var flag = false;
                    var wods = db.WorkOrderDetails.Where(x => x.WorkOrderId == wo.WorkOrderId).OrderByDescending(x => x.IsApproved).ToList();
                    foreach (var wod in wods)
                    {
                        if (wod.IsApproved == false) { flag = false;
                            result.Add(wod);
                        }
                        if (wod.IsApproved == true) { flag = true; result.Add(wod); }
                    }
                    if (!flag) workOrders.Add(wo);
                }

                foreach (var item in result)
                {
                    var requestDetails = db.RequestDetails.FirstOrDefault(x => x.RequestDetailId == item.RequestDetailId);
                    var department = db2.Departments.FirstOrDefault(x => x.DepartmentId == requestDetails.DepartmentId);
                    workSheet.Cells[string.Format("A{0}", row)].Value = item.WorkOrderDetailId;
                    workSheet.Cells[string.Format("B{0}", row)].Value = requestDetails.RequestDetailCode;

                    workSheet.Cells[string.Format("C{0}", row)].Value = requestDetails.RequestDetailSerial;
                    workSheet.Cells[string.Format("D{0}", row)].Value = requestDetails.RequestDetailName;
                    workSheet.Cells[string.Format("E{0}", row)].Value = requestDetails.Unit;
                    workSheet.Cells[string.Format("F{0}", row)].Value = item.Qty;
                    workSheet.Cells[string.Format("G{0}", row)].Value = requestDetails.UnitPrice;
                    workSheet.Cells[string.Format("H{0}", row)].Value = requestDetails.Qty;
                    workSheet.Cells[string.Format("I{0}", row)].Value = item.Qty;
                    if (department != null)
                        workSheet.Cells[string.Format("J{0}", row)].Value = department.DepartmentName;

                    row++;
                }
            }




            using (var memoryStream = new MemoryStream())
            {
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;  filename=نموذج المستخلص.xlsx");
                excel.SaveAs(memoryStream);
                memoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }

        public void ExportExcelData()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("إسم البند"));
            dataTable.Columns.Add(new DataColumn("الوصف"));
            dataTable.Columns.Add(new DataColumn("الوحدة"));
            dataTable.Columns.Add(new DataColumn("الكمية المقدرة"));
            dataTable.Columns.Add(new DataColumn("سعر الوحدة بالجنية"));
            dataTable.Columns.Add(new DataColumn("الفرع المختص"));
            dataTable.Columns.Add(new DataColumn("فئة البند"));

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Sheet1");

            workSheet.Cells[1, 1].LoadFromDataTable(dataTable, true);

            ExcelWorksheet Sheet = excel.Workbook.Worksheets.Add("الوحدة");
            Sheet.Cells["A1"].Value = "الوحدة";

            int row = 2;
            var collection = db.Units.ToList();
            foreach (var item in collection)
            {
                Sheet.Cells[string.Format("A{0}", row)].Value = item.UnitName;
                row++;
            }
            ExcelWorksheet Sheet2 = excel.Workbook.Worksheets.Add("الفرع المختص");
            Sheet2.Cells["A1"].Value = "الفرع المختص";

            int row2 = 2;
            var collection2 = db2.Departments.ToList();
            foreach (var item in collection2)
            {
                Sheet2.Cells[string.Format("A{0}", row2)].Value = item.DepartmentName;
                row2++;
            }
            ExcelWorksheet Sheet3 = excel.Workbook.Worksheets.Add("فئات البنود");
            Sheet3.Cells["A1"].Value = "فئة البند";

            int row3 = 2;
            var collection3 = db.ItemClass.ToList();
            foreach (var item in collection3)
            {
                Sheet3.Cells[string.Format("A{0}", row3)].Value = item.ItemClassName;
                row3++;
            }


            using (var memoryStream = new MemoryStream())
            {
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;  filename=نموذج مقايسة الأعمال.xlsx");
                excel.SaveAs(memoryStream);
                memoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }


    }
}