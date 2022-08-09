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
    public class UnitController : BaseController
    {
        readonly AbstractsDBContext db = new AbstractsDBContext();
        private readonly IUnitService unitService;
        public UnitController(IUnitService _unitService)
        {
            unitService = _unitService;
        }
        [AuthorizedAction]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetUnitList([DataSourceRequest] DataSourceRequest request)
        {
            Models.Refactored.Units units;
            List<Models.Refactored.Units> list = new List<Models.Refactored.Units>();
            var lst = unitService.Read();
            foreach (var obj in lst)
            {
                units = new Models.Refactored.Units();
                units.UnitId = obj.UnitId;
                units.UnitName = obj.UnitName;

                units.IsLocked = obj.IsLocked;
                if ((bool)obj.IsLocked) units.Locked = "نعم";
                else units.Locked = "لا";
                list.Add(units);
            }
            return Json(list.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveUnit([DataSourceRequest] DataSourceRequest request, Units units)
        {
            if (units != null && units.UnitId == 0)
            {
                return Json(unitService.Create(units));

            }
            else
                unitService.Update(units);

            return Json(unitService.Read().ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteUnit([DataSourceRequest] DataSourceRequest request, Units units)
        {
            if (units != null && ModelState.IsValid)
            {
                var exists = db.RequestDetails.FirstOrDefault(x => x.UnitId == units.UnitId);
                if (exists != null)
                {
                    ModelState.AddModelError("خطأ", "لا يتم مسح هذا الصف لانه معتمد عليه");
                    var result = ModelState.ToDataSourceResult();
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                var entity = db.Units.First(s => s.UnitId == units.UnitId);

                db.Units.Attach(entity);
                db.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                try
                {
                    db.SaveChanges();
                }
                catch
                {
                    ModelState.AddModelError("خطأ", "لا يتم مسح هذا الصف لانه معتمد عليه");
                    var result = ModelState.ToDataSourceResult();
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(unitService.Read().ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllUnits()
        {
            var units = db.Units.Where(x => x.IsLocked == false).ToList();
            return Json(units.ToList(), JsonRequestBehavior.AllowGet);
        }
    }
}