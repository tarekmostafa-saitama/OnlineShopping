using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShopping.Core;
using OnlineShopping.Persistence.ViewModels;

namespace OnlineShopping.Controllers
{
    public class ProductController : Controller
    {
        IUnitOfWork unitOfWork;
        HomeViewModel homeViewModel;

        public ProductController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;

        }

        [HttpGet]
        [Route("/Product/Details/{id}")]
        public IActionResult Details(int id)
        {
            var product = unitOfWork.ProductRepository.Get(id, new string[] { });

            homeViewModel = new HomeViewModel()
            {
                brands = unitOfWork.BrandRepository.GetAll(new string[0] { }).ToList(),
                categories = unitOfWork.CategoryRepository.GetAll(new string[0] { }).ToList(),
                products = unitOfWork.ProductRepository.GetAll(new string[] { "ProductImages" }).ToList().Where(item => item.CategoryId == product.CategoryId && item.Id != id).Take(3).ToList()
            };

            homeViewModel.products.Add(product);
            return View(homeViewModel);
        }
    }
}