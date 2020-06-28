using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.Areas.Admin.Models
{
    public class ChangePasswordViewModel
    {
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Old password is required.")]

        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "New password is required.")]

        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Confirm new password is required.")]

        [Compare("NewPassword", ErrorMessage = "New password and Confirm new password don't match.")]
        public string ConfirmPassword { get; set; }
    }
}
