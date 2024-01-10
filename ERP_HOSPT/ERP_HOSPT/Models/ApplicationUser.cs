using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP_HOSPT.Data;
using Microsoft.AspNetCore.Identity;

namespace ERP_HOSPT.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public byte[] Image { get; set; }
        public string CompanyId { get; set; }
        public Company Company { get; set; }
      
    }
}
