#nullable enable
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AdCampaign.DAL;
using AdCampaign.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AdCampaign.Authetication
{
    public class AuthenticationService
    {
        private readonly AdCampaignContext _db;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AuthenticationService(AdCampaignContext db, IPasswordHasher<User> passwordHasher)
        {
            _db = db;
            _passwordHasher = passwordHasher;
        }

        public async Task<ClaimsPrincipal?> CreatePrincipal(string username, string password)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == username);
            if (user == null)
                return null;

            var passwordVerification = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            if (passwordVerification == PasswordVerificationResult.Failed)
                return null;

            var claimsIdentity = new ClaimsIdentity(new List<Claim>
            {
                new(ClaimTypes.Name, user.Name),
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.Role, user.Role.ToString())
            },"Login");
            return new ClaimsPrincipal(claimsIdentity);
        }
    }
}