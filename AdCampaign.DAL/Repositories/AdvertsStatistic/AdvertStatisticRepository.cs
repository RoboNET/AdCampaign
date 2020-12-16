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
            using var transaction = context.Database.BeginTransactionAsync();
            await context.Database.ExecuteSqlRawAsync(
                $@"UPDATE ""AdvertsStatistics"" SET ""Value"" = ""AdvertsStatistics"".""Value"" + 1
                WHERE ""AdvertId"" = {advertId} and ""AdvertStatisticType"" = {(int)type}");
        }

        public Task Increment(IEnumerable<long> advertId, AdvertStatisticType type)
        {
            throw new System.NotImplementedException();
        }
    }
}