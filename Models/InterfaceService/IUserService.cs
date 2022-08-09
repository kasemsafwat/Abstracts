using Abstracts.Models.MasterLoginModel;
using Abstracts.Models.Pbo;
using Abstracts.Models.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracts.Models.InterfaceService
{
    public interface IUserService
    {
        IEnumerable<Refactored.User> Read();
        Users Login(UserModel users);
        Users LoginByUserCode(string userCode);
        List<Actions> GetActionsForUserRoleID(int? roleID);
        long Create(Users users);
        void Update(Users users);
    }
}
