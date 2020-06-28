using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.Areas.Admin.Models
{
    public class DashboardViewModel
    {
        public int CategoriesCount { get; set; }
        public int ProductsCount { get; set; }
        public int OrdersCount { get; set; }
        public int MembersCount { get; set; }
        public int BrandCount { get; set; }
    }
}
