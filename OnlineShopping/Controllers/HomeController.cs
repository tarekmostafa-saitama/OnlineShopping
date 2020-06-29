using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineShopping.Core;
using OnlineShopping.Core.DbEntities;
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

        public IActionResult GetCategoryItems(int id)
        {
            homeViewModel = new HomeViewModel()
            {
                brands = unitOfWork.BrandRepository.GetAll(new string[0] { }).ToList(),
                categories = unitOfWork.CategoryRepository.GetAll(new string[0] { }).ToList(),
                products = unitOfWork.ProductRepository.Find(i => i.CategoryId == id, new string[0] { }).ToList()
            };

            return View(homeViewModel);
        }

        public IActionResult Search(String ProductName, int categories)
        {

            homeViewModel = new HomeViewModel()
            {
                brands = unitOfWork.BrandRepository.GetAll(new string[0] { }).ToList(),
                categories = unitOfWork.CategoryRepository.GetAll(new string[0] { }).ToList(),
                products = unitOfWork.ProductRepository.Find(oh => oh.Title.Contains(ProductName) && oh.CategoryId == categories, new string[] { "productImages", "Brand", "Category" }).ToList()
            };

            return View("GetCategoryItems", homeViewModel);

        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            var product = unitOfWork.ProductRepository.Get(id, new string[] { });

            homeViewModel = new HomeViewModel()
            {
                brands = unitOfWork.BrandRepository.GetAll(new string[0] { }).ToList(),
                categories = unitOfWork.CategoryRepository.GetAll(new string[0] { }).ToList(),
                products = unitOfWork.ProductRepository.GetAll(new string[0] { }).ToList().Where(item => item.CategoryId == product.CategoryId && item.Id != id).Take(3).ToList()
            };

            homeViewModel.products.Add(product);
            return View(homeViewModel);
        }


    }
}