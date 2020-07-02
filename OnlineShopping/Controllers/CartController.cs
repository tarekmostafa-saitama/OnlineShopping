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
    public class CartController : Controller
    {
        IUnitOfWork unitOfWork;
        HomeViewModel homeViewModel;
        UserManager<Member> userManager;

        public CartController(IUnitOfWork _unitOfWork, UserManager<Member> _userManager)
        {
            unitOfWork = _unitOfWork;
            userManager = _userManager;

        }
        public IActionResult Index()
        {
            return View();
        }


        [Authorize(Roles = "Member")]
        [Route("/cart/addTocart/{id}")]
        public async Task<IActionResult> addTocartAsync(int id)
        {
            Member myUser = await userManager.GetUserAsync(User);
            var chkAdd = unitOfWork.TemporaryItemsRepository.Find(w => w.ProductId == id && w.MemberId == myUser.Id, new string[] { });
            if(chkAdd.Count() == 0)
            {
                TemporaryItems temp = new TemporaryItems()
                {
                    ProductId = id,
                    MemberId = myUser.Id
                };
                unitOfWork.TemporaryItemsRepository.Add(temp);
                unitOfWork.Complete();
            }
           
            return RedirectToAction("Details" , "product" , new { id = id});
        }

        [Authorize(Roles = "Member")]
        [Route("/Cart/DispalyCart")]
        public async Task<IActionResult> DisplayCart()
        {
            Member myUser = await userManager.GetUserAsync(User);
            var productIDs = unitOfWork.TemporaryItemsRepository.Find(item => item.MemberId == myUser.Id,new string[] { }).ToList();
            List<Product> products = new List<Product>();
            foreach(var item in productIDs)
            {
                products.AddRange(unitOfWork.ProductRepository.Find(i => i.Id == item.ProductId, new string[] { "ProductImages", "OrderProductDetails" }).Distinct());
            }
            return View(products);
        }

        [Route("/Cart/Delete/{id}")]

        public async Task<IActionResult> Delete(int id)
        {
            Member myUser = await userManager.GetUserAsync(User);
            var product = unitOfWork.TemporaryItemsRepository.Find(item => item.ProductId == id && myUser.Id == item.MemberId, new string[] { }).ToList();
            unitOfWork.TemporaryItemsRepository.DeleteRange(product);
            unitOfWork.Complete();
            return RedirectToAction("DisplayCart","Cart");
        }

    }


}