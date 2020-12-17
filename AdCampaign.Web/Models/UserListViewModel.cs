using System.Collections.Generic;
using AdCampaign.DAL.Entities;

namespace AdCampaign.Models
{
    public class UserListViewModel
    {
        public IEnumerable<User> Users { get; set; }
    }
}