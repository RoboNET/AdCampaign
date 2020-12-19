using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using AuthenticationService = AdCampaign.Authetication.AuthenticationService;

namespace AdCampaign.Controllers
{
    public class AuthController : Controller
    {
        private readonly AuthenticationService _service;

        public AuthController(AuthenticationService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string username, string password, string returnUrl)
        {
            var principalResult = await _service.CreatePrincipal(username, password);
            if (!principalResult.Ok)
            {
                ViewData["Errors"] = principalResult.Errors;
                return View();
            }

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                principalResult.Unwrap());
            return Redirect(returnUrl ?? "/Advert");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}