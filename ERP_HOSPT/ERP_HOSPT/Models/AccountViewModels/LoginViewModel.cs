using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ERP_HOSPT.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "اسم المستخدم")]
        public string Email { get; set; }

        [Required]
        [Display(Name = " كلمه المرور")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "تذكرني؟")]
        public bool RememberMe { get; set; }
    }
}
