using Abstracts.Models.InterfaceService;
using Abstracts.Models.MasterLoginModel;
using Abstracts.Models.Pbo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Abstracts.Models.Service
{
    public class UserService : MasterLoginService, IUserService
    {
        public Users Login(UserModel users)
        {
            using (var db = GetContext())
            {
                var result = db.Users.ToList()
                     .Where(x => (x.UserName.ToLower() == users.UserName.ToLower() || x.Email!=null && x.Email.ToLower() == users.UserName.ToLower())
                     && x.UserPass == users.Password).FirstOrDefault();
                return result;
            }
        }

        public IEnumerable<Refactored.User> Read()
        {
            return GetAll();
        }
        public List<Refactored.User> GetAll()
        {
            using (var db = GetContext())
            {

                var result = db.Users.ToList()
                     .ToList();



                return result;
            }
        }

        public List<Actions> GetActionsForUserRoleID(int? roleID)
        {
            List<Actions> results = new List<Actions>();
            using (var db = GetContext())
            {
                var result = db.RoleActions.ToList().Where(x => x.RoleId == roleID)
                   .ToList();
                return results;
            }
        }

        public Users LoginByUserCode(string userCode)
        {
            using (var db = GetContext())
            {
                var result = db.Users.ToList()
                     .Where(x => x.UserCode == userCode).FirstOrDefault();
                return result;
            }
        }

        public long Create(Users users)
        {
            using (var db = GetContext())
            {
                var UserExists = db.Users.FirstOrDefault(x => x.Email == users.Email || x.UserName == users.UserName);
                if (UserExists == null)
                {
                    var entity = new Users();
                    var AutoNo = db.Users.Max(x => x.AutoNo).Value;

                    AutoNo++;
                    entity.AutoNo = AutoNo;



                    if ((bool)entity.IsLocked == false && (bool)users.IsLocked)
                        entity.DateLock = DateTime.Now;

                    entity.UserIdlock = 1;

                    db.Users.Add(entity);
                    db.SaveChanges();
                    users.UserId = (int)entity.UserId;
                    return users.UserId;
                }
                else
                {
                    return -1;
                }
            }
        }

        public void Update(Users users)
        {
            using (var db = GetContext())
            {
                var entity = db.Users.Where(x => x.UserId == users.UserId).FirstOrDefault();


                if ((bool)entity.IsLocked == false && (bool)users.IsLocked)
                    entity.DateLock = DateTime.Now;

                entity.UserIdlock = 1;

                db.Users.Attach(entity);
                db.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}