using Microsoft.AspNetCore.Mvc;
using SE.Core.Mediator;
using SE.Customers.API.Application.Commands;
using SE.WebApi.Core.Controllers;
using System.Threading.Tasks;

namespace SE.Customers.API.Controllers
{
    [Route("api/customer")]
    public class CustomerController : BaseController
    {
        private readonly IMediatorHandler _mediatorHandler;

        public CustomerController(IMediatorHandler mediatorHandler) => _mediatorHandler = mediatorHandler;

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return Ok();
        }
    }
}
