using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdCampaign.DAL;
using AdCampaign.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AdCampaign.BLL.Services.Users
{
    public class UserService : IUserService
    {
        private readonly AdCampaignContext _context;
        private readonly IPasswordHasherService _passwordHasherService;

        public UserService(AdCampaignContext context, IPasswordHasherService passwordHasherService)
        {
            _context = context;
            _passwordHasherService = passwordHasherService;
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

        public async Task CreateUser(string username, string password, string email, string phone, Role role)
        {
            var user = new User
            {
                Email = email,
                Name = username,
                Phone = phone,
                Role = role
            };
            user.PasswordHash = _passwordHasherService.HashPassword(user, password);
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUser(long id, string username, string password, string email, string phone, Role role)
        {
            var user = await _context.Users.FindAsync(id);
            user.Email = email;
            user.Name = username;
            user.Phone = phone;
            user.Role = role;
            user.PasswordHash = _passwordHasherService.HashPassword(user, password);
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