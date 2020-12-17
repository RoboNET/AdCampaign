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

        [HttpGet]
        public IActionResult Auth()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Auth(string username, string password, string returnUrl)
        {
            var principalResult = await _service.CreatePrincipal(username, password);
            if (!principalResult.Ok)
            {
                ViewData["Errors"] = principalResult.Errors;
                return View("Auth");
            }

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                principalResult.Unwrap());
            return Redirect(returnUrl ?? "/Home");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}