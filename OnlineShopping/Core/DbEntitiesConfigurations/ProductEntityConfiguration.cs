using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShopping.Core.DbEntities;

namespace OnlineShopping.Core.DbEntitiesConfigurations
{
    public class ProductEntityConfiguration  :IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(c => c.Category).WithMany(c => c.Products).HasForeignKey(n => n.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(c => c.Brand).WithMany(c => c.Products).HasForeignKey(n => n.BrandId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
