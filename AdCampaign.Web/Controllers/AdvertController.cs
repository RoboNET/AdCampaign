using Microsoft.AspNetCore.Mvc;

namespace AdCampaign.Controllers
{
    public class AdvertController : Controller
    {
        public AdvertController()
        {
        }
        
        public IActionResult Index()
        {
            return View();
        }
    }
}