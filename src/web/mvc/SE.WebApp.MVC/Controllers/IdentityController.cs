using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using SE.WebApp.MVC.Models;
using SE.WebApp.MVC.Models.Internal;
using SE.WebApp.MVC.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SE.WebApp.MVC.Controllers
{
    public class IdentityController : BaseController
    {
        private readonly IIdentityService _identityService;
        public IdentityController(IIdentityService authenticationService) => _identityService = authenticationService;

        [HttpGet]
        [Route("register")]
        public IActionResult Register()
        {
            return View("Registry");
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> Register(UserRegistryViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            UserLoginResponse response = await _identityService.Register(model);

            if (HasResponseErrors(response.ResponseResult))
                return View("Registry", model);

            await Authenticate(response);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Route("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login(UserAuthenticationViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            UserLoginResponse response = await _identityService.Authentication(model);

            if (HasResponseErrors(response.ResponseResult))
                return View(model);

            await Authenticate(response);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Route("logout")]
        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        private async Task Authenticate(UserLoginResponse response)
        {
            JwtSecurityToken jwtSecurityToken = GetSecurityToken(response?.AccessToken);

            if (jwtSecurityToken == null)
                return;

            var claims = new List<Claim> { new Claim("JWT", response.AccessToken) };
            claims.AddRange(jwtSecurityToken.Claims);

            ClaimsIdentity claimsIdentity = new(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authenticationProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60),
                IsPersistent = true
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme, 
                new ClaimsPrincipal(claimsIdentity), 
                authenticationProperties);
        }

        private static JwtSecurityToken GetSecurityToken(string jwtBearer) =>
            new JwtSecurityTokenHandler().ReadToken(jwtBearer) as JwtSecurityToken;
    }
}
