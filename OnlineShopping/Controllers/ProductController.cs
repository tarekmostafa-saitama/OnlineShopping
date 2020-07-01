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
                comments = unitOfWork.CommentRepository.Find(x=>x.ProductId == id,new string[] { "Member" }).ToList(),
                products = unitOfWork.ProductRepository.Find(x => x.IsDeleted == false, new string[] { "ProductImages" }).ToList().Where(item => item.CategoryId == product.CategoryId && item.Id != id).Take(3).ToList()
            };

            homeViewModel.products.Add(product);
            return View(homeViewModel);
        }

        [Authorize(Roles = "Member")]
        [Route("/Product/AddFavourite/{id}")]
        public async Task<IActionResult> AddFavouriteAsync(int id)
        {
            Member myUser = await userManager.GetUserAsync(User);
            var chkFav = unitOfWork.MemberProductFavouriteRepository.Find(w => w.ProductId == id && w.MemberId == myUser.Id, new string[] { });
           if(chkFav.Count() == 0)
            {
                var prd = unitOfWork.ProductRepository.Get(id, new string[] { });

                MemberProductFavourite memberProductFavourite = new MemberProductFavourite()
                {
                    Member = myUser,
                    MemberId = myUser.Id,
                    Product = prd,
                    ProductId = prd.Id

                };
                unitOfWork.MemberProductFavouriteRepository.Add(memberProductFavourite);
                unitOfWork.Complete();
            }
            
            return RedirectToAction("Details", new { id = id });
        }

        [Authorize(Roles = "Member")]
        [Route("/Product/GetFavourite")]
        public async Task<IActionResult> GetFavouriteAsync()
        {
            Member myUser = await userManager.GetUserAsync(User);
            IEnumerable<MemberProductFavourite> favprds = unitOfWork.MemberProductFavouriteRepository.Find(x => x.MemberId == myUser.Id && x.Product.IsDeleted == false, new string[] { "Product" });
            foreach (var item in favprds)
            {
                item.Product.ProductImages = unitOfWork.ProductImageRepository.Find(w => w.ProductId == item.ProductId, new string[] { }).ToList();
            }
            homeViewModel = new HomeViewModel()
            {
                brands = null,
                products = null,
                categories = unitOfWork.CategoryRepository.GetAll(new string[] { }).ToList()
            };
            ViewBag.e = homeViewModel;
            return View(favprds);
        }

        [HttpPost]
        [Authorize(Roles = "Member")]
        [Route("/Product/Delete/{id}")]
        public IActionResult Delete(int id)
        {
            var obj = unitOfWork.MemberProductFavouriteRepository.Get(id,new string[] { });
            unitOfWork.MemberProductFavouriteRepository.Delete(obj);
            unitOfWork.Complete();
            return RedirectToAction("GetFavourite","Product");
        }

        [Authorize(Roles = "Member")]
        [Route("/Product/AddComment/{id}")]
        [HttpPost]
        public async Task<IActionResult> AddCommentAsync(string Content, int id)
        {
            Member myUser = await userManager.GetUserAsync(User);
            var product = unitOfWork.ProductRepository.Find(i => i.Id == id && i.IsDeleted == false, new string[] { "comments" });
            var data = new Comment();
            data.Content = Content;
            data.Date = System.DateTime.Now;
            data.ProductId = id;
            data.Member = myUser;
            unitOfWork.CommentRepository.Add(data);
            unitOfWork.Complete();
            ViewData["data"] = data;
            return RedirectToAction("Details",new {id=id} );
        }
       
    }


}
        