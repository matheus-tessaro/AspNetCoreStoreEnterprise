using Microsoft.AspNetCore.Mvc;
using SE.WebApp.MVC.Models;
using System.Linq;

namespace SE.WebApp.MVC.Controllers
{
    public class BaseController : Controller
    {
        protected bool HasResponseErrors(ResponseResult response)
        {
            if (response != null && response.Errors.Messages.Any())
                return true;

            return false;
        }
    }
}
