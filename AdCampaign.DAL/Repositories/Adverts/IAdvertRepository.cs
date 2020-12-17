using System.Collections.Generic;
using System.Threading.Tasks;
using AdCampaign.DAL.Entities;

namespace AdCampaign.DAL.Repositories.Adverts
{
    public interface IAdvertRepository
    {
        Task<IEnumerable<Advert>> Get(GetAdvertsParams advertParams);
        Task<Advert> Insert(Advert advert);
        Task<Advert> Update(Advert advert);
        Task Delete(long advertId);
        Task<Advert> Get(long id);
    }
}