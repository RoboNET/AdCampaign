using System;
using AdCampaign.DAL.Entities;

namespace AdCampaign.BLL.Services.Adverts.DTO
{
    /// <summary>
    ///     Модель для создания кампании
    /// </summary>
    public class AdvertDto
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public RequestType RequestType { get; set; }
        
        public bool IsVisible { get; set; }
        
        public bool ImpressingAlways { get; set; }
        
        public DateTime ImpressingDateFrom { get; set; }

        public DateTime ImpressingDateTo { get; set; }

        public TimeSpan? ImpressingTimeFrom { get; set; }

        public TimeSpan? ImpressingTimeTo { get; set; }
    }
}