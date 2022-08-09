using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Abstracts.Models.MasterLoginModel
{
    public partial class Users
    {
        public Users()
        {
            Employees = new HashSet<Employees>();
        }

        public long UserId { get; set; }
        public string UserCode { get; set; }
        public string UserName { get; set; }
        public string UserFullName { get; set; }
        public string UserPass { get; set; }
        public string Email { get; set; }
        public string UserStatus { get; set; }
        public bool? IsAdmin { get; set; }
        public bool? IsFrontOffice { get; set; }
        public bool? IsBackOffice { get; set; }
        public long? RoleId { get; set; }
        public bool? IsLocked { get; set; }
        public long? UserIdlock { get; set; }
        public DateTime? DateLock { get; set; }
        public long? AutoNo { get; set; }
        public bool? IsExecutiveEngineer { get; set; }
        public long? DepartmentId { get; set; }
        public long? EmployeeId { get; set; }

        public virtual Roles Role { get; set; }
        public virtual ICollection<Employees> Employees { get; set; }
    }
}
