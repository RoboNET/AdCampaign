using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AdCampaign.BLL.Services.Adverts.DTO;
using AdCampaign.Common;
using AdCampaign.Common.Extensions;
using AdCampaign.DAL.Entities;
using AdCampaign.DAL.Repositories.Adverts;
using AdCampaign.DAL.Repositories.AdvertsStatistic;

namespace AdCampaign.BLL.Services.Adverts
{
    public class AdvertService : IAdvertService
    {
        private readonly IAdvertRepository advertRepository;
        private readonly IAdvertStatisticRepository statisticRepository;

        //количество показываемых кампаний
        private const int ToShowCount = 4;

        public AdvertService(IAdvertRepository advertRepository, IAdvertStatisticRepository statisticRepository)
        {
            this.advertRepository = advertRepository;
            this.statisticRepository = statisticRepository;
        }
        
        public async Task<Result<IEnumerable<ShowAdvertDto>>> GetAdvertsToShow(CancellationToken ct)
        {
            var adverts = await advertRepository.Get(new GetAdvertsParams
            {
                IsBlocked = false,
                IsVisible = true,
                ImpressingDate = DateTime.UtcNow,
                ImpressingTime = DateTime.UtcNow.TimeOfDay
            });

            var toShow = adverts
                .ToList()
                .Shuffle()
                .Take(ToShowCount).Select(x => new ShowAdvertDto(x)).ToList();
            
            if (ct.IsCancellationRequested)
                return toShow;
            
            await statisticRepository.Increment(toShow.Select(x => x.Id), AdvertStatisticType.Impression);
            return toShow;
        }
    }
}