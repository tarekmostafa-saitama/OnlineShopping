using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShopping.Core.DbEntities;

namespace OnlineShopping.Core.DbEntitiesConfigurations
{
    public class CategoryEntityConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasMany(c => c.Products).WithOne(c => c.Category).HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
