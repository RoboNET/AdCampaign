using System.Collections.Generic;
using AdCampaign.DAL.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AdCampaign.Models
{
    public class UserEditViewModel
    {
        public long UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Role Role { get; set; }
        public bool IsActive { get; set; }

        public List<SelectListItem> Roles { get; set; } = new()
        {
            new SelectListItem {Value = Role.Administrator.ToString("D"), Text = "Администратор"},
            new SelectListItem {Value = Role.Moderator.ToString("D"), Text = "Модератор"},
            new SelectListItem {Value = Role.Advertiser.ToString("D"), Text = "Рекламодатель"}
        };
    }

    public static class UserRoles
    {
        public static List<SelectListItem> GetList() => new()
        {
            new SelectListItem {Value = Role.Administrator.ToString("D"), Text = "Администратор"},
            new SelectListItem {Value = Role.Moderator.ToString("D"), Text = "Модератор"},
            new SelectListItem {Value = Role.Advertiser.ToString("D"), Text = "Рекламодатель"}
        };
    }
}