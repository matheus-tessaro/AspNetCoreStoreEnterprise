using SE.WebApp.MVC.Models;
using SE.WebApp.MVC.Models.Internal;
using System.Threading.Tasks;

namespace SE.WebApp.MVC.Services
{
    public interface IIdentityService
    {
        Task<UserLoginResponse> Authentication(UserAuthenticationViewModel model);

        Task<UserLoginResponse> Register(UserRegistryViewModel model);
    }
}
