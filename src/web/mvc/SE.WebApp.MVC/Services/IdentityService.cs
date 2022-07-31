using SE.WebApp.MVC.Models;
using SE.WebApp.MVC.Models.Internal;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SE.WebApp.MVC.Services
{
    public class IdentityService : IIdentityService
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
            StringContent content = new(JsonSerializer.Serialize(model), Encoding.UTF8, "appliation/json");
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage response = await _httpClient.PostAsync("https://localhost:44325/api/identity/authentication", content);
            return JsonSerializer.Deserialize<UserLoginResponse>(await response?.Content.ReadAsStringAsync(), _jsonSerializerOptions);
        }

        public async Task<UserLoginResponse> Register(UserRegistryViewModel model)
        {
            StringContent content = new(JsonSerializer.Serialize(model), Encoding.UTF8, "appliation/json");
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage response = await _httpClient.PostAsync("https://localhost:44325/api/identity/register", content);
            return JsonSerializer.Deserialize<UserLoginResponse>(await response?.Content.ReadAsStringAsync(), _jsonSerializerOptions);
        }
    }
}
