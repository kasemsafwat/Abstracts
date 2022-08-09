using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Abstracts.Controllers
{
    public class Base2Controller : Controller
    {
        public Base2Controller()
        {
            CultureInfo.DefaultThreadCurrentCulture =
                CultureInfo.DefaultThreadCurrentUICulture =
                new CultureInfo("en-US");
        }
        public virtual Models.MasterLoginModel.ParadiseMasterLogInDBContext GetContext()
        {
            return new Models.MasterLoginModel.ParadiseMasterLogInDBContext();
        }
    }
}