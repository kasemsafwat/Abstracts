using Abstracts.Models.InterfaceService;
using Abstracts.Models.MasterLoginModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Abstracts.Models.Service
{
    public class DepartmentService : MasterLoginService, IDepartmentService
    {
        public IEnumerable<Departments> Read()
        {
            using (var db = GetContext())
            {
                var result = db.Departments.ToList();

                return result;
            }
        }
    }
}