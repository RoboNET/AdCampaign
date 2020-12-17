using System.Linq;
using System.Threading.Tasks;
using AdCampaign.BLL.Services.Adverts;
using AdCampaign.BLL.Services.Adverts.DTO;
using AdCampaign.DAL.Entities;
using AdCampaign.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdCampaign.Controllers
{
    public class AdvertController : Controller
    {
        private readonly IAdvertService _service;

        public AdvertController(IAdvertService service)
        {
            _service = service;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddAdvert([FromForm]IFormFileCollection files, AdvertDto dto)
        {
            var (primary, secondary) = GetFiles(files);
            var created = await _service.Create(dto, primary, secondary);
            //todo redirect to created
            return RedirectToAction("Index");
        }
        
        [HttpPut]
        public async Task<IActionResult> UpdateAdvert([FromForm]IFormFileCollection files, AdvertDto dto)
        {
            var (primary, secondary) = GetFiles(files);
            var updated = await _service.Update(dto, primary, secondary);
            //todo redirect to updated
            return RedirectToAction("Index");
        }

        private static (File primary, File secondary) GetFiles(IFormFileCollection files)
        {
            var  primary = files.FirstOrDefault(x => x.Name == "primary");
            var  secondary = files.FirstOrDefault(x => x.Name == "secondary");
            return (primary?.ToFile(), secondary?.ToFile());
        }
        
    }
}