using Microsoft.AspNetCore.Mvc.Rendering;

namespace AdCampaign.Models
{
    public static class RequestTypeSelectList
    {
        public static SelectListItem[] Items = new SelectListItem[]
        {
            new("Телефон", "1"),
            new("Email", "2"),
            new("Телефон + Email", "3"),
        };
    }
}