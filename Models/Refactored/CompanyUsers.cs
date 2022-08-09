using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Abstracts.Models.Refactored
{
    public class CompanyUsers
    {
        public long CompanyUsersId { get; set; }
        public long? CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public long? RequestId { get; set; }
        public string RequestName { get; set; }
    }
}