using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Abstracts.Models.Refactored
{
    public class User
    {
        public long UserId { get; set; }
        public string UserCode { get; set; }
        public string UserName { get; set; }
        public string UserFullName { get; set; }
        public string UserPass { get; set; }
        public string Email { get; set; }
        public string UserStatus { get; set; }
        public bool? IsAdmin { get; set; }
        public string Admin { get; set; }
        public bool? IsFrontOffice { get; set; }
        public string FrontOffice { get; set; }
        public bool? IsBackOffice { get; set; }
        public string BackOffice { get; set; }
        public bool? IsExecutiveEngineer { get; set; }
        public string ExecutiveEngineer { get; set; }
        public long? RoleId { get; set; }
        public string Role { get; set; }
        public long? DepartmentId { get; set; }
        public string Department { get; set; }
        public bool? IsLocked { get; set; }
        public string Locked { get; set; }
    }
}