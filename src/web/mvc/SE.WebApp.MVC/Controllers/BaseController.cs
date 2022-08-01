using Microsoft.AspNetCore.Mvc;
using SE.WebApp.MVC.Models;
using System.Linq;

namespace SE.WebApp.MVC.Controllers
{
    public class BaseController : Controller
    {
        protected bool HasResponseErrors(ResponseResult response)
        {
            if (response != null && response.Errors.Errors.Any())
            {
                foreach (var message in response.Errors.Errors)
                    ModelState.AddModelError(string.Empty, message);

                return true;
            }

            return false;
        }
    }
}
