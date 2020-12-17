using System;
using System.Linq;
using System.Threading.Tasks;
using AdCampaign.BLL.Services.Users;
using AdCampaign.DAL;
using AdCampaign.DAL.Entities;
using AdCampaign.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdCampaign.Controllers
{
    public class UserController :  Controller
    {
        private readonly AdCampaignContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IUserService _userService;

        public UserController(AdCampaignContext context, IPasswordHasher<User> passwordHasher, IUserService userService)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _userService = userService;
        }

        [HttpGet("Users")]
        public async Task<ViewResult> List()
        {
            var users = await _context.Users.ToArrayAsync();
            Console.WriteLine(string.Join(',', users.Select(user => user.Email)));
            return View(new UserListViewModel
            {
                Users = users
            });
        }
        
        [HttpGet]
        public async Task<ViewResult> Create()
        {
            var users = await _context.Users.ToArrayAsync();
            Console.WriteLine(string.Join(',', users.Select(user => user.Email)));
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(
            string username,
            string password,
            string email,
            string phone,
            Role role)
        {
            var user = new User
            {
                Email = email,
                Name = username,
                Phone = phone,
                Role = role
            };
            user.PasswordHash = _passwordHasher.HashPassword(user, password);
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return RedirectToAction("List", "User");
        }
        
        [HttpGet("User/{id}/edit")]
        public async Task<IActionResult> Edit(long id)
        {
            var user = await _context.Users.FirstAsync(u => u.Id == id);
            return View(new UserEditViewModel
            {
                Name = user.Name,
                Email = user.Email,
                Phone = user.Phone,
                Role = user.Role,
                IsActive = !user.IsBlocked,
                UserId = user.Id
            });
        }
        
        [HttpPost("User/{id}/edit")]
        public async Task<IActionResult> Edit(
            long id,
            string username,
            string email,
            string phone,
            string password,
            Role role)
        {
            var user = await _context.Users.FirstAsync(u => u.Id == id);
            user.Email = email;
            user.Name = username;
            user.Phone = phone;
            user.Role = role;
            user.PasswordHash = _passwordHasher.HashPassword(user, password);
            await _context.SaveChangesAsync();
            return RedirectToAction("Edit", "User",new {id});
        }
        
        [HttpPost("User/{id}/block")]
        public async Task<IActionResult> Block(long id)
        {
            await _userService.BlockUser(id, null);
            return RedirectToAction("Edit", "User",new {id});
        }
    }
}