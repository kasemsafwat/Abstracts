using Abstracts.Models.InterfaceService;
using Abstracts.Models.MasterLoginModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Abstracts.Models.Service
{
    public class ActionService : MasterLoginService, IActionService
    {
        public IEnumerable<Actions> Read()
        {
            return GetAll();
        }
        public IList<Actions> GetAll()
        {
            using (var db = GetContext())
            {
                var result = db.Actions.ToList();

                return result;
            }
        }
    }
}