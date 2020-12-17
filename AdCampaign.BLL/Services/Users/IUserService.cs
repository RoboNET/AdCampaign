using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdCampaign.BLL.Services.Users
{
    public interface IUserService
    {
        Task BlockUser(long userId, long? blockedBy);
        Task Delete(long id);
        Task<IEnumerable<UserDto>> GetAllUsers();
    }
}