using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdCampaign.DAL.Repositories.Application
{
    public interface IApplicationRepository
    {
        Task<Entities.Application> Insert(Entities.Application application);
        Task<IEnumerable<Entities.Application>> Get(long? userId, long? advertId);
    }
}