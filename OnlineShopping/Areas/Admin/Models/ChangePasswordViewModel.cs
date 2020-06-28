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
        [Required(ErrorMessage = "الرقم السرى القديم مطلوب")]
        [Display(Name = "الرقم السرى القديم")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "الرقم السرى الجديد مطلوب")]
        [Display(Name = "الرقم السرى الجديد")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "تأكيد الرقم السرى الجديد مطلوب")]
        [Display(Name = "تأكيد الرقم السرى الجديد")]
        [Compare("NewPassword", ErrorMessage = "الرقم السرى و تأكيد الرقم السرى غير متطابقان")]
        public string ConfirmPassword { get; set; }
    }
}
