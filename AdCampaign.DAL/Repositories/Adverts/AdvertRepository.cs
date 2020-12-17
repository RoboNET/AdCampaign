using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AdCampaign.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace AdCampaign.DAL.Repositories.Adverts
{
    public class AdvertRepository : IAdvertRepository
    {
        private readonly AdCampaignContext context;

        public AdvertRepository(AdCampaignContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Advert>> Get(GetAdvertsParams advertParams)
        {
            IQueryable<Advert> query = context.Adverts
                .Include(x => x.Applications)
                .Include(x => x.AdvertStatistics)
                .Include(x => x.BlockedBy)
                .Include(x => x.Owner);

            AddClause(advertParams.UserEmail, x => x.Owner.Email == advertParams.UserEmail);
            AddClause(advertParams.IsBlocked, x => x.IsBlocked == advertParams.IsBlocked);
            AddClause(advertParams.IsVisible, x => x.IsVisible == advertParams.IsVisible);
            AddClause(advertParams.OwnerId, x => x.OwnerId == advertParams.OwnerId);
            
            AddClause(advertParams.ImpressingDate, x => x.ImpressingDateFrom >= advertParams.ImpressingDate);
            AddClause(advertParams.ImpressingDate, x => x.ImpressingDateTo <=  advertParams.ImpressingDate);
            
            AddClause(advertParams.ImpressingTime, x => x.ImpressingTimeFrom <=  advertParams.ImpressingTime);
            AddClause(advertParams.ImpressingTime, x => x.ImpressingTimeTo <=  advertParams.ImpressingTime);

            if (advertParams.Shuffle)
                query = query.OrderBy(x => Guid.NewGuid());
            
            if (advertParams.ToTake.HasValue)
                query = query.Take(advertParams.ToTake.Value);
            
            return await query.ToListAsync();
            
            void AddClause<T>(T predicate, Expression<Func<Advert, bool>> exp)
            {
                if (predicate is not null)
                    query = query.Where(exp);
            }
        }

        public async Task<Advert> Insert(Advert advert)
        {
            await context.Adverts.AddAsync(advert);
            await context.SaveChangesAsync();
            return advert;
        }

        public async Task<Advert> Update(Advert advert)
        {
            context.Adverts.Update(advert);
            await context.SaveChangesAsync();
            return advert;
        }

        public async Task Delete(long advertId)
        {
            var advert = await context.Adverts.FindAsync(advertId);
            if (advert != null)
            {
                context.Adverts.Remove(advert);
                await context.SaveChangesAsync();
            }
        }

        public Task<Advert> Get(long id) => context.Adverts
            .Include(x => x.Applications)
            .Include(x => x.AdvertStatistics)
            .Include(x => x.BlockedBy)
            .Include(x => x.Owner)
            .Include(x => x.PrimaryImage)
            .Include(x => x.SecondaryImage)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}