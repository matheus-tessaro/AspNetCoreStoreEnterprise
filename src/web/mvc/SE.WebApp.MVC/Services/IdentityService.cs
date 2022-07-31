using SE.WebApp.MVC.Models;
using SE.WebApp.MVC.Models.Internal;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SE.WebApp.MVC.Services
{
    public class IdentityService : Service, IIdentityService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public IdentityService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _jsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<UserLoginResponse> Authentication(UserAuthenticationViewModel model)
        {
            StringContent content = GetStringContent(model);
            return await PostRequest(content, "https://localhost:44325/api/identity/authentication");
        }

        public async Task<UserLoginResponse> Register(UserRegistryViewModel model)
        {
            StringContent content = GetStringContent(model);
            return await PostRequest(content, "https://localhost:44325/api/identity/register");
        }

        private static StringContent GetStringContent(dynamic model)
        {
            StringContent content = new(JsonSerializer.Serialize(model), Encoding.UTF8, "appliation/json");
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return content;
        }

        private async Task<UserLoginResponse> PostRequest(StringContent content, string requestUri)
        {
            HttpResponseMessage response = await _httpClient.PostAsync(requestUri, content);

            if (!HandleResponseErrors(response))
                return new UserLoginResponse { ResponseResult = JsonSerializer.Deserialize<ResponseResult>(await response?.Content.ReadAsStringAsync(), _jsonSerializerOptions) };

            return JsonSerializer.Deserialize<UserLoginResponse>(await response?.Content.ReadAsStringAsync(), _jsonSerializerOptions);
        }
    }
}
