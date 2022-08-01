using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SE.Identity.API.Models;
using SE.Identity.API.Models.Internal;
using SE.WebApi.Core.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace SE.Identity.API.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/identity")]
    public class IdentityController : BaseController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppSettings _appSettings;

        public IdentityController(
            SignInManager<IdentityUser> signInManager, 
            UserManager<IdentityUser> userManager, 
            IOptions<AppSettings> appSettings)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _appSettings = appSettings.Value;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] UserRegistryViewModel model)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            IdentityUser user = new()
            {
                UserName = model.Email,
                Email = model.Email,
                EmailConfirmed = true
            };

            IdentityResult result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
                return CustomResponse(await GenerateJwt(user.Email));

            AddErrors(result.Errors.Select(x => x.Description).ToList());
            return CustomResponse();
        }

        [HttpPost("authentication")]
        public async Task<ActionResult> Login([FromBody] UserAuthenticationViewModel model)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            SignInResult result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, true);

            if (result.Succeeded)
                return CustomResponse(await GenerateJwt(model.Email));

            if (result.IsLockedOut)
            {
                AddErrors("User locked out due to multiple failed login attempts");
                return CustomResponse();
            }

            AddErrors("Username or password is incorrect");
            return CustomResponse();
        }

        private async Task<UserLoginResponse> GenerateJwt(string email)
        {
            IdentityUser user = await _userManager.FindByEmailAsync(email);
            IList<Claim> claims = await GetClaims(user);

            ClaimsIdentity claimsIdentity = new();
            claimsIdentity.AddClaims(claims);

            JwtSecurityTokenHandler tokenHandler = new();
            SecurityToken securityToken = tokenHandler.CreateToken(
                new SecurityTokenDescriptor
                {
                    Issuer = _appSettings.Issuer,
                    Audience = _appSettings.Audience,
                    Subject = claimsIdentity,
                    Expires = DateTime.UtcNow.AddHours(_appSettings.ExpirationHours),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appSettings.Secret)), SecurityAlgorithms.HmacSha256Signature)
                });

            return new UserLoginResponse
            {
                AccessToken = tokenHandler.WriteToken(securityToken),
                ExpiresIn = TimeSpan.FromHours(_appSettings.ExpirationHours).TotalSeconds,
                UserToken = new UserToken
                {
                    Id = user.Id,
                    Email = user.Email,
                    Claims = claims.Select(x => new UserClaim(x.Type, x.Value))
                }
            };
        }

        private async Task<IList<Claim>> GetClaims(IdentityUser user)
        {
            IList<Claim> claims = await _userManager.GetClaimsAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

            foreach (var role in await _userManager.GetRolesAsync(user))
                claims.Add(new Claim("role", role));

            return claims;
        }

        private static long ToUnixEpochDate(DateTime date) =>
            (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }
}
