using System.Collections.Generic;
using System.Threading.Tasks;
using AdCampaign.Common;
using AdCampaign.DAL.Entities;

namespace AdCampaign.BLL.Services.Users
{
    public interface IUserService
    {
        Task BlockUser(long userId, long? blockedBy);
        
        Task UnBlockUser(long userId);
        Task Delete(long id);
        Task<Result> CreateUser(string username, string password, string email, string phone, Role role);
        Task UpdateUser(long id, string username, string password, string email, string phone, Role role);
        Task<IEnumerable<UserDto>> GetAllUsers();
        Task<UserDto> Get(long id);
    }
}