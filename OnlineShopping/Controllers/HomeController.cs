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
        public IActionResult GetProduct(int id)
        {
            var Category = unitOfWork.CategoryRepository.Get(id, new string[0] { });
            ViewData["Categories"] = unitOfWork.CategoryRepository.GetAll( new string[0] { });
            ViewData["Brands"] = unitOfWork.BrandRepository.GetAll(new string[0] { });

            var Products = unitOfWork.ProductRepository.Find(i => i.CategoryId == Category.Id, new string[0] { }).ToList();
            return View(Products);
        }
    }
}