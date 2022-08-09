using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Abstracts.Models.MasterLoginModel
{
    public partial class Employees
    {
        public long EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeAddress { get; set; }
        public string EmployeeTitle { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public DateTime? DisableDate { get; set; }
        public string NationalIdno { get; set; }
        public string Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public long? DepartmentId { get; set; }
        public long? ManagerId { get; set; }
        public long? UserId { get; set; }
        public byte[] EmployeePhoto { get; set; }
        public string EmployeeStatus { get; set; }
        public long? UserIdadd { get; set; }
        public DateTime? DateAdd { get; set; }
        public long? UserIdupdate { get; set; }
        public DateTime? DateUpdate { get; set; }
        public bool? IsLocked { get; set; }
        public long? UserIdlock { get; set; }
        public DateTime? DateLock { get; set; }

        public virtual Departments Department { get; set; }
        public virtual Users User { get; set; }
    }
}
