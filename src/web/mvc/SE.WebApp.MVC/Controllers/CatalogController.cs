using Microsoft.AspNetCore.Mvc;
using SE.WebApp.MVC.Services;
using System;
using System.Threading.Tasks;

namespace SE.WebApp.MVC.Controllers
{
    public class CatalogController : BaseController
    {
        private readonly ICatalogService _catalogService;
        public CatalogController(ICatalogService catalogService) => _catalogService = catalogService;

        [HttpGet]
        [Route("")]
        [Route("showcase")]
        public async Task<IActionResult> Index() =>
            View(await _catalogService.GetAll());

        [HttpGet]
        [Route("product-details/{id}")]
        public async Task<IActionResult> ProductDetails(Guid id) =>
            View(await _catalogService.GetById(id));
    }
}
