using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AdCampaign.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace AdCampaign.DAL.Repositories.Application
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly AdCampaignContext _context;

        public ApplicationRepository(AdCampaignContext context) => _context = context;

        public async Task<Entities.Application> Insert(Entities.Application application)
        {
            await _context.Applications.AddAsync(application);
            await _context.SaveChangesAsync();
            return application;
        }

        public async Task<IEnumerable<Entities.Application>> Get(long? userId, long? advertId)
        {
            var query = _context.Applications.Include(application => application.Advert).AsQueryable();

            AddClause(userId, application => application.Advert.OwnerId == userId);
            AddClause(advertId, application => application.AdvertId == advertId);

            return await query.ToListAsync();

            void AddClause<T>(T predicate, Expression<Func<Entities.Application, bool>> exp)
            {
                if (predicate is not null)
                    query = query.Where(exp);
            }
        }

        public async Task<IEnumerable<Entities.Application>> Get(long advertId)
        {
            return await _context.Applications.Where(a => a.AdvertId == advertId).ToListAsync();
        }
    }
}