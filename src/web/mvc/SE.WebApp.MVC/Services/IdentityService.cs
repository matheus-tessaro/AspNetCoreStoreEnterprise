using Microsoft.Extensions.Options;
using SE.WebApp.MVC.Extensions;
using SE.WebApp.MVC.Models;
using SE.WebApp.MVC.Models.Internal;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SE.WebApp.MVC.Services
{
    public class IdentityService : Service, IIdentityService
    {
        private readonly HttpClient _httpClient;

        public IdentityService(HttpClient httpClient, IOptions<AppSettings> appSettings)
        {
            httpClient.BaseAddress = new Uri(appSettings.Value.AuthenticationUrl);
            _httpClient = httpClient;
        }

        public async Task<UserLoginResponse> Authentication(UserAuthenticationViewModel model)
        {
            StringContent content = GetContent(model);
            return await PostRequest(content, "/api/identity/authentication");
        }

        public async Task<UserLoginResponse> Register(UserRegistryViewModel model)
        {
            StringContent content = GetContent(model);
            return await PostRequest(content, "/api/identity/register");
        }

        private async Task<UserLoginResponse> PostRequest(StringContent content, string requestUri)
        {
            HttpResponseMessage response = await _httpClient.PostAsync(requestUri, content);

            if (!HandleResponseErrors(response))
                return new UserLoginResponse { ResponseResult = await DeserializeResponseObject<ResponseResult>(response) };

            return await DeserializeResponseObject<UserLoginResponse>(response);
        }
    }
}
