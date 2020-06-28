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
    public class BrandsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        public BrandsController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }
        [Route("Admin/Brands/List")]
        public IActionResult List()
        {
            var brands = _unitOfWork.BrandRepository.GetAll(new[] {"Products"}).ToList();
            return View(brands);
        }
        [Route("Admin/Brands/Add")]
        public IActionResult Add()
        {
            
            return View(new AddBrandViewModel());
        }
        [Route("Admin/Brands/Add")]
        [HttpPost]
        public IActionResult Add(AddBrandViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var newName = Guid.NewGuid();
            var path = _hostEnvironment.WebRootPath + "/Uploads/" + newName + model.Image.FileName;
            FileStream stream = new FileStream(path, FileMode.Create);
            model.Image.CopyTo(stream);
            stream.Close();

            var brand = new Brand()
            {
                Name = model.Name,
                ImagePath = newName + model.Image.FileName
            };
            _unitOfWork.BrandRepository.Add(brand);
            _unitOfWork.Complete();
            return RedirectToAction(nameof(List));
        }
        [Route("Admin/Brands/Delete")]
        public IActionResult Delete(int id)
        {
            var brand = _unitOfWork.BrandRepository.Find(x => x.Id == id, new[] { "Products" }).FirstOrDefault();
            if (brand == null)
            {
                ViewBag.Error = "Brand Not Found";
                return View("Error");
            }

            if (brand.Products.Count != 0)
            {
                ViewBag.Error = "Can't delete brand while it have existing products, Please delete products first.";
                return View("Error");
            }
            _unitOfWork.BrandRepository.Delete(brand);
            _unitOfWork.Complete();
         
            return RedirectToAction(nameof(List));
        }
    }
}