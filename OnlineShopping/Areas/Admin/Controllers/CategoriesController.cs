using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShopping.Core;
using OnlineShopping.Core.DbEntities;

namespace OnlineShopping.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoriesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoriesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [Route("Admin/Categories/List")]
        public IActionResult List()
        {
            var categories = _unitOfWork.CategoryRepository.GetAll(new[] {"Products"}).ToList();
            return View(categories);
        }
        [HttpPost]
        [Route("Admin/Categories/Add")]
        public IActionResult AddCategory(string name)
        {
            if(string.IsNullOrEmpty(name))
            {
                ViewBag.Error = "Category name can't be null";
                return View("Error");
            }
            _unitOfWork.CategoryRepository.Add(new Category(){Name = name});
            _unitOfWork.Complete();
            return RedirectToAction(nameof(List));
        }
        [Route("Admin/Categories/Delete")]
        public IActionResult Delete(int id)
        {
            var category = _unitOfWork.CategoryRepository.Find(x => x.Id == id, new []{"Products"}).FirstOrDefault();
            if (category == null)
            {
                ViewBag.Error = "Category Not Found";
                return View("Error");
            }

            if (category.Products.Count != 0)
            {
                ViewBag.Error = "Can't delete category while it have existing products, Please delete products first.";
                return View("Error");
            }
            _unitOfWork.CategoryRepository.Delete(category);
            _unitOfWork.Complete();
            return RedirectToAction(nameof(List));
        }
    }
}