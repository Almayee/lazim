﻿using Core.Models;
using FilmProject.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FilmProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        //public async Task<IActionResult> CreateRoles()
        //{
        //    IdentityRole role1 = new IdentityRole("Member");
        //    IdentityRole role2 = new IdentityRole("Admin");
        //    IdentityRole role3 = new IdentityRole("SuperAdmin");

        //    await _roleManager.CreateAsync(role1);
        //    await _roleManager.CreateAsync(role2);
        //    await _roleManager.CreateAsync(role3);
        //    return Ok("Roles have been created.");

        //}

        //public async Task<IActionResult> CreateAdmin()
        //{
        //    AppUser admin = new()
        //    {
        //        FullName="Almaye Memmedli",
        //        UserName="Admin"
        //    };
        //    await _userManager.CreateAsync(admin, "Admin123@");
        //    await _userManager.AddToRoleAsync(admin, "SuperAdmin");

        //    return Ok("Admin have been created.");
        //}

        public IActionResult Login()
        {
            return View();
        }
        public async Task<IActionResult> Login(AdminLoginVm adminLoginVm)
        {
            if (!ModelState.IsValid)
                return View();
                AppUser user =await _userManager.FindByNameAsync(adminLoginVm.UserName);
                
                if(user == null)
                {
                    ModelState.AddModelError("", "Username or password is not valid!");
                    return View();
                }
            var result = await _signInManager.PasswordSignInAsync(user, adminLoginVm.Password, adminLoginVm.IsPersistent, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Username or passwor dis not valid!");
                return View();
            }
            return RedirectToAction("Index","Home");   
        }

    }
}
