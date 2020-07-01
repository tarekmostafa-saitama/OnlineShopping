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
            TemporaryItems temp = new TemporaryItems()
            {
                ProductId = id,
                MemberId = myUser.Id
            };
            unitOfWork.TemporaryItemsRepository.Add(temp);
            unitOfWork.Complete();
            return RedirectToAction("Details" , "product" , new { id = id});
        }



    }

    
}