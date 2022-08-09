using Abstracts.Models;
using Abstracts.Models.InterfaceService;
using Abstracts.Models.Pbo;
using Abstracts.Models.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Abstracts.Controllers
{
    public class HomeController : Base2Controller
    {
        readonly AbstractsDBContext db = new AbstractsDBContext();
        private readonly IActionService actionService;
        private readonly IUserService userService;
        private readonly ISystemService systemService;
        public static long _UserID = 0;
        public HomeController(ISystemService _systemService, IActionService _actionService, IUserService _userService)
        {
            systemService = _systemService;
            actionService = _actionService;
            userService = _userService;
        }
        [Authenticate]
        [AuthorizedAction]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            var sysVersion = systemService.Read().SystemVersion;
            ViewBag.sysVersion = sysVersion;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserModel User)
        {
            if (ModelState.IsValid && User.type == 1)
            {
                Session.Remove("menus");
                Session.Remove("UserID");
                Models.MasterLoginModel.Users users = userService.Login(User);

                if (users != null)
                {
                    if (users.IsBackOffice == false || users.IsBackOffice == null)
                    {
                        ModelState.AddModelError("", "غير مسموح لك بإستخدام البرنامج الرئيسي. يرجى الرجوع لمسؤول الموقع");
                    }
                    else
                    {
                        if (users.IsLocked == true || users.IsLocked == null)
                        {
                            ModelState.AddModelError("", "لقد تم وقف حسابك. يرجى الرجوع لمسؤول الموقع");
                        }
                        else
                        {
                            FormsAuthentication.SetAuthCookie(users.UserFullName, false);
                            int? RoleID = (int?)users.RoleId;
                            Session["UserID"] = users.UserId;
                            Session["name"] = users.UserName;
                            _UserID = users.UserId;
                            List<string> menus = new List<string>();

                            var Screens = userService.GetActionsForUserRoleID(RoleID);

                            foreach (var Screen in Screens)
                            {
                                string URL = string.Concat("/", Screen.Controller, "/", Screen.Action);
                                menus.Add(URL);
                            }
                            if (menus.Count > 0) HttpContext.Session["menus"] = JsonConvert.SerializeObject(menus);

                            return Redirect("/Home/Index");
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "فشل تسجيل الدخول... اسم المستخدم أو كلمة المرور غير صحيحة");
                }
            }
            else if (ModelState.IsValid && User.type == 2)
            {
                Session.Remove("User");
                CompanyUsers user = db.CompanyUsers.Where(x => x.UserName == User.UserName && x.Password == User.Password).FirstOrDefault();

                if (user != null)
                {

                    FormsAuthentication.SetAuthCookie(user.UserName, false);

                    Session["User"] = user;
                    return Redirect("/Workorder/WorkOrdersForCompany");

                }
            }
            else if (ModelState.IsValid && User.type == 3)
            {
                Session.Remove("User");
                ConsultationUser user = db.ConsultationUser.Where(x => x.Email == User.UserName && x.Password == User.Password).FirstOrDefault();

                if (user != null)
                {

                    FormsAuthentication.SetAuthCookie(user.Email, false);

                    Session["User"] = user;
                    Session["UserID"] = user.ConsultationUserId;
                    return Redirect("/Consultation/ConsultationApproval");

                }
            }
            return View(User);
        }

        public ActionResult LoginFromOthers(string UC)
        {
            string UserCode = EncryptionService.Decrypt(UC);
            if (ModelState.IsValid)
            {
                Session.Remove("menus");
                Session.Remove("UserID");
                Models.MasterLoginModel.Users users = userService.LoginByUserCode(UserCode);

                if (users != null)
                {
                    if (users.IsBackOffice == false || users.IsBackOffice == null)
                    {
                        ModelState.AddModelError("", "غير مسموح لك بإستخدام البرنامج الرئيسي. يرجى الرجوع لمسؤول الموقع");
                    }
                    else
                    {
                        if (users.IsLocked == true || users.IsLocked == null)
                        {
                            ModelState.AddModelError("", "لقد تم وقف حسابك. يرجى الرجوع لمسؤول الموقع");
                        }
                        else
                        {
                            FormsAuthentication.SetAuthCookie(users.UserFullName, false);
                            int? RoleID = (int?)users.RoleId;
                            Session["UserID"] = users.UserId;
                            List<string> menus = new List<string>();

                            var Screens = userService.GetActionsForUserRoleID(RoleID);

                            foreach (var Screen in Screens)
                            {
                                string URL = string.Concat("/", Screen.Controller, "/", Screen.Action);
                                menus.Add(URL);
                            }
                            if (menus.Count > 0) HttpContext.Session["menus"] = JsonConvert.SerializeObject(menus);

                            return Redirect("/Home/Index");
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "فشل تسجيل الدخول... اسم المستخدم أو كلمة المرور غير صحيحة");
                }
            }
            return View(User);
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Remove("menus");
            Session.Remove("UserID");
            return RedirectToAction("Login");
        }
        public ActionResult AccessDenied()
        {
            return View();
        }

        public JsonResult GetAllDepartments()
        {
            using (var db = GetContext())
            {
                var department = db.Departments.ToList();
                return Json(department.ToList(), JsonRequestBehavior.AllowGet);
            }
        }
    }
}