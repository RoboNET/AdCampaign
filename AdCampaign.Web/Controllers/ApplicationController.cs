using System.Linq;
using System.Threading.Tasks;
using AdCampaign.Authetication;
using AdCampaign.BLL.Services.Adverts;
using AdCampaign.BLL.Services.Adverts.DTO;
using AdCampaign.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdCampaign.Controllers
{
    public class ApplicationController : Controller
    {
        private readonly IApplicationService _applicationService;

        public ApplicationController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateApplicationViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return Json(ModelState.Values.SelectMany(entry => entry.Errors));
            }

            var result = await _applicationService.Create(new CreateApplicationDto()
            {
                Email = request.Email,
                Phone = request.Phone,
                AdvertId = request.AdvertId
            });

            if (!result.Ok)
                return Json(result.Errors);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index(long? advertId)
        {
            var items = await _applicationService.Get(User.GetId(), User.GetRole(), advertId);

            if (!items.Ok)
            {
                ViewData["Errors"] = items.Errors;
                return View();
            }

            return View(items.Unwrap());
        }
    }
}