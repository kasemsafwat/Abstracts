
using Abstracts.Models.InterfaceService;
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
    public class RoleController : Base2Controller
    {
        private readonly IRoleService roleService;
        public RoleController(IRoleService _roleService)
        {
            roleService = _roleService;
        }
        [AuthorizedAction]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetRoleList([DataSourceRequest] DataSourceRequest request)
        {
            return Json(roleService.Read().ToDataSourceResult(request),JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllRoles()
        {
            using (var db = GetContext())
            {
                var roles = db.Roles.Select(x => new { RoleId = x.RoleId, RoleName = x.RoleName })
                    .ToList();
                return Json(roles.ToList(), JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SaveRole([DataSourceRequest] DataSourceRequest request, Roles roles)
        {
            if (roles != null && roles.RoleId == 0 && ModelState.IsValid)
                roleService.Create(roles);
            else
                roleService.Update(roles);

            return Json(roleService.Read().ToDataSourceResult(request),JsonRequestBehavior.AllowGet);
        }



    }
}