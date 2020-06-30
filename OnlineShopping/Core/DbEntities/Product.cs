using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using OnlineShopping.Core.DbEntities;

namespace OnlineShopping.Core.DbEntities
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public int Price { get; set; }
        public int BrandId { get; set; }
        [ForeignKey("BrandId")]
        public Brand Brand { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        public ICollection<ProductImage> ProductImages { get; set; }
        public ICollection<OrderProductDetail> OrderProductDetails { get; set; }
        public ICollection<MemberProductFavourite> MemberProductFavourites { get; set; }
        public ICollection<Comment> comments { get; set; }

    }
}
