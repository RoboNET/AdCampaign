using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AdCampaign.BLL.Services.Adverts.DTO;
using AdCampaign.Common;

namespace AdCampaign.BLL.Services.Adverts
{
    public interface IAdvertService
    {
        /// <summary>
        /// Получить кампании к показу
        /// </summary>
        Task<Result<IEnumerable<ShowAdvertDto>>> GetAdvertsToShow(CancellationToken ct);
    }
}