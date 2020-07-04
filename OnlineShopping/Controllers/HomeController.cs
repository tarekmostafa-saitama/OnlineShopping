using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
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
        UserManager<Member> userManager;

        public HomeController(IUnitOfWork _unitOfWork, UserManager<Member> _userManager)
        {
            unitOfWork = _unitOfWork;
            userManager = _userManager;

            homeViewModel = new HomeViewModel()
            {
                brands = unitOfWork.BrandRepository.GetAll(new string[0] { }).ToList(),
                categories = unitOfWork.CategoryRepository.GetAll(new string[0] { }).ToList(),
                products = unitOfWork.ProductRepository.Find(x=>x.IsDeleted == false,new string[] { "ProductImages", "Brand", "Category" }).ToList()
            };
        }
        [Route("~/")]
        [Route("/Home")]
        public async Task<IActionResult> IndexAsync()
        {
            Member myUser = await userManager.GetUserAsync(User);
            if (User.Identity.IsAuthenticated )
            {
                ViewBag.CartCount = unitOfWork.TemporaryItemsRepository.GetAll(new string[] { }).Where(x => x.MemberId == myUser.Id).Count();
                ViewBag.FavCount = unitOfWork.MemberProductFavouriteRepository.GetAll(new string[] { }).Where(x => x.MemberId == myUser.Id).Count();
            }

            return View(homeViewModel);
        }
        [Route("/Home/GetCategoryItems/{id}")]
        public async Task<IActionResult> GetCategoryItemsAsync(int id)
        {
            Member myUser = await userManager.GetUserAsync(User);
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.CartCount = unitOfWork.TemporaryItemsRepository.GetAll(new string[] { }).Where(x => x.MemberId == myUser.Id).Count();
                ViewBag.FavCount = unitOfWork.MemberProductFavouriteRepository.GetAll(new string[] { }).Where(x => x.MemberId == myUser.Id).Count();
            }

            homeViewModel = new HomeViewModel()
            {
                brands = unitOfWork.BrandRepository.GetAll(new string[0] { }).ToList(),
                categories = unitOfWork.CategoryRepository.GetAll(new string[0] { }).ToList(),
                products = unitOfWork.ProductRepository.Find(i => i.CategoryId == id && i.IsDeleted == false, new string[0] { }).ToList()
            };

            return View(homeViewModel);
        }
        [Route("/Home/Search")]
        public async Task<IActionResult> SearchAsync(String ProductName, int categories)
        {
            Member myUser = await userManager.GetUserAsync(User);

            if (User.Identity.IsAuthenticated)
            {
                ViewBag.CartCount = unitOfWork.TemporaryItemsRepository.GetAll(new string[] { }).Where(x => x.MemberId == myUser.Id).Count();
                ViewBag.FavCount = unitOfWork.MemberProductFavouriteRepository.GetAll(new string[] { }).Where(x => x.MemberId == myUser.Id).Count();
            }

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