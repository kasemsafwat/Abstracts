using Abstracts.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Abstracts.Controllers
{
    public class ItemClassController : BaseController
    {
        private readonly AbstractsDBContext db = new AbstractsDBContext();
        [AuthorizedAction]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetItemClassList([DataSourceRequest] DataSourceRequest request)
        {
            List<Models.Refactored.ItemClass> list = new List<Models.Refactored.ItemClass>();

            var result = db.ItemClass.ToList().OrderBy(x => x.IsLocked)
                         .Select(item =>
                         {
                             string locked = "لا";
                             if (item.IsLocked == true)
                                 locked = "نعم";
                             return new Models.Refactored.ItemClass
                             {
                                 ItemClassId = item.ItemClassId,
                                 ItemClassName = item.ItemClassName,
                                 IsLocked = item.IsLocked,
                                 Locked = locked
                             };
                         }).ToList();


            list = result;

            return Json(list.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveItemClass(ItemClass itemClass)
        {
            if (itemClass != null && itemClass.ItemClassId == 0)
            {

                var entity = db.ItemClass.Where(x => x.ItemClassName == itemClass.ItemClassName).FirstOrDefault();
                if (entity == null)
                {
                    entity = new ItemClass();
                    if (itemClass.ItemClassName != null)
                        entity.ItemClassName = itemClass.ItemClassName;
                    entity.IsLocked = itemClass.IsLocked;
                    db.ItemClass.Add(entity);
                    db.SaveChanges();


                    itemClass.ItemClassId = (int)entity.ItemClassId;
                    return Json(1, JsonRequestBehavior.AllowGet);
                }
                return Json(-1, JsonRequestBehavior.AllowGet);


            }
            else
            {

                var entity = db.ItemClass.First(s => s.ItemClassId == itemClass.ItemClassId);
                if (itemClass.ItemClassName != null)
                    entity.ItemClassName = itemClass.ItemClassName;
                entity.UserIdlock = 1;
                entity.IsLocked = itemClass.IsLocked;
                if ((bool)entity.IsLocked == false && (bool)itemClass.IsLocked)
                    entity.DateLock = DateTime.Now;

                db.ItemClass.Attach(entity);
                db.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();

            }

            return Json(1, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteItemClass(ItemClass itemClass)
        {
            if (itemClass != null && ModelState.IsValid)
            {
                var entity = db.ItemClass.First(s => s.ItemClassId == itemClass.ItemClassId);

                entity.IsLocked = true;
                entity.DateLock = DateTime.Now;
                entity.UserIdlock = 123;

                db.ItemClass.Attach(entity);
                db.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
            }
            return Json(1, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetItemClassByRequestDetailsID(int RequestDetailsID)
        {
            var itemClass = db.RequestDetailClass.Where(x => x.RequestDetailId == RequestDetailsID).ToList()
                .Select(x =>
                {
                    var i = db.ItemClass.Where(item => item.ItemClassId == x.ItemClassId).FirstOrDefault();
                    return new Models.Refactored.RequestDetailsClass
                    {
                        ItemClassId = x.ItemClassId,
                    };
                }).ToList();
            return Json(itemClass.ToList(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllItemClass()
        {
            var itemClass = db.ItemClass.Where(x => x.IsLocked == false).ToList();
            return Json(itemClass.ToList(), JsonRequestBehavior.AllowGet);
        }
    }
}