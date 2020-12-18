using System.Collections.Generic;
using AdCampaign.DAL.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AdCampaign.Models.Users
{
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