using Abstracts.Models;
using Abstracts.Models.MasterLoginModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Abstracts.Controllers
{
    public class SettingController : BaseController
    {
        private readonly AbstractsDBContext db = new AbstractsDBContext();
        private readonly ParadiseMasterLogInDBContext db2 = new ParadiseMasterLogInDBContext();
        [AuthorizedAction]
        public ActionResult Index()
        {
            var entity = db.Setting.ToList().FirstOrDefault();
            if (entity == null) entity = new Setting();
            return View(entity);
        }
        public ActionResult SaveSetting(Setting setting)
        {
            if (setting != null && ModelState.IsValid)
            {
                bool IsNew = false;
                using (var db = GetContext())
                {
                    var entity = db.Setting.Where(x => x.DepartmentId == setting.DepartmentId).FirstOrDefault();
                    if (entity == null)
                    {
                        IsNew = true;
                        entity = new Setting();
                    }

                    entity.DepartmentId = setting.DepartmentId;
                    entity.President = setting.President;
                    entity.VicePresident = setting.VicePresident;
                    entity.GeneralDirector = setting.GeneralDirector;
                    entity.GeneralManager = setting.GeneralManager;
                    entity.Engineer = setting.Engineer;
                    entity.Technical = setting.Technical;


                    if (IsNew)
                        db.Setting.Add(entity);
                    else
                        db.Setting.Attach(entity);
                    db.SaveChanges();
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetAllDepartments()
        {
            var departments = db2.Departments.ToList();

            return Json(departments.ToList(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetSettingData(long DepartmentID)
        {
            var setting = db.Setting.Where(x => x.DepartmentId == DepartmentID).FirstOrDefault();
            if (setting == null) return Json(-1, JsonRequestBehavior.AllowGet);
            return Json(setting, JsonRequestBehavior.AllowGet);
        }
    }
}