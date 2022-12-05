
using Library.Entities;
using Library.Interfaces.Service;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Library.UI.WebApi.infrastructures;

namespace Library.UI.WebApi.Controllers
{
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(ILogger<AccountController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpPost("token")]
        public async Task<ActionResult> Get(string login, string password)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            //From String to byte array
            SHA512 shaM = new SHA512Managed();
            byte[] sourceBytes = Encoding.UTF8.GetBytes(password);
            byte[] hashBytes = shaM.ComputeHash(sourceBytes);
            string hashPass = BitConverter.ToString(hashBytes).Replace("-", String.Empty);

            if (_userService.LoginUser(new User() { Login = login.ToLower(), Password = hashPass }))
            {
                var user = _userService.GetUser(login);
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Login.ToLower()),
                    new Claim(ClaimTypes.Name, user.Login.ToLower()),
                    new Claim(ClaimTypes.Role, user.Role),
                };

                ClaimsIdentity claimsIdentity =
                    new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

                var now = DateTime.Now;
                var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUEDIENCE,
                    notBefore: now,
                    claims: claimsIdentity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
                    ); 
                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                var response = new
                {
                    access_token = encodedJwt,
                    user_name = claimsIdentity.Name
                };

                return Ok(response);
            }
            return Unauthorized();
        }

    }
}
