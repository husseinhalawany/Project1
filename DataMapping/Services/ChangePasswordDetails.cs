using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapping.Services
{
    public class ChangePasswordDetails
    {
        public string UserName { get; set; }
        public string Name { get; set; }

        [Required (ErrorMessage=" you have to Enter the Old Paswword")]
        public string OldPassword { get; set; }

        [Required (ErrorMessage="you have to Enter the New password")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Please enter confirm password")]
        [Compare("NewPassword", ErrorMessage = "Must be the same")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassWord { get; set; }

    }
}
