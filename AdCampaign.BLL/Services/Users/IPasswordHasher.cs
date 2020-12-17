using AdCampaign.DAL.Entities;

namespace AdCampaign.BLL.Services.Users
{
    public interface IPasswordHasherService
    {
        string HashPassword(User user, string password);
    }
}