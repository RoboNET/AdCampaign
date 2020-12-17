using System;

namespace AdCampaign.DAL.Repositories.Adverts
{
    /// <summary>
    ///     Параметры для получения кампаний
    /// </summary>
    public class GetAdvertsParams
    {
        public bool? IsVisible { get; set; }

        public bool? IsBlocked { get; set; }
        
        public long? OwnerId { get; set; }

        public DateTime? ImpressingDate { get; set; }

        public TimeSpan? ImpressingTime { get; set; }

        public string UserEmail { get; set; }

        public int? ToTake { get; set; }

        public bool Shuffle { get; set; }
    }
}