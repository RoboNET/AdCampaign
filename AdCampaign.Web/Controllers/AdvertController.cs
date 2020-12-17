﻿using System.Linq;
using System.Threading.Tasks;
using AdCampaign.Authetication;
using AdCampaign.BLL.Services.Adverts;
using AdCampaign.BLL.Services.Adverts.DTO;
using AdCampaign.DAL.Entities;
using AdCampaign.Extensions;
using AdCampaign.Models;
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

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateFileRequestModel dto)
        {
            var created = await _service.Create(User.GetId(), new AdvertDto()
            {
                Name = dto.Name,
                RequestType = dto.RequestType,
                ImpressingDateFrom = dto.ImpressingDateFrom,
                ImpressingDateTo = dto.ImpressingDateTo,
                ImpressingTimeFrom = dto.ImpressingTimeFrom,
                ImpressingTimeTo = dto.ImpressingTimeTo
            }, dto.PrimaryImage.ToFile(), dto.SecondaryImage.ToFile());
            //todo redirect to created
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(long id)
        {
            var updated = await _service.Get(User.GetId(), id);
            return View(updated.Unwrap());
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromForm] IFormFileCollection files, AdvertDto dto)
        {
            var (primary, secondary) = GetFiles(files);
            var updated = await _service.Update(User.GetId(), dto, primary, secondary);
            return View(updated.Data);
        }

        private static (File primary, File secondary) GetFiles(IFormFileCollection files)
        {
            var primary = files.FirstOrDefault(x => x.Name == "primary");
            var secondary = files.FirstOrDefault(x => x.Name == "secondary");
            return (primary?.ToFile(), secondary?.ToFile());
        }
    }
}