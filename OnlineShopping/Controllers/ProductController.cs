using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineShopping.Core;

namespace OnlineShopping.Controllers
{
    public class ProductController : Controller
    {
        IUnitOfWork DB;
        public ProductController(IUnitOfWork _DB)
        {
            DB = _DB;
        }
        public IActionResult Index()
        {
            var prds = DB.ProductRepository.GetAll(new string[0] { });
            return View();
        }
    }
}