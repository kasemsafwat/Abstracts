using Abstracts.Models.InterfaceService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Abstracts.Models.Service
{
    public class CompaniesService : BaseService, ICompaniesService
    {
        public void Create(Companies companies)
        {
            using (var db = GetContext())
            {
                var entity = new Companies();
                long AutoNo = 0;
                if (db.Companies.Max(x => x.AutoNo) != null)
                {
                    AutoNo = db.Companies.Max(x => x.AutoNo).Value;
                }
                AutoNo++;
                entity.AutoNo = AutoNo;

                entity.CompanyCode = Pbo.PublicVariables.CompanyPrefixCode + AutoNo;
                entity.CompanyName = companies.CompanyName;
                entity.DateAdd = DateTime.Now;
                entity.Email = companies.Email;
                entity.InsuranceNo = companies.InsuranceNo;
                entity.IsLocked = companies.IsLocked;

                entity.Mobile = companies.Mobile;
                entity.Phone = companies.Phone;
                entity.Requests = companies.Requests;
                entity.TaxRecordNo = companies.TaxRecordNo;
                entity.UserIdadd = 1;


                if ((bool)entity.IsLocked == false && (bool)companies.IsLocked)
                    entity.DateLock = DateTime.Now;


                db.Companies.Add(entity);
                db.SaveChanges();
                companies.CompanyId = (int)entity.CompanyId;
            }
        }

        public void Delete(Companies companies)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Companies> Read()
        {
            return GetAll();
        }
        public IList<Companies> GetAll()
        {
            using (var db = GetContext())
            {
                var result = db.Companies.ToList().OrderBy(x => x.IsLocked)
                             .Select(cp =>
                             {
                                 return new Companies
                                 {
                                     CompanyId = cp.CompanyId,
                                     CompanyCode = cp.CompanyCode,
                                     IsLocked = cp.IsLocked,
                                     CompanyName = cp.CompanyName,
                                     Email = cp.Email,
                                     InsuranceNo = cp.InsuranceNo,
                                     Mobile = cp.Mobile,
                                     Phone = cp.Phone,
                                     Requests = cp.Requests,
                                     TaxRecordNo = cp.TaxRecordNo
                                 };
                             }).ToList();

                return result;
            }
        }

        public void Update(Companies companies)
        {
            using (var db = GetContext())
            {
                var entity = db.Companies.First(s => s.CompanyId == companies.CompanyId);

                entity.Phone = companies.Phone;
                entity.Requests = companies.Requests;
                entity.TaxRecordNo = companies.TaxRecordNo;
                entity.UserIdupdate = 1;

                if ((bool)entity.IsLocked == false && (bool)companies.IsLocked)
                {
                    entity.DateLock = DateTime.Now;
                    entity.UserIdlock = 1;
                }
            }
        }
    }
}