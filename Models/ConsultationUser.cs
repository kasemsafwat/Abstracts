using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Abstracts.Models
{
    public partial class ConsultationUser
    {
        public long ConsultationUserId { get; set; }
        public string ConsultationName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public long? UserAdd { get; set; }
        public DateTime? DateAdded { get; set; }
        public long? UserUpdate { get; set; }
        public DateTime? DateUpdated { get; set; }
    }
}
