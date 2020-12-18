using System.Collections.Generic;
using AdCampaign.BLL.Services.Users;

namespace AdCampaign.Models.Users
{
    public class UserListViewModel
    {
        public IEnumerable<UserDto> Users { get; set; }
    }
}