using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.Areas.Admin.Models
{
    public class RegisterViewModel
    {
   
        [Required(ErrorMessage = "Username is required.")]
        public string UserName { get; set; }
   
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Confirm password is required.")]
        [Compare("Password", ErrorMessage = "Confirmation password and password don't match.")]
        public string ConfirmPassword { get; set; }
    }
}
