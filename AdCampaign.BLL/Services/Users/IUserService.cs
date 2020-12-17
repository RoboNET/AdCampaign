using System.Threading.Tasks;
using AdCampaign.DAL.Entities;

namespace AdCampaign.BLL.Services.Users
{
    public interface IUserService
    {
        Task BlockUser(long userId, long? blockedBy);
        Task Delete(long id);
        Task CreateUser(string username, string password, string email, string phone, Role role);
        Task UpdateUser(long id, string username, string password, string email, string phone, Role role);
    }
}