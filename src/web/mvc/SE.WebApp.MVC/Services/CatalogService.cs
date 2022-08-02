using Microsoft.Extensions.Options;
using SE.WebApp.MVC.Extensions;
using SE.WebApp.MVC.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SE.WebApp.MVC.Services
{
    public class CatalogService : Service, ICatalogService
    {
        private readonly HttpClient _httpClient;

        public CatalogService(HttpClient httpClient, IOptions<AppSettings> appSettings)
        {
            httpClient.BaseAddress = new Uri(appSettings.Value.CatalogUrl);
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ProductViewModel>> GetAll()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("api/catalog/products");
            HandleResponseErrors(response);

            return await DeserializeResponseObject<IEnumerable<ProductViewModel>>(response);
        }

        public async Task<ProductViewModel> GetById(Guid id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"api/catalog/products/{id}");
            HandleResponseErrors(response);

            return await DeserializeResponseObject<ProductViewModel>(response);
        }
    }
}
