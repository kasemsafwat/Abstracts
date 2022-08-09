using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracts.Models.InterfaceService
{
    public interface IUnitService
    {
        IEnumerable<Units> Read();
        int Create(Units units);
        void Update(Units units);
        void Delete(Units units);
    }
}
