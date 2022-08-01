using SE.WebApp.MVC.Extensions;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SE.WebApp.MVC.Services
{
    public abstract class Service
    {
        protected StringContent GetContent(object data)
        {
            StringContent content = new(JsonSerializer.Serialize(data), Encoding.UTF8, "appliation/json");
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return content;
        }

        protected async Task<T> DeserializeResponseObject<T>(HttpResponseMessage responseMessage) =>
            JsonSerializer.Deserialize<T>(await responseMessage?.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

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
