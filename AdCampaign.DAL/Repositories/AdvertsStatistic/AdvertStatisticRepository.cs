using System.Collections.Generic;
using System.Threading.Tasks;
using AdCampaign.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace AdCampaign.DAL.Repositories.AdvertsStatistic
{
    public class AdvertStatisticRepository : IAdvertStatisticRepository
    {
        private readonly AdCampaignContext context;

        public AdvertStatisticRepository(AdCampaignContext context)
        {
            this.context = context;
        }
        
        public async Task Increment(long advertId, AdvertStatisticType type)
        {
            await context.Database.ExecuteSqlInterpolatedAsync (
                $@"UPDATE ""AdvertsStatistics"" SET ""Value"" = ""AdvertsStatistics"".""Value"" + 1
                WHERE ""AdvertId"" = {advertId} and ""AdvertStatisticType"" = {(int)type}");
        }

        public async Task Increment(IEnumerable<long> advertIds, AdvertStatisticType type)
        {
            await context.Database.ExecuteSqlInterpolatedAsync (
                $@"UPDATE ""AdvertsStatistics"" SET ""Value"" = ""AdvertsStatistics"".""Value"" + 1
                WHERE ""AdvertId"" in {string.Join(',', advertIds)} and ""AdvertStatisticType"" = {(int)type}");
        }
    }
}