using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineShopping.Core;
using OnlineShopping.Core.Repositories;
using OnlineShopping.Persistence.Context;
using OnlineShopping.Persistence.Repositories;

namespace OnlineShopping.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        public ICategoryRepository CategoryRepository { get; set; }
        public IBrandRepository BrandRepository { get; set; }
        public ICommentRepository CommentRepository { get; set; }
        public IMemberProductFavouriteRepository MemberProductFavouriteRepository { get; set; }
        public IOrderProductDetailRepository OrderProductDetailRepository { get; set; }
        public IOrderRepository OrderRepository { get; set; }
        public IProductImageRepository ProductImageRepository { get; set; }
        public IProductRepository ProductRepository { get; set; }
        public ITemporaryItemsRepository TemporaryItemsRepository { get; set; }

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            CategoryRepository = new CategoryRepository(_dbContext);
            BrandRepository = new BrandRepository(_dbContext);
            CommentRepository = new CommentRepository(_dbContext);
            MemberProductFavouriteRepository = new MemberProductFavouriteRepository(_dbContext);
            OrderProductDetailRepository = new OrderProductDetailRepository(_dbContext);
            OrderRepository = new OrderRepository(_dbContext);
            ProductImageRepository = new ProductImageRepository(_dbContext);
            ProductRepository = new ProductRepository(_dbContext);
            TemporaryItemsRepository = new TemporaryItemsRepository(_dbContext);
        }
        public void Complete()
        {
            _dbContext.SaveChanges();
        }
    }
}
