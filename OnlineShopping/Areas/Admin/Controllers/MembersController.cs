using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShopping.Core;
using OnlineShopping.Core.DbEntities;

namespace OnlineShopping.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MembersController : Controller
    {
    
        private readonly UserManager<Member> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly RoleManager<IdentityRole> _roleManager;

        public MembersController(UserManager<Member> userManager, IUnitOfWork unitOfWork,RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _roleManager = roleManager;
        }
        [Route("Admin/Members/List")]
        public async Task<IActionResult> List()
        {
            var members = await _userManager.GetUsersInRoleAsync("Member");
            return View(members);
        }
    }
}