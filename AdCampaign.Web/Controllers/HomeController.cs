using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AdCampaign.Authetication;
using AdCampaign.BLL.Services.Adverts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AdCampaign.Models;
using Microsoft.AspNetCore.Authorization;

namespace AdCampaign.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAdvertService _advertService;

        public HomeController(ILogger<HomeController> logger, IAdvertService advertService)
        {
            _logger = logger;
            _advertService = advertService;
        }

        public async Task<IActionResult> Index(CancellationToken ct)
        {
            if (User.Identity is {IsAuthenticated:true})
            {
                return RedirectToAction("Index", "Advert");
            }

            var ads= await _advertService.GetAdvertsToShow(ct);
            
            if (!ads.Ok)
            {
                ViewData["Errors"] = ads.Errors;
                return View();
            }

            
            return View(ads.Unwrap().ToList());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}