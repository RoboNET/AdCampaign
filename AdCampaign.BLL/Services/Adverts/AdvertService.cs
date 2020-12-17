using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AdCampaign.BLL.Services.Adverts.DTO;
using AdCampaign.Common;
using AdCampaign.DAL.Entities;
using AdCampaign.DAL.Repositories.Adverts;
using AdCampaign.DAL.Repositories.AdvertsStatistic;

namespace AdCampaign.BLL.Services.Adverts
{
    public class AdvertService : IAdvertService
    {
        private readonly IAdvertRepository advertRepository;
        private readonly IAdvertStatisticRepository statisticRepository;

        public AdvertService(IAdvertRepository advertRepository, IAdvertStatisticRepository statisticRepository)
        {
            this.advertRepository = advertRepository;
            this.statisticRepository = statisticRepository;
        }
        
        public async Task<Result<IEnumerable<ShowAdvertDto>>> GetAdvertsToShow(CancellationToken ct)
        {
            //количество показываемых кампаний
            const int ToShowCount = 4;
            var adverts = await advertRepository.Get(new GetAdvertsParams
            {
                IsBlocked = false,
                IsVisible = true,
                ImpressingDate = DateTime.UtcNow,
                ImpressingTime = DateTime.UtcNow.TimeOfDay,
                ToTake = ToShowCount,
                Shuffle = true
            });

            var toShow = adverts
                .Select(x => new ShowAdvertDto(x))
                .ToList();
            
            if (ct.IsCancellationRequested)
                return toShow;
            
            await statisticRepository.Increment(toShow.Select(x => x.Id), AdvertStatisticType.Impression);
            return toShow;
        }

        public async Task<Result<Advert>> Create(CreateAdvertDto dto)
        {
            var advert = new Advert
            {
                Name = dto.Name,
                OwnerId = dto.OwnerId,
                IsBlocked = dto.IsBlocked,
                IsVisible = dto.IsVisible,
                BlockedById = dto.BlockedById,
                BlockedDate = dto.BlockedDate,
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow,
                AdvertStatistics = new List<AdvertStatistic>
                {
                    new() {AdvertStatisticType = AdvertStatisticType.Filled},
                    new() {AdvertStatisticType = AdvertStatisticType.Impression},
                    new() {AdvertStatisticType = AdvertStatisticType.Followed},
                },
                RequestType = dto.RequestType,
                ImpressingDateFrom = dto.ImpressingDateFrom,
                PrimaryImageId = dto.PrimaryImageId,
                SecondaryImageId = dto.SecondaryImageId,
                ImpressingDateTo = dto.ImpressingDateFrom,
                ImpressingTimeFrom = dto.ImpressingTimeFrom,
                ImpressingTimeTo = dto.ImpressingTimeTo
            };
            return await advertRepository.Insert(advert);
        }
    }
}