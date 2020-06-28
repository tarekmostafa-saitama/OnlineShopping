using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.Areas.Admin.Models
{
    public class LoginViewModel
    {

        [Required(ErrorMessage = "User name is required.")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }

    }
}
