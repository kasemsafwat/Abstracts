using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Abstracts.Models.MasterLoginModel
{
    public partial class Actions
    {
        public Actions()
        {
            RoleActions = new HashSet<RoleActions>();
        }

        public long ActionId { get; set; }
        public long? SystemId { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public long? ActionLevel { get; set; }
        public string Url { get; set; }
        public bool? IsParent { get; set; }
        public long? ParentActionId { get; set; }
        public string ActionDescription { get; set; }

        public virtual Systems System { get; set; }
        public virtual ICollection<RoleActions> RoleActions { get; set; }
    }
}
