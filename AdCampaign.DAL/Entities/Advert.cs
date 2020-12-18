using System;
using System.Collections.Generic;

namespace AdCampaign.DAL.Entities
{
    /// <summary>
    /// Рекламная кампания
    /// </summary>
    public class Advert
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public RequestType RequestType { get; set; }

        public long OwnerId { get; set; }
        public virtual User Owner { get; set; }

        public bool IsVisible { get; set; }
        
        public bool ImpressingAlways { get; set; } 

        public bool IsBlocked { get; set; }
        public virtual User BlockedBy { get; set; }
        public long? BlockedById { get; set; }
        public DateTime? BlockedDate { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

        public DateTime ImpressingDateFrom { get; set; }
        public DateTime ImpressingDateTo { get; set; }

        public TimeSpan? ImpressingTimeFrom { get; set; }
        public TimeSpan? ImpressingTimeTo { get; set; }
        
        public virtual ICollection<AdvertStatistic> AdvertStatistics { get; set; }
        public virtual ICollection<Application> Applications { get; set; }

        public long? PrimaryImageId { get; set; }
        public virtual File PrimaryImage { get; set; }
        
        public long? SecondaryImageId { get; set; }
        public virtual File SecondaryImage { get; set; }
    }
}