using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Abstracts.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
            CultureInfo.DefaultThreadCurrentCulture =
                CultureInfo.DefaultThreadCurrentUICulture =
                new CultureInfo("en-US");
        }
        public virtual Models.AbstractsDBContext GetContext()
        {
            return new Models.AbstractsDBContext();
        }
    }
}