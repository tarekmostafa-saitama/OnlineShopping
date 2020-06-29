using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShopping.Core;
using OnlineShopping.Core.DbEntities;
using OnlineShopping.Persistence.ViewModels;

namespace OnlineShopping.Controllers
{
    public class ProductController : Controller
    {
        IUnitOfWork unitOfWork;
        HomeViewModel homeViewModel;
        UserManager<Member> userManager;

        public ProductController(IUnitOfWork _unitOfWork, UserManager<Member> _userManager)
        {
            unitOfWork = _unitOfWork;
            userManager = _userManager;

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
                products = unitOfWork.ProductRepository.Find(x => x.IsDeleted == false, new string[] { "ProductImages" }).ToList().Where(item => item.CategoryId == product.CategoryId && item.Id != id).Take(3).ToList()
            };

            homeViewModel.products.Add(product);
            return View(homeViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Member")]
        [Route("/Product/addToFavourite/{id}")]
        public async Task<IActionResult> addToFavouriteAsync(int id)
        {
            var prd = unitOfWork.ProductRepository.Get(id, new string[] { });
            Member myUser = await userManager.GetUserAsync(User);
            MemberProductFavourite memberProductFavourite = new MemberProductFavourite()
            {
                Member = myUser,
                MemberId = myUser.Id,
                Product = prd,
                ProductId = prd.Id

            };
            unitOfWork.MemberProductFavouriteRepository.Add(memberProductFavourite);
            unitOfWork.Complete();
            return View();
        }
    }
}