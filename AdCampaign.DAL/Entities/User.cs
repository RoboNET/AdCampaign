using System;
using System.Collections.Generic;

namespace AdCampaign.DAL.Entities
{
    /// <summary>
    /// Пользователь системы
    /// </summary>
    public class User
    {
        public long Id { get; set; }
        public Role Role { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Phone { get; set; }

        public string Name { get; set; }

        public bool IsBlocked { get; set; }
        public virtual User BlockedBy { get; set; }
        public long? BlockedById { get; set; }
        public DateTime? BlockedDate { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

        public virtual ICollection<Advert> Adverts { get; set; }
    }
}