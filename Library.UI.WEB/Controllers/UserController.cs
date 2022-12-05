
using Library.Entities;
using Library.Interfaces.Service;
using Library.UI.WEB.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Library.UI.WEB.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        // GET: UserController
        [Authorize(Roles = "admin")]
        [TypeFilter(typeof(AuditLogActionFilter))]
        public ActionResult UserList()
        {
            List<User> users = _userService.GetUsers();
            if(users == null)
            {
                return View();
            }
            return View(users);
        }

        // GET: UserController/Edit/5
        [Authorize(Roles = "admin")]
        [TypeFilter(typeof(AuditLogActionFilter))]
        public ActionResult UserEdit(string id)
        {
            User user = _userService.GetUser(id);
            if(user == null)
            {
                return View();
            }
            return View(_userService.GetUser(id));
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        [TypeFilter(typeof(AuditLogActionFilter))]
        public ActionResult UserEdit(int id, IFormCollection collection)
        {
            try
            {
                User updateUser = new User()
                {
                    Login = collection["Login"],
                    Password = collection["Password"],
                    Role = collection["Role"]
                };
                return RedirectToRoute(new { controller = "User", action = "UserList" });
            }
            catch
            {
                return RedirectToRoute(new { controller = "User", action = "UserList" });
            }
        }
    }
}
