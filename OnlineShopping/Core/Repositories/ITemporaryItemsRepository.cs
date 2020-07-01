using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineShopping.Core.DbEntities;

namespace OnlineShopping.Core.Repositories
{
    public interface ITemporaryItemsRepository : IRepository<TemporaryItems, int>
    {
    }
}
