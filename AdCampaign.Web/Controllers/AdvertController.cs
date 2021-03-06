#nullable enable
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using AdCampaign.Authetication;
using AdCampaign.BLL.Services.Adverts;
using AdCampaign.BLL.Services.Adverts.DTO;
using AdCampaign.Common;
using AdCampaign.DAL.Entities;
using AdCampaign.DAL.Repositories.Adverts;
using AdCampaign.Extensions;
using AdCampaign.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdCampaign.Controllers
{
    public record AdvertViewModel(long Id, string Name, bool IsActive, RequestType RequestType, long OwnerId,
        string OwnerName, bool IsBlocked);


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

        public async Task<IActionResult> Index(long? userId)
        {
            var filter = new GetAdvertsParams
            {
                OwnerId = User.GetRole() == Role.Advertiser ? User.GetId() : userId
            };

            IEnumerable<Advert> advertResponses = await _repository.Get(filter);
            return View(advertResponses.Select(a =>
                new AdvertViewModel(a.Id, a.Name, a.IsVisible, a.RequestType, a.OwnerId, a.Owner.Name, a.IsBlocked)));
        }

        public async Task<IActionResult> Delete(long id)
        {
            var res = await _service.Delete(id, User.GetId(), User.GetRole());
            if (!res.Ok)
            {
                return Forbid();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Advertiser")]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Advertiser")]
        public async Task<IActionResult> Create(CreateFileRequestModel dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            if (!CheckFileExtension(dto.PrimaryImage?.FileName) || !CheckFileExtension(dto.SecondaryImage?.FileName))
            {
                ViewData["Errors"] = new Error[]
                    {new("Расширение файла должно быть .jpg, .jpeg или .png", "400")};
                return View(dto);
            }

            var created = await _service.Create(User.GetId(), User.GetRole(), new AdvertDto()
            {
                Name = dto.Name,
                RequestType = dto.RequestType,
                ImpressingDateFrom = dto.ImpressingDateFrom,
                ImpressingDateTo = dto.ImpressingDateTo,
                ImpressingTimeFrom = dto.ImpressingTimeFrom,
                ImpressingTimeTo = dto.ImpressingTimeTo,
                ImpressingAlways = dto.ImpressingAlways,
                IsVisible = true
            }, dto.PrimaryImage.ToFile(), dto.SecondaryImage.ToFile());

            if (!created.Ok)
            {
                ViewData["Errors"] = created.Errors;
                return View(dto);
            }


            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(long id)
        {
            var updated = await _service.Get(User.GetId(), User.GetRole(), id);

            if (!updated.Ok)
            {
                ViewData["Errors"] = updated.Errors;
                return View();
            }

            var result = updated.Unwrap();
            @ViewData["IsBlocked"] = result.IsBlocked;

            return View(new UpdateFileRequestModel
            {
                Id = result.Id,
                Name = result.Name,
                IsVisible = result.IsVisible,
                RequestType = result.RequestType,
                ImpressingDateFrom = result.ImpressingDateFrom,
                ImpressingDateTo = result.ImpressingDateTo,
                ImpressingTimeFrom = result.ImpressingTimeFrom,
                ImpressingTimeTo = result.ImpressingTimeTo,
                ImpressingAlways = result.ImpressingAlways,
                PrimaryImageId = result.PrimaryImageId,
                SecondaryImageId = result.SecondaryImageId
            });
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateFileRequestModel dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            if (!CheckFileExtension(dto.PrimaryImage?.FileName) || !CheckFileExtension(dto.SecondaryImage?.FileName))
            {
                ViewData["Errors"] = new Error[]
                    {new("Расширение файла должно быть .jpg, .jpeg или .png", "400")};
                return View(dto);
            }

            var updated = await _service.Update(User.GetId(), User.GetRole(), new AdvertDto()
            {
                Id = dto.Id,
                IsVisible = dto.IsVisible,
                Name = dto.Name,
                RequestType = dto.RequestType,
                ImpressingDateFrom = dto.ImpressingDateFrom,
                ImpressingDateTo = dto.ImpressingDateTo,
                ImpressingTimeFrom = dto.ImpressingTimeFrom,
                ImpressingTimeTo = dto.ImpressingTimeTo,
                ImpressingAlways = dto.ImpressingAlways
            }, dto.PrimaryImage?.ToFile(), dto.SecondaryImage?.ToFile());

            if (!updated.Ok)
            {
                ViewData["Errors"] = updated.Errors;
                return View(dto);
            }

            var result = updated.Unwrap();

            @ViewData["IsBlocked"] = result.IsBlocked;
            return View(new UpdateFileRequestModel
            {
                Id = result.Id,
                Name = result.Name,
                IsVisible = result.IsVisible,
                RequestType = result.RequestType,
                ImpressingDateFrom = result.ImpressingDateFrom,
                ImpressingDateTo = result.ImpressingDateTo,
                ImpressingTimeFrom = result.ImpressingTimeFrom,
                ImpressingTimeTo = result.ImpressingTimeTo,
                ImpressingAlways = dto.ImpressingAlways,
                PrimaryImageId = result.PrimaryImageId,
                SecondaryImageId = result.SecondaryImageId
            });
        }

        [HttpPost]
        [Authorize(Roles = "Administrator,Moderator")]
        public async Task<IActionResult> ChangeBlock(long id, bool block)
        {
            var result = await _service.ChangeBlock(User.GetId(), User.GetRole(), id, block);
            if (!result.Ok)
                return Json(result.Errors);

            return RedirectToAction("Update", new {id});
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> IncrementStatistic(long id, AdvertStatisticType statisticType)
        {
            await _service.IncrementAdvertsStats(id, statisticType);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Statistic(long id)
        {
            var result = await _service.Get(User.GetId(), User.GetRole(), id);
            if (!result.Ok)
                return Json(result.Errors);

            ViewData["AdName"] = result.Unwrap().Name;

            return View(result.Unwrap().AdvertStatistics);
        }

        bool CheckFileExtension(string? name)
        {
            if (string.IsNullOrEmpty(name))
                return true;

            var extension = Path.GetExtension(name);
            if (extension != ".jpg" && extension != ".jpeg" && extension != ".png")
            {
                return false;
            }

            return true;
        }
    }
}