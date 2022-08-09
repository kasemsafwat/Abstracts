using Abstracts.Models.InterfaceService;
using Abstracts.Models.MasterLoginModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Abstracts.Models.Service
{
    public class RoleService : MasterLoginService, IRoleService
    {
        public void Create(Roles roles)
        {
            using (var db = GetContext())
            {
                var entity = new Roles();

                db.Roles.Add(entity);
                db.SaveChanges();

                roles.RoleId = (int)entity.RoleId;
            }
        }

        public IEnumerable<Roles> Read()
        {
            return GetAll();
        }
        public IList<Roles> GetAll()
        {
            using (var db = GetContext())
            {
                var result = db.Roles.ToList();

                return result;
            }
        }

        public void Update(Roles roles)
        {
            using (var db = GetContext())
            {
                var entity = db.Roles.First(s => s.RoleId == roles.RoleId);

                db.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}