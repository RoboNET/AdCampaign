using System;
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

        public static string Localize(Role role)
        {
            switch (role)
            {
                case Role.Administrator:
                    return "Администратор";
                case Role.Moderator:
                    return "Модератор";
                case Role.Advertiser:
                    return "Рекламодатель";
                default:
                    throw new ArgumentOutOfRangeException(nameof(role), role, null);
            }
        }
    }
}