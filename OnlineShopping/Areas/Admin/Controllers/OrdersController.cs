using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineShopping.Core;
using OnlineShopping.Core.Enums;

namespace OnlineShopping.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrdersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrdersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [Route("Admin/Orders/List")]
        public IActionResult List()
        {
            var orders = _unitOfWork.OrderRepository.GetAll(new[] { "Member" }).ToList();
            return View(orders);
        }
        [Route("Admin/Orders/Delete/{id}")]
        public IActionResult Delete(int id)
        {
            var order = _unitOfWork.OrderRepository.Get(id, new string[0]);
            var orderDetails = _unitOfWork.OrderProductDetailRepository.Find(x => x.OrderId == id, new string[0])
                .ToList();
            if (orderDetails.Any())
            {
                _unitOfWork.OrderProductDetailRepository.DeleteRange(orderDetails);
                _unitOfWork.Complete();
            }

            if (order != null)
            {
                _unitOfWork.OrderRepository.Delete(order);
                _unitOfWork.Complete();
            }

            return RedirectToAction(nameof(List));
        }
        [HttpPost]
        [Route("Admin/Orders/UpdateShippingState")]
        public IActionResult UpdateShippingState(int orderId,ShippingState shippingState)
        {
            var order = _unitOfWork.OrderRepository.Get(orderId, new string[0]);
            if (order != null)
            {
                order.ShippingState = shippingState;
                _unitOfWork.Complete();
            }

            return RedirectToAction(nameof(List));
        }
    }
}