#nullable enable
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using AdCampaign.Authetication;
using AdCampaign.BLL.Services.Adverts;
using AdCampaign.BLL.Services.Adverts.DTO;
using AdCampaign.DAL.Entities;
using AdCampaign.DAL.Repositories.Adverts;
using AdCampaign.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdCampaign.Controllers
{
    public record AdvertViewModel(long Id, string Name, bool IsActive, RequestType RequestType, long OwnerId,
        string OwnerName);


    [Authorize]
    public class AdvertController : Controller
    {
        private readonly IAdvertRepository _repository;
        private readonly IAdvertService _service;

        public AdvertController(IAdvertRepository repository, IAdvertService service)
        {
            _repository = repository;
            _service = service;
        }
        
        public async Task<IActionResult> Index()
        {
            var filter = new GetAdvertsParams
            {
                IsVisible = true,
                UserEmail = User.GetRole() == Role.Advertiser ? User.GetEmail() : null
            };

            IEnumerable<Advert> advertResponses = await _repository.Get(filter);
            return View(advertResponses.Select(a =>
                new AdvertViewModel(a.Id, a.Name, !a.IsBlocked, a.RequestType, a.OwnerId, a.Owner.Name)));
        }

        public async Task<IActionResult> Delete(long id)
        {
            var res = await _service.Delete(id, User.GetEmail(), User.GetRole());
            if (!res.Ok)
            {
                return Forbid();
            }

            return RedirectToAction("Index");
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