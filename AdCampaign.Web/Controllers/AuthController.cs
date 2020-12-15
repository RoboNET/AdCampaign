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

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password, string returnUrl)
        {
            var principal = await _service.CreatePrincipal(username, password);
            if (principal == null)
                return View("Index");

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                principal);
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