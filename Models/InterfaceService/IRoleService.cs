using Abstracts.Models.MasterLoginModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracts.Models.InterfaceService
{
    public interface IRoleService
    {
        IEnumerable<Roles> Read();
        void Create(Roles roles);
        void Update(Roles roles);
    }
}
