using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using OnlineShopping.Core.DbEntities;
using OnlineShopping.Core.Enums;

namespace OnlineShopping.Core.DbEntities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        [Required]
        public string Address { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^[01][0-2]{1}[0-9]\d{8}$", ErrorMessage = "Not a valid phone number")]
        [Required(ErrorMessage ="Phone number is required")]
        
        public string Telephone { get; set; }
        public ShippingState ShippingState { get; set; }
    
        [ForeignKey("MemberId")]
        public string MemberId { get; set; }
        public Member Member { get; set; }

        public ICollection<OrderProductDetail> OrderProductDetails { get; set; }
    }
}
