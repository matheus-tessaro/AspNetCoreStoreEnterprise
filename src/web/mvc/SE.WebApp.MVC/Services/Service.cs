using SE.WebApp.MVC.Extensions;
using System.Net;
using System.Net.Http;

namespace SE.WebApp.MVC.Services
{
    public abstract class Service
    {
        protected bool HandleResponseErrors(HttpResponseMessage response)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.Unauthorized:
                case HttpStatusCode.Forbidden:
                case HttpStatusCode.NotFound:
                case HttpStatusCode.InternalServerError:
                    throw new CustomHttpRequestException(response.StatusCode);

                case HttpStatusCode.BadRequest:
                    return false;
            }

            response.EnsureSuccessStatusCode();
            return true;
        }
    }
}
