using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.Core.DbEntities
{
    public class TemporaryItems
    {
        public int id { get; set; }
        [ForeignKey("MemberId")]
        public string MemberId { get; set; }
        [ForeignKey("ProductId")]
        public int ProductId { get; set; }
        public int Quantity { get; set; } = 1;
        public Member Member { get; set; }
        public Product Product { get; set; }
    }
}
