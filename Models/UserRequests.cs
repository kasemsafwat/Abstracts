﻿using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Abstracts.Models
{
    public partial class UserRequests
    {
        public long UserRequestId { get; set; }
        public long? UserId { get; set; }
        public long? RequestId { get; set; }
    }
}
