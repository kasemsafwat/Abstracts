using Abstracts.Models;
using Abstracts.Models.InterfaceService;
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
    public class CompanyController : BaseController
    {
        readonly AbstractsDBContext db = new AbstractsDBContext();
        private readonly ICompaniesService companiesService;
        public CompanyController(ICompaniesService _companiesService)
        {
            companiesService = _companiesService;
        }
        [HandleError]
        [AuthorizedAction]
        public ActionResult Index()
        {
            if (db.Companies.Max(x => x.AutoNo) != null)
            {
                var AutoNo = db.Companies.Max(x => x.AutoNo).Value;
                AutoNo++;

                ViewBag.CompanyCode = PublicVariables.CompanyPrefixCode + AutoNo;
                return View();
            }
            return View();
        }
        [HandleError]
        public ActionResult WorkOrderStatusForCompany()
        {
            WorkOrderController._WorkOrder = null;
            return View();
        }
        public ActionResult GetCompanyList([DataSourceRequest] DataSourceRequest request)
        {
            Models.Refactored.Companies companies;
            List<Models.Refactored.Companies> list = new List<Models.Refactored.Companies>();
            var lst = companiesService.Read();
            foreach (var obj in lst)
            {
                companies = new Models.Refactored.Companies();
                companies.CompanyId = obj.CompanyId;
                companies.CompanyName = obj.CompanyName ?? "";
                companies.InsuranceNo = obj.InsuranceNo ?? "";
                companies.Mobile = obj.Mobile ?? "";
                companies.Phone = obj.Phone ?? "";
                companies.TaxRecordNo = obj.TaxRecordNo ?? "";
                companies.CompanyCode = obj.CompanyCode ?? "";
                companies.Email = obj.Email ?? "";
                companies.IsLocked = obj.IsLocked;
                if ((bool)obj.IsLocked) companies.Locked = "نعم";
                else companies.Locked = "لا";
                list.Add(companies);
            }
            return Json(list.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveCompany(Companies companies)
        {
            if (companies != null && companies.CompanyId == 0 && ModelState.IsValid)
                companiesService.Create(companies);
            else
                companiesService.Update(companies);
            var AutoNo = db.Companies.Max(x => x.AutoNo).Value;
            AutoNo++;
            return Json(AutoNo, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllCompanys()
        {
            using (var db = GetContext())
            {
                var companies = db.Companies.Where(x => x.IsLocked == false).ToList();

                return Json(companies.ToList(), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult CompanyUser()
        {
            return View();
        }
        public ActionResult SaveCompanyUsers(CompanyUsers companyUsers)
        {
            if (companyUsers != null && companyUsers.CompanyUsersId == 0 && ModelState.IsValid)
            {
                var entity = new CompanyUsers();

                entity.CompanyId = companyUsers.CompanyId;
                entity.Password = companyUsers.Password;
                entity.UserName = companyUsers.UserName;
                entity.RequestId = companyUsers.RequestId;

                db.CompanyUsers.Add(entity);
                db.SaveChanges();
                companyUsers.CompanyUsersId = (int)entity.CompanyUsersId;

            }
            else
            {
                var entity = db.CompanyUsers.First(s => s.CompanyUsersId == companyUsers.CompanyUsersId);

                entity.CompanyId = companyUsers.CompanyId;
                entity.Password = companyUsers.Password;
                entity.UserName = companyUsers.UserName;
                entity.RequestId = companyUsers.RequestId;

                db.CompanyUsers.Attach(entity);
                db.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();

            }

            return Json(1, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCompanyUsersList([DataSourceRequest] DataSourceRequest request)
        {
            var lst = db.CompanyUsers.ToList()
                             .Select(cp =>
                             {
                                 var _request = db.Requests.Where(x => x.RequestId == cp.RequestId).FirstOrDefault();
                                 var company = db.Companies.Where(x => x.CompanyId == cp.CompanyId).FirstOrDefault();
                                 return new Models.Refactored.CompanyUsers
                                 {
                                     CompanyId = cp.CompanyId,
                                     RequestName = _request.RequestName,
                                     CompanyName = company.CompanyName,
                                     Password = cp.Password,
                                     UserName = cp.UserName,
                                     CompanyUsersId = cp.CompanyUsersId,
                                     RequestId = cp.RequestId
                                 };
                             }).ToList();

            return Json(lst.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllCompanyUsers()
        {
            using (var db = GetContext())
            {
                var companies = db.CompanyUsers.ToList();

                return Json(companies.ToList(), JsonRequestBehavior.AllowGet);
            }
        }
    }
}