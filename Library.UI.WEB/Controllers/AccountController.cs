
using Library.Entities;
using Library.Interfaces.Service;
using Library.UI.WEB.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Library.UI.WEB.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IUserService _userService;

        public AccountController(ILogger<AccountController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        public IActionResult Login(string returnUrl = "/")
        {
            TempData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel account)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            //From String to byte array
            SHA512 shaM = new SHA512Managed();
            byte[] sourceBytes = Encoding.UTF8.GetBytes(account.Password);
            byte[] hashBytes = shaM.ComputeHash(sourceBytes);
            string hashPass = BitConverter.ToString(hashBytes).Replace("-", String.Empty);

            if (_userService.LoginUser(new User() { Login = account.Login.ToLower(), Password = hashPass}))
            {
                var user = _userService.GetUser(account.Login);
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Login.ToLower()),
                    new Claim(ClaimTypes.Name, user.Login.ToLower()),
                    new Claim(ClaimTypes.Role, user.Role),
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties()
                {
                    IsPersistent = false,
                });

                return Redirect(TempData["ReturnUrl"].ToString());
            }
            ViewBag.LoginError = "Login or password is invalid";
            return View();
        }
    }
}
