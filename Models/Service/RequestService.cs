using Abstracts.Models.InterfaceService;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Abstracts.Models.Service
{
    public class RequestService : BaseService, IRequestService
    {
        readonly IDepartmentService departmentService;
        public RequestService(IDepartmentService _departmentService) { departmentService = _departmentService; }
        public int Create(Requests requests)
        {
            using (var db = GetContext())
            {
                var No = db.Requests.Where(x => x.RequestNo == requests.RequestNo && x.DateAdd.Value.Year == requests.RequestDate.Value.Year).FirstOrDefault();

                if (No == null)
                {
                    var entity = new Requests();
                    long? AutoNo;
                    if (db.Requests.Max(x => x.AutoNo) == null) { AutoNo = 0; }
                    else { AutoNo = db.Requests.Max(x => x.AutoNo).Value; }

                    AutoNo++;
                    entity.AutoNo = AutoNo;

                    entity.RequestName = requests.RequestName;
                    entity.RequestDate = requests.RequestDate;
                    entity.UserIdadd = 1;


                    db.Requests.Add(entity);
                    db.SaveChanges();
                    requests.RequestId = (int)entity.RequestId;
                    return 0;
                }
                return -1;
            }
        }
        public long CreateDetails(RequestDetails requestDetails, Requests requests)
        {
            using (var db = GetContext())
            {
                var entity = db.RequestDetails.Where(x => x.RequestDetailName == requestDetails.RequestDetailName && x.RequestId == requests.RequestId).FirstOrDefault();

                if (entity == null)
                {
                    entity = new RequestDetails();
                    entity.RequestId = requests.RequestId;

                    long? AutoNo;
                    if (db.RequestDetails.Max(x => x.AutoNo) == null) { AutoNo = 0; }
                    else { AutoNo = db.RequestDetails.Max(x => x.AutoNo).Value; }
                    entity.TotalPrice = entity.Qty * entity.UnitPrice;
                    entity.UserIdadd = 1;


                    db.RequestDetails.Add(entity);
                    db.SaveChanges();
                    requestDetails.RequestDetailId = (int)entity.RequestDetailId;
                    return entity.RequestDetailId;
                }
                return entity.RequestDetailId;
            }
        }
        public IEnumerable<Refactored.Requests> Read()
        {
            using (var db = GetContext())
            {

                var result = db.Requests.ToList().OrderBy(x => x.IsLocked).OrderByDescending(x => x.RequestId)
                            .ToList();

                return result;
            }

        }
        public IEnumerable<Refactored.RequestDetails> ReadDetails(long RequestID)
        {
            using (var db = GetContext())
            {
                var result = db.RequestDetails.ToList().Where(x => x.RequestId == RequestID).OrderByDescending(x => x.RequestDetailId)
                             .ToList();

                return result;
            }
        }
        public void Update(Requests requests)
        {
            using (var db = GetContext())
            {
                var entity = db.Requests.First(s => s.RequestId == requests.RequestId);
                entity.RequestNo = requests.RequestNo == null ? entity.RequestNo : requests.RequestNo;
                //entity.UserIdupdate = 1;

                db.Requests.Attach(entity);
                db.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
            }
        }
        public void UpdateDetails(RequestDetails requestDetails)
        {
            using (var db = GetContext())
            {
                var entity = db.RequestDetails.First(s => s.RequestDetailId == requestDetails.RequestDetailId);


                db.RequestDetails.Attach(entity);
                db.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}