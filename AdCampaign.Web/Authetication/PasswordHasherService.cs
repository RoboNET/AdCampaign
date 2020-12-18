using AdCampaign.BLL.Services.Users;
using AdCampaign.DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace AdCampaign.Authetication
{
    public class PasswordHasherService : IPasswordHasherService
    {
        private readonly IPasswordHasher<User> _passwordHasher;

        public PasswordHasherService(IPasswordHasher<User> passwordHasher)
        {
            _passwordHasher = passwordHasher;
        }

        public string HashPassword(User user, string password)
        {
            return _passwordHasher.HashPassword(user, password);
        }
    }
}