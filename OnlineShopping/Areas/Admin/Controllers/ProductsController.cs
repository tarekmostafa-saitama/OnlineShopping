using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OnlineShopping.Areas.Admin.Models;
using OnlineShopping.Core;
using OnlineShopping.Core.DbEntities;

namespace OnlineShopping.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductsController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }
        [Route("Admin/Products/Add")]
        public IActionResult Add()
        {
            ViewBag.Categories = _unitOfWork.CategoryRepository.GetAll(new string[0]).ToList();
            ViewBag.Brands = _unitOfWork.BrandRepository.GetAll(new string[0]).ToList();
            return View(new AddProductViewModel());
        }
        [Route("Admin/Products/Add")]
        [HttpPost]
        public IActionResult Add(AddProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _unitOfWork.CategoryRepository.GetAll(new string[0]).ToList();
                ViewBag.Brands = _unitOfWork.BrandRepository.GetAll(new string[0]).ToList();
                return View(model);
            }

            var product = new Product()
            {
                Title = model.Title,
                Description = model.Description,
                Price = model.Price ?? default,
                BrandId = model.BrandId ?? default,
                CategoryId = model.CategoryId ?? default
            };
            _unitOfWork.ProductRepository.Add(product);
            _unitOfWork.Complete();
            foreach (var image in model.Images)
            {
                var newName = Guid.NewGuid();
                var path = _hostEnvironment.WebRootPath + "/Uploads/" + newName + image.FileName;
                FileStream stream = new FileStream(path, FileMode.Create);
                image.CopyTo(stream);
                stream.Close();

                var productImage = new ProductImage()
                {
                    Path = newName + image.FileName,
                    ProductId = product.Id
                };
                _unitOfWork.ProductImageRepository.Add(productImage);
                _unitOfWork.Complete();
            }
            return RedirectToAction("Add");
        }
        [Route("Admin/Category/{categoryId}/Products/List")]
        public IActionResult ListOverCategories(int categoryId)
        {
            var category = _unitOfWork.CategoryRepository.Find(x => x.Id == categoryId, new[] {"Products"})
                .FirstOrDefault();
            if (category == null)
            {
                ViewBag.Error = "Category not found.";
                return View("Error");
            }
            return View(category);
        }
        [Route("Admin/Brand/{brandId}/Products/List")]
        public IActionResult ListOverBrands(int brandId)
        {
            var brand = _unitOfWork.BrandRepository.Find(x => x.Id == brandId, new[] { "Products" })
                .FirstOrDefault();
            if (brand == null)
            {
                ViewBag.Error = "Brand not found.";
                return View("Error");
            }
            return View(brand);
        }
    }
}