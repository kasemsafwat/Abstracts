using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Abstracts.Models.Refactored
{
    public class Companies
    {
        public long CompanyId { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyName { get; set; }
        public string TaxRecordNo { get; set; }
        public string InsuranceNo { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public bool? IsLocked { get; set; }
        public string Locked { get; set; }

    }
}