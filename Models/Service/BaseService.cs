using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Abstracts.Models.Service
{
    public abstract class BaseService
    {
        public virtual AbstractsDBContext GetContext()
        {
            return new AbstractsDBContext();
        }
    }
}