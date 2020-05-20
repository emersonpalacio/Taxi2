using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Taxi.Web.Data;
using Taxi.Web.Helpers;
using Taxi.Web.Models;

namespace Taxi.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly DataContext _context;
        private readonly IUserHelpers _userHelpers;

        public AccountController(DataContext context,
                                 IUserHelpers userHelpers)
        {
            this._context = context;
            this._userHelpers = userHelpers;
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "home");
            }
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model )
        {
            if (ModelState.IsValid)
            {
                var result = await _userHelpers.LoginAsync(model);
                if (result.Succeeded)
                {
                    if (Request.Query.Keys.Contains("ReturnUrl"))
                    {
                        return Redirect(Request.Query["ReturnUrl"].First());
                    }
                    return RedirectToAction("Index","Home");
                }

            }

            ModelState.AddModelError(string.Empty,"Failed to login");
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _userHelpers.LogoutAsync();
            return RedirectToAction("Index","Home");
        }
    }
}