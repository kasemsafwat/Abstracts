using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Abstracts.Models.MasterLoginModel
{
    public partial class Roles
    {
        public Roles()
        {
            RoleActions = new HashSet<RoleActions>();
            Users = new HashSet<Users>();
        }

        public long RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }

        public virtual ICollection<RoleActions> RoleActions { get; set; }
        public virtual ICollection<Users> Users { get; set; }
    }
}
