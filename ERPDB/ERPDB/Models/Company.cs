using System;
using System.Collections.Generic;

namespace ERPDB.Models
{
    public partial class Company
    {
        public string CompanyId { get; set; }
        public string CompanName { get; set; }
        public int? State { get; set; }
        public string Addrees { get; set; }
        public string Email { get; set; }
    }
}
