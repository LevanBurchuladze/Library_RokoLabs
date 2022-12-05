
using Library.Entities;
using Library.Interfaces.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Library.UI.WebApi.infrastructures;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Library.UI.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet]
        public ActionResult<List<User>> Get()
        {
            List<User> users = _userService.GetUsers();
            if (users == null)
            {
                return NotFound("Users is not found");
            }
            return users;
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [TypeFilter(typeof(AuditLogActionFilter))]
        public ActionResult<int> Post(User user)
        {
            int res = _userService.UpdateUserRole(user);
            if (res == 0)
            {
                return BadRequest("User is not edited");
            }
            return Ok();
        }
    }
}
