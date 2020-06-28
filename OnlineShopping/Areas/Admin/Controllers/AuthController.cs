using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShopping.Areas.Admin.Models;
using OnlineShopping.Core.DbEntities;

namespace OnlineShopping.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthController : Controller
    {
        private readonly SignInManager<Member> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<Member> _userManager;
 


        public AuthController(SignInManager<Member> signInManager, RoleManager<IdentityRole> roleManager,
            UserManager<Member> userManager)
        {
            _signInManager = signInManager;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [Route("Admin/Auth/Login")]
        public IActionResult Login()
        {
            return View();
        }

        [Route("Admin/Auth/Login")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {

            if (!_userManager.Users.Any())
            {
                var user = new Member() { UserName = "admin", Email = "test@gmail.com" };
                var result = await _userManager.CreateAsync(user, "123456");

                await _roleManager.CreateAsync(new IdentityRole("Admin"));
                await _userManager.AddToRoleAsync(user, "Admin");
            }

            if (ModelState.IsValid)
            {
                var result =
                    await _signInManager.PasswordSignInAsync(model.UserName, model.Password,false,false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Dashboard", "Dashboard");
                }

                ModelState.AddModelError("", "Invalid login attemp.");
            }

            return View(model);
        }

        [Route("Admin/Auth/Logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }

        [Route("Admin/Auth/ChangePassword")]
        [Authorize]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [Route("Admin/Auth/ChangePassword")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);

                // ChangePasswordAsync changes the user password
                var result = await _userManager.ChangePasswordAsync(user,
                    model.CurrentPassword, model.NewPassword);

                // The new password did not meet the complexity rules or
                // the current password is incorrect. Add these errors to
                // the ModelState and rerender ChangePassword view
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }

                    return View(model);
                }

                // Upon successfully changing the password refresh sign-in cookie
                await _signInManager.RefreshSignInAsync(user);
                return RedirectToAction("Dashboard", "Dashboard");
            }

            return View(model);
        }
    }
}