using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Abstracts.Models.MasterLoginModel
{
    public partial class RoleActions
    {
        public long RoleActionId { get; set; }
        public long? RoleId { get; set; }
        public long? ActionId { get; set; }

        public virtual Actions Action { get; set; }
        public virtual Roles Role { get; set; }
    }
}
