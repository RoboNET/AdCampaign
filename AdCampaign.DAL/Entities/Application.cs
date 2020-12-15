using System;

namespace AdCampaign.DAL.Entities
{
    /// <summary>
    /// Ответ пользователя на форму ввода в рекламе
    /// </summary>
    public class Application
    {
        public long Id { get; set; }

        public string Phone { get; set; }
        public string Email { get; set; }

        public long AdvertId { get; set; }
        public virtual Advert Advert { get; set; }

        public DateTime DateCreated { get; set; }
    }
}