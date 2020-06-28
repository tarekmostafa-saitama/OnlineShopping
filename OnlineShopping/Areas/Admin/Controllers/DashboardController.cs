using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShopping.Areas.Admin.Models;
using OnlineShopping.Core;
using OnlineShopping.Core.DbEntities;

namespace OnlineShopping.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<Member> _userManager;

        public DashboardController(IUnitOfWork unitOfWork,UserManager<Member> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        [Route("Admin/Dashboard")]
        public IActionResult Dashboard()
        {
           
           
            var model = new DashboardViewModel()
            {
                CategoriesCount = _unitOfWork.CategoryRepository.Count(null),
                MembersCount = _userManager.Users.Count(),
                OrdersCount = _unitOfWork.OrderRepository.Count(null),
                ProductsCount = _unitOfWork.ProductRepository.Count(null),
                BrandCount = _unitOfWork.BrandRepository.Count(null)

            };
            return View(model);
        }
    }
}