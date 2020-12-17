using System;
using AdCampaign.DAL.Entities;

namespace AdCampaign.BLL.Services.Adverts.DTO
{
    /// <summary>
    ///     Модель для создания кампании
    /// </summary>
    public class CreateAdvertDto
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public RequestType RequestType { get; set; }

        public long OwnerId { get; set; }

        public bool IsVisible { get; set; }

        public bool IsBlocked { get; set; }

        public long? BlockedById { get; set; }

        public DateTime? BlockedDate { get; set; }

        public DateTime DateUpdated { get; set; }

        public DateTime ImpressingDateFrom { get; set; }

        public DateTime ImpressingDateTo { get; set; }

        public TimeSpan ImpressingTimeFrom { get; set; }

        public TimeSpan ImpressingTimeTo { get; set; }

        public long? PrimaryImageId { get; set; }

        public long? SecondaryImageId { get; set; }
    }
}