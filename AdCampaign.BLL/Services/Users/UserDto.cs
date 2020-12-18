using AdCampaign.DAL.Entities;

namespace AdCampaign.BLL.Services.Users
{
    public class UserDto
    {
        public long Id { get; set; }
        public Role Role { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public string Name { get; set; }

        public bool IsBlocked { get; set; }
    }
}