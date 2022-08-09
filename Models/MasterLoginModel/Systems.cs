using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Abstracts.Models.MasterLoginModel
{
    public partial class Systems
    {
        public Systems()
        {
            Actions = new HashSet<Actions>();
        }

        public long SystemId { get; set; }
        public string SystemCode { get; set; }
        public string SystemName { get; set; }
        public string SystemVersion { get; set; }
        public string SystemDescription { get; set; }

        public virtual ICollection<Actions> Actions { get; set; }
    }
}
