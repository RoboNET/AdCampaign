using AdCampaign.DAL.Entities;

namespace AdCampaign.BLL.Services.Adverts.DTO
{
    /// <summary>
    ///  Кампания к показу
    /// </summary>
    public class ShowAdvertDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public RequestType RequestType { get; set; }
        
        public long? PrimaryImageId { get; set; }
        public long? SecondaryImageId { get; set; }


        //todo automapper   
        public ShowAdvertDto(Advert advert)
        {
            Id = advert.Id;
            Name = advert.Name;
            RequestType = advert.RequestType;
            PrimaryImageId = advert.PrimaryImageId;
            SecondaryImageId = advert.SecondaryImageId;
        }
    }
}