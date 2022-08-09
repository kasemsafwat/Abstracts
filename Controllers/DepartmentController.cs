using Abstracts.Models;
using Abstracts.Models.MasterLoginModel;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Abstracts.Controllers
{
    public class DepartmentController : Controller
    { 
        private readonly ParadiseMasterLogInDBContext db = new ParadiseMasterLogInDBContext();
        [AuthorizedAction]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetDepartmentList([DataSourceRequest] DataSourceRequest request)
        {
            var lst = db.Departments.ToList();
            return Json(lst.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveDepartment([DataSourceRequest] DataSourceRequest request, Departments departments)
        {
            if (departments != null && departments.DepartmentId == 0)
            {

                var entity = db.Departments.Where(x => x.DepartmentName == departments.DepartmentName).FirstOrDefault();

                entity = new Departments();
                entity.DepartmentName = departments.DepartmentName;

                db.Departments.Add(entity);
                db.SaveChanges();


                departments.DepartmentId = (int)entity.DepartmentId;



                return Json(departments.DepartmentId, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var entity = db.Departments.Where(x => x.DepartmentId == departments.DepartmentId).FirstOrDefault();

                entity.DepartmentName = departments.DepartmentName;

                db.Departments.Attach(entity);
                db.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
                return Json(entity, JsonRequestBehavior.AllowGet);
            }


        }
        public ActionResult DeleteDepartment([DataSourceRequest] DataSourceRequest request, Departments departments)
        {
            if (departments != null && departments.DepartmentId != 0)
            {
                var exists = db.Users.Where(x => x.DepartmentId == departments.DepartmentId).FirstOrDefault();
                if (exists != null)
                {
                    ModelState.AddModelError("خطأ", "لا يتم مسح هذا الصف لانه معتمد عليه");
                    var result = ModelState.ToDataSourceResult();
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                var entity = db.Departments.Where(x => x.DepartmentId == departments.DepartmentId).FirstOrDefault();

                db.Departments.Add(entity);
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
                return Json(1, JsonRequestBehavior.AllowGet);
            }

            return Json(0, JsonRequestBehavior.AllowGet);

        }
    }
}