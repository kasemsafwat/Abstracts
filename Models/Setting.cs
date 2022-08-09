using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Abstracts.Models
{
    public partial class Setting
    {
        public int SettingId { get; set; }
        public string Engineer { get; set; }
        public string GeneralDirector { get; set; }
        public string GeneralManager { get; set; }
        public string Technical { get; set; }
        public string VicePresident { get; set; }
        public string President { get; set; }
        public long? DepartmentId { get; set; }
    }
}
