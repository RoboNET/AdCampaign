using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdCampaign.DAL;
using Microsoft.EntityFrameworkCore;

namespace AdCampaign.BLL.Services.Users
{
    public class UserService : IUserService
    {
        private readonly AdCampaignContext _context;

        public UserService(AdCampaignContext context)
        {
            _context = context;
        }
        
        public async Task BlockUser(long userId, long? blockedBy)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return;
            }

            user.BlockedDate = DateTime.UtcNow;
            user.IsBlocked = true;
            user.BlockedById = blockedBy;

            await _context.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            _context.Users.Remove(await _context.Users.FindAsync(id));
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserDto>> GetAllUsers() =>
            await _context.Users.Select(user => new UserDto
            {
                Email = user.Email,
                Id = user.Id,
                Name = user.Name,
                Phone = user.Phone,
                Role = user.Role,
                IsBlocked = user.IsBlocked
            }).ToArrayAsync();
    }
}