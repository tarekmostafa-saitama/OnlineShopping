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
            homeViewModel = new HomeViewModel()
            {
                brands = null,
                categories = unitOfWork.CategoryRepository.GetAll(new string[0] { }).ToList(),
                comments = null,
                products = products
            };
            ViewData["Order"] = new Order();
            ViewData["TempProd"] = unitOfWork.TemporaryItemsRepository.Find(ww => ww.MemberId == myUser.Id , new string[] { }).ToList();
            return View(homeViewModel);
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
        [Authorize(Roles = "Member")]
        [Route("/Cart/submitOrder")]
        [HttpPost]
        public async Task<IActionResult> submitOrder(Order order)
        {
            Member myUser = await userManager.GetUserAsync(User);
            if (ModelState.IsValid)
            {
                order.Date = DateTime.Now;
                order.MemberId = myUser.Id;
                order.ShippingState = Core.Enums.ShippingState.inProgress;
                unitOfWork.OrderRepository.Add(order);
                unitOfWork.Complete();

                var userTempPrds = unitOfWork.TemporaryItemsRepository.Find(ww => ww.MemberId == myUser.Id, new string[] { "Product" }).ToList();
                foreach (var item in userTempPrds)
                {
                    unitOfWork.OrderProductDetailRepository.Add(new OrderProductDetail
                    {
                        OrderId = order.Id,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        Price = item.Quantity * item.Product.Price
                    });

                }
                unitOfWork.Complete();
                unitOfWork.TemporaryItemsRepository.DeleteRange(userTempPrds);
                unitOfWork.Complete();

            }
            return RedirectToAction("DisplayCart", "Cart");
        }

        [HttpPost]
        [Route("/Cart/updateQuantity")]
        public async Task<IActionResult> updateQuantityAsync(List<int> id, List<int> Quantity)
        {
            Member myUser = await userManager.GetUserAsync(User);
            var tempPrd = unitOfWork.TemporaryItemsRepository.Find(w => w.MemberId == myUser.Id , new string[] { }).ToList();
            for(int i = 0; i < tempPrd.Count(); i++)
            {
                tempPrd[i].Quantity = Quantity[i];
            }
            unitOfWork.Complete();
            return RedirectToAction("DisplayCart", "Cart");
        }

    }


}