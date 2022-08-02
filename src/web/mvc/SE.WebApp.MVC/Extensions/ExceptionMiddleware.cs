using Microsoft.AspNetCore.Http;
using Polly.CircuitBreaker;
using System.Net;
using System.Threading.Tasks;

namespace SE.WebApp.MVC.Extensions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next) => _next = next;

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (CustomHttpRequestException ex)
            {
                HandleRequestException(httpContext, ex);
            }
            catch (BrokenCircuitException)
            {
                HandleCircuitBreakerException(httpContext);
            }
        }

        private static void HandleRequestException(HttpContext httpContext, CustomHttpRequestException httpRequestException)
        {
            switch (httpRequestException.StatusCode)
            {
                case HttpStatusCode.Unauthorized:
                    httpContext.Response.Redirect($"/login?ReturnUrl={httpContext.Request.Path}");
                    return;
            }

            httpContext.Response.StatusCode = (int)httpRequestException.StatusCode;
        }

        private static void HandleCircuitBreakerException(HttpContext httpContext) =>
            httpContext.Response.Redirect("/service-unavailable");
    }
}
