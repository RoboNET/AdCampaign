namespace AdCampaign.DAL.Entities
{
    /// <summary>
    /// Статистика рекламной кампании
    /// </summary>
    public class AdvertStatistic
    {
        public long Id { get; set; }
        public AdvertStatisticType AdvertStatisticType { get; set; }
        public long Value { get; set; }

        public long AdvertId { get; set; }
        public virtual Advert Advert { get; set; }
    }
}