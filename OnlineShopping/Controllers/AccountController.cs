using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShopping.Areas.Admin.Models;
using OnlineShopping.Core;
using OnlineShopping.Core.DbEntities;
using OnlineShopping.Persistence.ViewModels;

namespace OnlineShopping.Controllers
{

    public class AccountController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly SignInManager<Member> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<Member> _userManager;

        HomeViewModel homeViewModel;

        public AccountController(IUnitOfWork unitOfWork, SignInManager<Member> signInManager, RoleManager<IdentityRole> roleManager,
            UserManager<Member> userManager)
        {
            _unitOfWork = unitOfWork;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _userManager = userManager;

            homeViewModel = new HomeViewModel()
            {
                brands = null,
                products = null,
                categories = unitOfWork.CategoryRepository.GetAll(new string[] { }).ToList()
            };
        }
        [Route("Account/Login")]
        public IActionResult Login()
        {
            ViewBag.e = homeViewModel;
            return View();
        }
        [Route("Account/Login")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewBag.e = homeViewModel;
           

            if (ModelState.IsValid)
            {
                var result =
                    await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);
                if (result.Succeeded)
                {
                    if(string.IsNullOrEmpty(returnUrl))
                        return RedirectToAction("Index", "Home");
                    return Redirect(returnUrl);
                }

                ModelState.AddModelError("", "Invalid login attemp.");
            }

            return View(model);
        }

        [Route("Account/Logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
        [Route("Account/Register")]
        public IActionResult Register()
        {
            ViewBag.e = homeViewModel;

            return View();
        }
        [Route("Account/Register")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!await _roleManager.RoleExistsAsync("Member"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Member"));
            }
            ViewBag.e = homeViewModel;
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new Member()
            {
                UserName = model.UserName
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }

                return View(model);
            }

            await _userManager.AddToRoleAsync(user, "Member");
            return RedirectToAction(nameof(Login));
        }
    }
}