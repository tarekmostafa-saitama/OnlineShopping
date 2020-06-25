using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.Core.DbEntities
{
    public class MemberProductFavourite
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        public string MemberId { get; set; }
        [ForeignKey("MemberId")]
        public Member Member { get; set; }
    }
}
