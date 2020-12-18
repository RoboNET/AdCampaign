using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            await context.Database.ExecuteSqlInterpolatedAsync(
                $@"UPDATE ""AdvertsStatistics"" SET ""Value"" = ""AdvertsStatistics"".""Value"" + 1
                WHERE ""AdvertId"" = {advertId} and ""AdvertStatisticType"" = {(int) type}");
        }

        public async Task Increment(IEnumerable<long> advertIds, AdvertStatisticType type)
        {
            StringBuilder paramsValues = new StringBuilder();
            var adverts = advertIds.ToList();

            if (adverts.Count == 0)
                return;

            for (var index = 1; index <= adverts.Count; index++)
            {
                paramsValues.Append("{");
                paramsValues.Append(index);
                paramsValues.Append("}");
                if (index != adverts.Count)
                {
                    paramsValues.Append(",");
                }
            }

            var parameters = new List<object>()
            {
                (int) type
            };
            
            parameters.AddRange(advertIds.Select(a=>(object)a));
            
            await context.Database.ExecuteSqlRawAsync(
                @"UPDATE ""AdvertsStatistics"" SET ""Value"" = ""AdvertsStatistics"".""Value"" + 1
                WHERE ""AdvertId"" in (" + paramsValues + @") and ""AdvertStatisticType"" = {0}", parameters);
        }
    }
}