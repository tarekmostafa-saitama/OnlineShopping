using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace OnlineShopping.Areas.Admin.Models
{
    public class AddBrandViewModel
    {
        [Required(ErrorMessage = "Brand name is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Brand image is required.")]
        public IFormFile Image { get; set; }
    }
}
