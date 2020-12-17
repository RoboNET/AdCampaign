using System.Collections.Generic;
using System.Threading.Tasks;
using AdCampaign.DAL.Entities;

namespace AdCampaign.DAL.Repositories.AdvertsStatistic
{
    public interface IAdvertStatisticRepository
    {
        Task Increment(long advertId, AdvertStatisticType type);
        
        Task Increment(IEnumerable<long> advertId, AdvertStatisticType type);
    }
}