using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShopping.Core.DbEntities;

namespace OnlineShopping.Core.DbEntitiesConfigurations
{
    public class BrandEntityConfiguration:IEntityTypeConfiguration<Brand>
    {

        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.HasMany(c => c.Products).WithOne(c => c.Brand).HasForeignKey(c => c.BrandId)
                .OnDelete(DeleteBehavior.SetNull);
        }   
    }
}
