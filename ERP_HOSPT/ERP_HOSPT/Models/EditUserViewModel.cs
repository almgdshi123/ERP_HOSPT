using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ERP_HOSPT.Models
{
    public class EditUserViewModel
    {
        public EditUserViewModel()
        {
            Claims = new List<string>();
            Roles = new List<string>();
        }
        [Display(Name = "رقم المستخدم")]
        public string Id { get; set; }
        [Display(Name = "اسم المستخدم")]
        [Required]
        public string UserName { get; set; }
        [Display(Name = "البريد الاكتروني")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }



        public List<string> Claims { get; set; }

        public IList<string> Roles { get; set; }
    }
}
