using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.Areas.Admin.Models
{
    public class UpdateProductViewModel
    {
      
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Description is required.")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [Required(ErrorMessage = "Price is required.")]
        public int? Price { get; set; }
        [Required(ErrorMessage = "Brand is required.")]
        public int? BrandId { get; set; }
        [Required(ErrorMessage = "Category is required.")]
        public int? CategoryId { get; set; }
    }
}
