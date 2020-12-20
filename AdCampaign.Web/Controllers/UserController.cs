using System.Threading.Tasks;
using AdCampaign.Authetication;
using AdCampaign.BLL.Services.Users;
using AdCampaign.Common;
using AdCampaign.DAL.Entities;
using AdCampaign.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdCampaign.Controllers
{
    [Authorize(Policy = "CanEditUsers")]
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
            var users = await _userService.GetUsers(User.GetRole() == Role.Moderator);
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
            
            if (!User.IsAdministrator() && dto.Role != Role.Advertiser)
            {
                ViewData["Errors"] = new [] {new Error("Вы можете создать только рекламодателя", "400")};
                return View(dto);
            }
    
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
            if (user.Role == Role.Administrator && User.GetRole() != Role.Administrator)
                return NotFound();

            return View(new UserEditRequest
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
            if (await CheckRoles(id))
                return NotFound();
            
            if (!ModelState.IsValid)
            {
                var user = await _userService.Get(id);
                return View("Edit", new UserEditRequest
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
            if (!await CanChangeBlockingStatus(id))
            {
                ViewData["Errors"] = new[]{ new Error("Операция запрещена", "403")};

                var user = await _userService.Get(id);
                return View("Edit", new UserEditRequest
                {
                    Email = user.Email,
                    Name = user.Name,
                    Phone = user.Phone,
                    Role = user.Role,
                    Id = user.Id,
                    IsActive = !user.IsBlocked
                });
            } 
            
            await _userService.BlockUser(id, User.GetId());
            return RedirectToAction("Edit", "User", new {id});
        }

        [HttpPost("User/unblock")]
        public async Task<IActionResult> UnBlock(long id)
        {
            if (!await CanChangeBlockingStatus(id))
            {
                ViewData["Errors"] = new[]{ new Error("Операция запрещена", "403")};
                var user = await _userService.Get(id);
                return View("Edit", new UserEditRequest
                {
                    Email = user.Email,
                    Name = user.Name,
                    Phone = user.Phone,
                    Role = user.Role,
                    Id = user.Id,
                    IsActive = !user.IsBlocked
                });
            }
            
            await _userService.UnBlockUser(id);
            return RedirectToAction("Edit", "User", new {id});
        }

        [HttpPost("User/delete")]
        public async Task<IActionResult> Delete(long id)
        {
            if (await CheckRoles(id))
                return NotFound();

            if (User.GetId() == id)
            {
                ViewData["Errors"] = new[]{ new Error("Операция запрещена", "403")};
                var users = await _userService.GetUsers(User.GetRole() == Role.Moderator);
                return View("List", new UserListViewModel
                {
                    Users = users
                });
            }
            
            await _userService.Delete(id);
            return RedirectToAction("List", "User");
        }

        private async Task<bool> CanChangeBlockingStatus(long editableUserId)
        {
            var editableUser = await _userService.Get(editableUserId);
            if (editableUserId == User.GetId())
                return false;

            if (!User.IsAdministrator() && editableUser.Role == Role.Administrator)
                return false;

            if (!User.IsAdministratorOrModerator())
                return false;

            return true;
        }
        
        private async Task<bool> CheckRoles(long editableUserId)
        {
            var editableUser = await _userService.Get(editableUserId);
            return editableUser.Role == Role.Administrator && !User.IsAdministrator();
        }
    }
}