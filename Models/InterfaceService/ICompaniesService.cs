using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracts.Models.InterfaceService
{
    public interface ICompaniesService
    {
        IEnumerable<Companies> Read();
        void Create(Companies companies);
        void Update(Companies companies);
        void Delete(Companies companies);
    }
}
