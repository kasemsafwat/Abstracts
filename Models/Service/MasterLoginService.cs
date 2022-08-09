using Abstracts.Models.MasterLoginModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Abstracts.Models.Service
{
    public abstract class MasterLoginService
    {
        public virtual ParadiseMasterLogInDBContext GetContext()
        {
            return new ParadiseMasterLogInDBContext();
        }
    }
}