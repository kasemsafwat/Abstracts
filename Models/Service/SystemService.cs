using Abstracts.Models.MasterLoginModel;
using Abstracts.Models.InterfaceService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Abstracts.Models.Service
{
    public class SystemService : MasterLoginService, ISystemService
    {
        public Systems Read()
        {
            using (var db = GetContext())
            {
                var result = db.Systems
                     .FirstOrDefault();
                return result;
            }
        }
    }
}