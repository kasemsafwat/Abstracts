using Abstracts.Models;
using Abstracts.Models.InterfaceService;
using Abstracts.Models.MasterLoginModel;
using Abstracts.Models.Pbo;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Abstracts.Controllers
{
    public class UserController : Base2Controller
    {
        readonly ParadiseMasterLogInDBContext db = new ParadiseMasterLogInDBContext();
        private readonly AbstractsDBContext db2 = new AbstractsDBContext();
        private readonly IUserService userService;
        public UserController(IUserService _userService)
        {
            userService = _userService;
        }
        [AuthorizedAction]
        public ActionResult Index()
        {
            var AutoNo = db.Users.Max(x => x.AutoNo).Value;
            AutoNo++;

            ViewBag.RequestCode = PublicVariables.UserPrefixCode + AutoNo;

            return View();
        }
        public ActionResult UserRequests()
        {
            Session["UserRequestId"] = null;

            return View();
        }

        public JsonResult GetExecutiveEngineerUsersFromDepartment(long DepartmentId)
        {
            var ExecutiveEngineerUsers = db.Users.Where(x => x.DepartmentId == DepartmentId && x.IsExecutiveEngineer == true).Select(x => new { UserName = x.UserName, UserId = x.UserId }).ToList();
            return Json(ExecutiveEngineerUsers, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetRequestsNotRelatedToUser(long UserId)
        {
            Session["UserRequestId"] = UserId;
            var PreviousRequestIDs = db2.UserRequests.Select(x => x.RequestId).ToList();

            List<Requests> List = new List<Requests>();

            if (PreviousRequestIDs.Count == 0) List = db2.Requests.ToList();
            var requestIDs = db2.Requests.Select(x => x.RequestId).ToList();
            foreach (var obj in requestIDs)
            {
                var _request = db2.Requests.Where(x => !PreviousRequestIDs.Contains(obj) && x.RequestId == obj).FirstOrDefault();
                if (_request != null)
                    List.Add(_request);
            }
            var Result = db2.Requests.Select(x => new { RequestNo = x.RequestNo, RequestId = x.RequestId, RequestName = x.RequestName }).ToList();

            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetUserRequestsList([DataSourceRequest] DataSourceRequest request)
        {
            long userId = 0;
            if (Session["UserRequestId"] != null)
            {
                userId = (long)Session["UserRequestId"];
            }
            var UserRequest = db2.UserRequests.Where(x => x.UserId == userId).Select(x => x.RequestId).ToList();
            List<Requests> List = new List<Requests>();


            var requests = db2.Requests.Where(x => UserRequest.Contains(x.RequestId)).ToList();


            var result = db2.Requests.ToList().Where(x => UserRequest.Contains(x.RequestId)).OrderBy(x => x.IsLocked).OrderByDescending(x => x.RequestId)
                           .Select(req =>
                           {
                               var _UserRequest = db2.UserRequests.Where(x => x.RequestId == req.RequestId).FirstOrDefault();
                               var Company = new Companies();
                               if (req.CompanyId != null)
                               {
                                   Company = db2.Companies.Where(x => x.CompanyId == req.CompanyId).FirstOrDefault();
                               }
                               var _lock = "";
                               if ((bool)req.IsLocked) _lock = "نعم";
                               else _lock = "لا";
                               return new Models.Refactored.UserRequests
                               {
                                   UserRequestId = _UserRequest.UserRequestId,
                                   UserId = _UserRequest.UserId,
                                   RequestId = req.RequestId,
                                   RequestCode = req.RequestCode,
                                   RequestName = req.RequestName,
                                   IsLocked = req.IsLocked,
                                   RequestDate = req.RequestDate,
                                   FileNameDetails = req.RequestDetailsFileName,
                                   CompanyId = req.CompanyId,
                                   Company = Company == null ? null : Company.CompanyName,
                                   RequestAmount = req.RequestAmount,
                                   RequestDuration = req.RequestDuration,
                                   Notes = req.Notes ?? "",
                                   Locked = _lock,
                                   RequestStartDate = req.RequestStartDate,
                                   RequestEndDate = req.RequestEndDate,
                                   RequestDateString = req.RequestDate == null ? null : req.RequestDate.Value.ToShortDateString().ToString(),
                                   RequestStartDateString = req.RequestStartDate == null ? null : req.RequestStartDate.Value.ToShortDateString(),
                                   RequestEndDateString = req.RequestEndDate == null ? null : req.RequestEndDate.Value.ToShortDateString(),
                                   FileName = req.RequestFileName,
                                   RequestNo = req.RequestNo
                               };

                           }).ToList();
            if (result != null)

                return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            else
                return Json(new List<UserRequests>().ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetUserList([DataSourceRequest] DataSourceRequest request)
        {
            return Json(userService.Read().ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveUserRequests(UserRequests userRequests)
        {
            if (userRequests != null && ModelState.IsValid)
            {
                var entity = new UserRequests();


                entity.RequestId = userRequests.RequestId;
                entity.UserId = userRequests.UserId;

                db2.UserRequests.Add(entity);
                db2.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }

            return Json(false, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteUserRequests(UserRequests userRequests)
        {
            if (userRequests != null && userRequests.UserId != 0 && ModelState.IsValid)
            {
                var entity = db2.UserRequests.Where(x => x.UserRequestId == userRequests.UserRequestId).FirstOrDefault();


                entity.RequestId = userRequests.RequestId;
                entity.UserId = userRequests.UserId;

                db2.UserRequests.Add(entity);

                db2.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                db2.SaveChanges();

                return Json(true, JsonRequestBehavior.AllowGet);
            }

            return Json(false);
        }
        public ActionResult SaveUser(Users users)
        {
            if (users != null && users.UserId == 0 && ModelState.IsValid)
            {
                long result = userService.Create(users);
                if (result == -1) { return Json(-1, JsonRequestBehavior.AllowGet); }
            }
            else
                userService.Update(users);

            var AutoNo = db.Users.Max(x => x.AutoNo).Value;
            AutoNo++;

            return Json(AutoNo, JsonRequestBehavior.AllowGet);
        }
    }
}