
using System;
using Microsoft.EntityFrameworkCore;
using OnlineShopping.Core.DbEntities;
using OnlineShopping.Core.Repositories;
namespace OnlineShopping.Persistence.Repositories
{
    public class TemporaryItemsRepository : Repository<TemporaryItems, int>, ITemporaryItemsRepository
    {
        private readonly DbContext _context;

        public TemporaryItemsRepository(DbContext context) : base(context)
        {
            _context = context;
        }
    }
}
