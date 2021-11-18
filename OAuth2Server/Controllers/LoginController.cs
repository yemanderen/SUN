using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CDBWebSecurity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using CDBWebSecurity.Services;
using System.Security.Claims;
using Exceptionless.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace CDBWebSecurity.Controllers
{
    public class LoginController : Controller
    {
        private IHttpContextAccessor _httpContextAccessor;
        public LoginController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpGet]
        public IActionResult LoginIndex()
        {
            LoginModel loginModel = new LoginModel();
            loginModel.Account = "JackV123456";
            loginModel.Password = "";
            return View(loginModel);
        }

        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public IActionResult Login()
        {
            var jwtResult = _httpContextAccessor.HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme).Result;
            if (jwtResult.Principal == null)
                return null;
            var token = jwtResult.Principal.FindFirstValue(ClaimTypes.Sid);
            //token = token + "x";

            bool result = PingAccessService.ValidateUserToken(token);
            if (result == true)
            {
                return RedirectToAction("Index", "Home");
                //return Content("Login successfully");
            }
            else
            {
                return RedirectToAction("LoginIndex", "Login");
            }
        }

        [HttpPost]
        public IActionResult LoginIndex(LoginModel user)
        {
            if (!ModelState.IsValid)
            {
                return Content("Please input user account and password.");
            }

            bool result = PingFederateService.ValidateUser(user);
            if (result == true)
            {
                string token = PingAccessService.CreateJwtToken(user.Account);
                ClaimsIdentity identity = new ClaimsIdentity("Forms");
                identity.AddClaim(new Claim(ClaimTypes.Sid, token));
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
                return Content("Login successfully");
            }

            return RedirectToAction("Index", "Error");
            //return Content("Login successfully");
        }
    }
}
