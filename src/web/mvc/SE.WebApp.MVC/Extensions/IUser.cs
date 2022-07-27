using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace SE.WebApp.MVC.Extensions
{
    public interface IUser
    {
        string Name { get; }
        bool IsAuthenticated();
        bool HasRole(string role);
        Guid GetUserId();
        string GetUserEmail();
        string GetUserToken();
        IEnumerable<Claim> GetClaims();
        HttpContext GetHttpContext();
    }

    public class AspNetUser : IUser
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public AspNetUser(IHttpContextAccessor contextAccessor) => _contextAccessor = contextAccessor;

        public string Name => _contextAccessor.HttpContext.User.Identity.Name;

        public bool IsAuthenticated() => _contextAccessor.HttpContext.User.Identity.IsAuthenticated;

        public bool HasRole(string role) => _contextAccessor.HttpContext.User.IsInRole(role);

        public Guid GetUserId() => IsAuthenticated() ? Guid.Parse(_contextAccessor.HttpContext.User.GetUserId()) : Guid.Empty;

        public string GetUserEmail() => IsAuthenticated() ? _contextAccessor.HttpContext.User.GetUserEmail() : string.Empty;

        public string GetUserToken() => IsAuthenticated() ? _contextAccessor.HttpContext.User.GetUserToken() : string.Empty;

        public IEnumerable<Claim> GetClaims() => _contextAccessor.HttpContext.User.Claims;

        public HttpContext GetHttpContext() => _contextAccessor.HttpContext;
    }

    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentException(nameof(principal));

            Claim claim = principal.FindFirst("sub");
            return claim?.Value;
        }

        public static string GetUserEmail(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentException(nameof(principal));

            Claim claim = principal.FindFirst("email");
            return claim?.Value;
        }

        public static string GetUserToken(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentException(nameof(principal));

            Claim claim = principal.FindFirst("JWT");
            return claim?.Value;
        }
    }
}
