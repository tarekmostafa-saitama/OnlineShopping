using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineShopping.Core.Repositories;

namespace OnlineShopping.Core
{
    public interface IUnitOfWork
    {
        ICategoryRepository CategoryRepository { get; set; }
        IBrandRepository BrandRepository { get; set; }
        ICommentRepository CommentRepository { get; set; }
        IMemberProductFavouriteRepository MemberProductFavouriteRepository { get; set; }
        IOrderProductDetailRepository OrderProductDetailRepository { get; set; }
        IOrderRepository OrderRepository { get; set; }
        IProductImageRepository ProductImageRepository { get; set; }
        IProductRepository ProductRepository { get; set; }
        ITemporaryItemsRepository TemporaryItemsRepository { get; set; }
        void Complete();
    }
}
