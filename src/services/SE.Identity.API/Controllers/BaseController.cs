using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace SE.Identity.API.Controllers
{
    [ApiController]
    public abstract class BaseController : Controller
    {
        protected ICollection<string> Errors = new List<string>();

        protected ActionResult CustomResponse(object result = null)
        {
            if (Invalid())
                return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]> { { nameof(Errors), Errors.ToArray() }}));

            return Ok(result);
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(x => x.Errors);

            foreach (ModelError error in errors)
                AddErrors(error.ErrorMessage);

            return CustomResponse();
        }

        protected bool Invalid() => Errors != null && Errors.Any();

        protected void AddErrors(string error) => Errors.Add(error);

        protected void ClearErrors() => Errors.Clear();
    }
}
