using Abstracts.Models;
using Abstracts.Models.InterfaceService;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Abstracts.Controllers
{
    public class NoteController : Controller
    {
        private readonly IWorkOrderNoteService workOrderNoteService;
        public NoteController(IWorkOrderNoteService _workOrderNoteService)
        {
            workOrderNoteService = _workOrderNoteService;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetNoteList([DataSourceRequest] DataSourceRequest request)
        {
            if (Session["RequestIDForNote"] != null)
            {
                long RequestID = (long)Session["RequestIDForNote"];

                var result = workOrderNoteService.Read(RequestID);
                if (result.Count() > 0)
                {
                    ViewData["notes"] = result.ToList();
                    ViewData["defaultCategory"] = result.First();
                }
                return Json(result.ToDataSourceResult(request));
            }
            return Json(new List<WorkOrderNotes>().ToDataSourceResult(request));
        }

        public JsonResult GetAllNotes()
        {
            if (Session["RequestIDForNote"] != null)
            {
                long RequestID = (long)Session["RequestIDForNote"];

                var result = workOrderNoteService.Read(RequestID);
                ViewData["notes"] = result.ToList();
                ViewData["defaultCategory"] = result.First();
                return Json(result.ToList(), JsonRequestBehavior.AllowGet);
            }
            return Json(new List<WorkOrderNotes>(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult saveNote(WorkOrderNotes workOrderNotes)
        {
            if (workOrderNotes != null && workOrderNotes.WorkOrderNoteId == 0)
            {
                workOrderNotes.RequestId = RequestController._Request.RequestId;
                workOrderNoteService.Create(workOrderNotes);
            }
            else
                workOrderNoteService.Update(workOrderNotes);

            return Json(1);
        }

    }
}