using Abstracts.Models.InterfaceService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Abstracts.Models.Service
{
    public class UnitService : BaseService, IUnitService
    {
        public IEnumerable<Units> Read()
        {
            return GetAll();
        }
        public IList<Units> GetAll()
        {
            using (var db = GetContext())
            {
                var result = db.Units.ToList().OrderBy(x => x.IsLocked)
                             .Select(unit =>
                             {
                                 return new Units
                                 {
                                     UnitId = unit.UnitId,
                                     UnitName = unit.UnitName,
                                     IsLocked = unit.IsLocked
                                 };
                             }).ToList();

                return result;
            }
        }
        public int Create(Units units)
        {
            using (var db = GetContext())
            {
                var entity = db.Units.Where(x => x.UnitName == units.UnitName).FirstOrDefault();
                if (entity == null)
                {
                    entity = new Units();
                    entity.UnitName = units.UnitName;

                    db.Units.Add(entity);
                    db.SaveChanges();


                    units.UnitId = (int)entity.UnitId;
                    return 1;
                }
                return -1;
            }
        }

        public void Update(Units units)
        {
            using (var db = GetContext())
            {
                var entity = db.Units.First(s => s.UnitId == units.UnitId);

                db.Units.Attach(entity);
                db.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void Delete(Units units)
        {
            using (var db = GetContext())
            {
                var entity = db.Units.First(s => s.UnitId == units.UnitId);
                db.SaveChanges();
            }
        }
    }
}