using OnlineShopping.Core.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.Persistence.ViewModels
{
    public class HomeViewModel
    {
        public ICollection<Product> products { get; set; }
        public ICollection<Brand> brands { get; set; }
        public ICollection<Category> categories { get; set; }
        public ICollection<Comment> comments { get; set; }

    }
}
