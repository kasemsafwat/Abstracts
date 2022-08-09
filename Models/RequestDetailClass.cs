using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Abstracts.Models
{
    public partial class RequestDetailClass
    {
        public long RequestDetailsClassId { get; set; }
        public long? ItemClassId { get; set; }
    }
}
