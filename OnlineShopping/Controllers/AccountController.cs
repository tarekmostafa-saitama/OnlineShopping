using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineShopping.Core;
using OnlineShopping.Persistence.ViewModels;

namespace OnlineShopping.Controllers
{

    public class AccountController : Controller
    {
        IUnitOfWork unitOfWork;
        HomeViewModel homeViewModel;

        public AccountController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
            homeViewModel = new HomeViewModel()
            {
                brands = null,
                products = null,
                categories = unitOfWork.CategoryRepository.GetAll(new string[] { }).ToList()
            };
        }

        public IActionResult Login()
        {
            ViewBag.e = homeViewModel;
            return View();
        }

        public IActionResult Register()
        {
            ViewBag.e = homeViewModel;
            return View();
        }
    }
}