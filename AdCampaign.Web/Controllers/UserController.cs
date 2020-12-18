using System.Threading.Tasks;
using AdCampaign.Authetication;
using AdCampaign.BLL.Services.Users;
using AdCampaign.DAL.Entities;
using AdCampaign.Models;
using AdCampaign.Models.Users;
using Microsoft.AspNetCore.Mvc;

namespace AdCampaign.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("Users")]
        public async Task<ViewResult> List()
        {
            var users = await _userService.GetAllUsers();
            return View(new UserListViewModel
            {
                Users = users
            });
        }

        [HttpGet]
        public async Task<ViewResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserCreateRequest dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var result = await _userService.CreateUser(dto.Name, dto.Password, dto.Email, dto.Phone, dto.Role);
            if (!result.Ok)
            {
                ViewData["Errors"] = result.Errors;
                return View(dto);
            }

            return RedirectToAction("List", "User");
        }

        [HttpGet("User/{id}/edit")]
        public async Task<IActionResult> Edit(long id)
        {
            var user = await _userService.Get(id);
            return View(new UserEditRequest()
            {
                Name = user.Name,
                Email = user.Email,
                Phone = user.Phone,
                Role = user.Role,
                IsActive = !user.IsBlocked,
                Id = user.Id
            });
        }

        [HttpPost("User/{id}/edit")]
        public async Task<IActionResult> Edit(long id, UserEditRequest dto)
        {
            if (!ModelState.IsValid)
            {
                var user = await _userService.Get(id);
                return View("Edit", new UserEditRequest()
                {
                    Email = dto.Email,
                    Name = dto.Name,
                    Phone = dto.Phone,
                    Role = dto.Role,
                    Id = dto.Id,
                    IsActive = !user.IsBlocked 
                });
            }

            await _userService.UpdateUser(dto.Id, dto.Name, dto.Password, dto.Email, dto.Phone, dto.Role);
            return RedirectToAction("Edit", "User", new {id});
        }

        [HttpPost("User/block")]
        public async Task<IActionResult> Block(long id)
        {
            await _userService.BlockUser(id, User.GetId());
            return RedirectToAction("Edit", "User", new {id});
        }

        [HttpPost("User/unblock")]
        public async Task<IActionResult> UnBlock(long id)
        {
            await _userService.UnBlockUser(id);
            return RedirectToAction("Edit", "User", new {id});
        }

        [HttpPost("User/delete")]
        public async Task<IActionResult> Delete(long id)
        {
            await _userService.Delete(id);
            return RedirectToAction("List", "User");
        }
    }
}