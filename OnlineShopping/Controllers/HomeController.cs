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
                products = unitOfWork.ProductRepository.Find(x=>x.IsDeleted == false,new string[] { "ProductImages", "Brand", "Category" }).ToList()
            };
        }
        [Route("~/")]
        [Route("/Home")]
        public IActionResult Index()
        {
            var ran = new Random();
            ViewData["dayDeals"] = unitOfWork.ProductRepository.GetAll(new string[1] { "ProductImages" }).OrderBy(i => ran.Next()).Take(6).ToList();
            return View(homeViewModel);
        }
        [Route("/Home/GetCategoryItems/{id}")]
        public IActionResult GetCategoryItems(int id)
        {
            homeViewModel = new HomeViewModel()
            {
                brands = unitOfWork.BrandRepository.GetAll(new string[0] { }).ToList(),
                categories = unitOfWork.CategoryRepository.GetAll(new string[0] { }).ToList(),
                products = unitOfWork.ProductRepository.Find(i => i.CategoryId == id && i.IsDeleted == false, new string[0] { }).ToList()
            };

            return View(homeViewModel);
        }
        [Route("/Home/Search")]
        public IActionResult Search(String ProductName, int categories)
        {
            homeViewModel = new HomeViewModel()
            {
                brands = unitOfWork.BrandRepository.GetAll(new string[0] { }).ToList(),
                categories = unitOfWork.CategoryRepository.GetAll(new string[0] { }).ToList(),
                products = unitOfWork.ProductRepository.Find(i => i.Title.Contains(ProductName) && i.CategoryId == categories && i.IsDeleted == false, new string[] { "ProductImages", "Brand", "Category" }).ToList()
            };
            return View("GetCategoryItems", homeViewModel);
        }
    }
}