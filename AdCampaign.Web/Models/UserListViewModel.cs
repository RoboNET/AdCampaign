using System.Collections.Generic;
using AdCampaign.BLL.Services.Users;

namespace AdCampaign.Models
{
    public class UserListViewModel
    {
        public IEnumerable<UserDto> Users { get; set; }
    }
}