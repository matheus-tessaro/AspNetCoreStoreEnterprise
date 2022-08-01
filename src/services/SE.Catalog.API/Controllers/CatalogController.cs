using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SE.Catalog.API.Models;
using SE.WebApi.Core.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SE.Catalog.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/catalog")]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public CatalogController(IProductRepository productRepository) => _productRepository = productRepository;

        [AllowAnonymous]
        [HttpGet("products")]
        public async Task<IEnumerable<Product>> GetProducts() =>
            await _productRepository.GetAll();

        [ClaimsAuthorize("Catalog", "Read")]
        [HttpGet("products/{id}")]
        public async Task<Product> GetProductById([FromRoute] Guid id) =>
            await _productRepository.GetById(id);
    }
}
