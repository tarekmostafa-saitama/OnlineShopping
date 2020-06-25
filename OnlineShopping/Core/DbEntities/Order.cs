using System;
using System.Collections.Generic;
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
        public string Address { get; set; }
        public ShippingState ShippingState { get; set; }

        public string MemberId { get; set; }
        [ForeignKey("MemberId")]
        public Member Member { get; set; }

        public ICollection<OrderProductDetail> OrderProductDetails { get; set; }
    }
}
