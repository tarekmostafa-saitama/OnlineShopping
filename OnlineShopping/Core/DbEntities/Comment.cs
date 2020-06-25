using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using OnlineShopping.Core.DbEntities;

namespace OnlineShopping.Core.DbEntities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }

        public string MemberId { get; set; }
        [ForeignKey("MemberId")]
        public Member Member { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
