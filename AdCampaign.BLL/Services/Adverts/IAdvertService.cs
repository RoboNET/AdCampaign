using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AdCampaign.BLL.Services.Adverts.DTO;
using AdCampaign.Common;
using AdCampaign.DAL.Entities;

namespace AdCampaign.BLL.Services.Adverts
{
    public interface IAdvertService
    {
        /// <summary>
        /// Получить кампании к показу
        /// </summary>
        Task<Result<IEnumerable<ShowAdvertDto>>> GetAdvertsToShow(CancellationToken ct);

        /// <summary>
        /// Увеличить счетчик статистики рекламы
        /// </summary>
        Task IncrementAdvertsStats(long id, AdvertStatisticType statisticType);
        
        /// <summary>
        /// Получить капмпанию
        /// </summary>
        Task<Result<Advert>> Get(long userId, Role role, long id);

        Task<Result<Advert>> Create(long userId, AdvertDto dto, File primaryImage, File secondaryImage);
        
        /// <summary>
        /// Обновить капмпанию
        /// </summary>
        Task<Result<Advert>> Update(long userId, Role role, AdvertDto dto, File primaryImage, File secondaryImage);
        
        Task<Result> Delete(long id, long userId, Role role);
        
        Task<Result> ChangeBlock(long userId, Role role, long id, bool block);

    }
}