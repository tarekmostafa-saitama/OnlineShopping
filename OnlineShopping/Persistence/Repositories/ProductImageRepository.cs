using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineShopping.Core.DbEntities;
using OnlineShopping.Core.Repositories;

namespace OnlineShopping.Persistence.Repositories
{
    public class ProductImageRepository : Repository<ProductImage, int>, IProductImageRepository
    {
        private readonly DbContext _context;

        public ProductImageRepository(DbContext context) : base(context)
        {
            _context = context;
        }
    }
}
