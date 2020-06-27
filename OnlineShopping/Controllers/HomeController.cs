using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineShopping.Core;
using OnlineShopping.Persistence.ViewModels;

namespace OnlineShopping.Controllers
{
    public class HomeController : Controller
    {
        IUnitOfWork unitOfWork;

        HomeViewModel homeViewModel;


        public HomeController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
            homeViewModel = new HomeViewModel()
            {
                brands = unitOfWork.BrandRepository.GetAll(new string[0] { }).ToList(),
                categories = unitOfWork.CategoryRepository.GetAll(new string[0] { }).ToList(),
                products = unitOfWork.ProductRepository.GetAll(new string[] { "productImages", "Brand", "Category" }).ToList()
            };
        }


        public IActionResult Index()
        {
            return View(homeViewModel);
        }

        public IActionResult Search(String ProductName,int categories)
        {
            var productList = unitOfWork.ProductRepository.Find(oh => oh.Title.StartsWith(ProductName) && oh.CategoryId== categories, new string[] { "productImages", "Brand", "Category" }).ToList();
           
            return View(productList);

        }
    }
}