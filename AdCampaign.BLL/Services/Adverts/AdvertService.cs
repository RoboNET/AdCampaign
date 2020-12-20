using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AdCampaign.BLL.Services.Adverts.DTO;
using AdCampaign.Common;
using AdCampaign.DAL.Entities;
using AdCampaign.DAL.Repositories;
using AdCampaign.DAL.Repositories.Adverts;
using AdCampaign.DAL.Repositories.AdvertsStatistic;

namespace AdCampaign.BLL.Services.Adverts
{
    public class AdvertService : IAdvertService
    {
        private readonly IAdvertRepository _advertRepository;
        private readonly IAdvertStatisticRepository _statisticRepository;
        private readonly IFileRepository _fileRepository;

        public AdvertService(IAdvertRepository advertRepository,
            IAdvertStatisticRepository statisticRepository, IFileRepository fileRepository)
        {
            _advertRepository = advertRepository;
            _statisticRepository = statisticRepository;
            _fileRepository = fileRepository;
        }

        public async Task<Result<IEnumerable<ShowAdvertDto>>> GetAdvertsToShow(CancellationToken ct)
        {
            //количество показываемых кампаний
            const int toShowCount = 4;
            var adverts = await _advertRepository.Get(new GetAdvertsParams
            {
                IsBlocked = false,
                IsOwnerBlocked = false,
                IsVisible = true,
                ImpressingDate = DateTime.UtcNow,
                ImpressingTime = DateTime.UtcNow.TimeOfDay,
                ToTake = toShowCount,
                Shuffle = true
            });

            var toShow = adverts
                .Select(x => new ShowAdvertDto(x))
                .ToList();

            if (ct.IsCancellationRequested)
                return toShow;

            await _statisticRepository.Increment(toShow.Select(x => x.Id), AdvertStatisticType.Impression);
            return toShow;
        }

        public Task IncrementAdvertsStats(long id, AdvertStatisticType statisticType) =>
            _statisticRepository.Increment(id, statisticType);

        public async Task<Result<Advert>> Get(long userId, Role role, long id)
        {
            var advert = await _advertRepository.Get(id);
            if (advert == null)
                return new Error("Кампания не найдена", "campaign-not-found");

            if (CanAdvertAccessByRole(role) || UserIsOwner(advert.Owner, userId))
                return advert;

            return new Error("У вас нет прав на просмотр", "403");
        }

        public async Task<Result<Advert>> Create(long userId, Role role, AdvertDto dto, File primaryImage,
            File secondaryImage)
        {
            if (role != Role.Advertiser)
                return new Error("У вас нет прав на создание кампании", "403");
            
            var primaryCreated = primaryImage != null
                ? await _fileRepository.Create(primaryImage.Name, primaryImage.Content)
                : null;
            var secondaryCreated = secondaryImage != null
                ? await _fileRepository.Create(secondaryImage.Name, secondaryImage.Content)
                : null;

            var advert = new Advert
            {
                Name = dto.Name,
                OwnerId = userId,
                IsBlocked = false,
                IsVisible = dto.IsVisible,
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow,
                RequestType = dto.RequestType,
                ImpressingDateFrom = dto.ImpressingDateFrom,
                ImpressingDateTo = dto.ImpressingDateTo,
                ImpressingTimeFrom = dto.ImpressingTimeFrom,
                ImpressingTimeTo = dto.ImpressingTimeTo,
                ImpressingAlways = dto.ImpressingAlways,
                PrimaryImageId = primaryCreated?.Id,
                SecondaryImageId = secondaryCreated?.Id,
                AdvertStatistics = new List<AdvertStatistic>
                {
                    new() {AdvertStatisticType = AdvertStatisticType.Filled},
                    new() {AdvertStatisticType = AdvertStatisticType.Impression},
                    new() {AdvertStatisticType = AdvertStatisticType.Followed},
                }
            };
            return await _advertRepository.Insert(advert);
        }

        public async Task<Result<Advert>> Update(long userId, Role role, AdvertDto dto, File primaryImage,
            File secondaryImage)
        {
            var advert = await _advertRepository.Get(dto.Id);
            if (advert == null)
                return new Error("Кампания не найдена", "campaign-not-found");

            if (!CanAdvertAccessByRole(role) && !UserIsOwner(advert.Owner, userId))
                return new Error("У вас нет прав на изменение", "403");

            if (primaryImage != null)
            {
                if (advert.PrimaryImage != null)
                    await _fileRepository.Delete(advert.PrimaryImage);
                var created = await _fileRepository.Create(primaryImage.Name, primaryImage.Content);
                advert.PrimaryImageId = created.Id;
            }

            if (secondaryImage != null)
            {
                if (advert.SecondaryImage != null)
                    await _fileRepository.Delete(advert.SecondaryImage);
                var created = await _fileRepository.Create(secondaryImage.Name, secondaryImage.Content);
                advert.SecondaryImageId = created.Id;
            }

            UpdateBaseInfo(advert, dto);
            advert.DateUpdated = DateTime.UtcNow;
            return await _advertRepository.Update(advert);
        }

        private static void UpdateBaseInfo(Advert advert, AdvertDto dto)
        {
            advert.Name = dto.Name;
            advert.IsVisible = dto.IsVisible;
            advert.RequestType = dto.RequestType;
            advert.ImpressingDateFrom = dto.ImpressingDateFrom;
            advert.ImpressingDateTo = dto.ImpressingDateTo;
            advert.ImpressingTimeFrom = dto.ImpressingTimeFrom;
            advert.ImpressingTimeTo = dto.ImpressingTimeTo;
            advert.ImpressingAlways = dto.ImpressingAlways;
        }

        private static bool UserIsOwner(User user, long userId) => user.Id == userId;

        private static bool CanAdvertAccessByRole(Role role) => role == Role.Administrator || role == Role.Moderator;

        public async Task<Result> Delete(long id, long userId, Role role)
        {
            var advert = await _advertRepository.Get(id);
            if (CanAdvertAccessByRole(role) || UserIsOwner(advert.Owner, userId))
            {
                await _advertRepository.Delete(id);
                return new();
            }

            return new Error("У вас нет прав на удаление", "403");
        }

        public async Task<Result> ChangeBlock(long userId, Role role, long id, bool block)
        {
            var advert = await _advertRepository.Get(id);
            if (CanBlockByRole(role))
            {
                advert.IsBlocked = block;
                if (block)
                {
                    advert.BlockedById = userId;
                    advert.BlockedDate = DateTime.UtcNow;
                }
                else
                {
                    advert.BlockedById = null;
                    advert.BlockedDate = null;
                }

                await _advertRepository.Update(advert);
                return new();
            }

            return new Error("У вас нет прав на блокировку", "403");

            static bool CanBlockByRole(Role role) => role == Role.Administrator || role == Role.Moderator;
        }
    }
}