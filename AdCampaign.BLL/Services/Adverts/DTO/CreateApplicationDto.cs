namespace AdCampaign.BLL.Services.Adverts.DTO
{
    public class ApplicationListItemDto : CreateApplicationDto
    {
        public string AdvertName { get; set; }
    }

    public class CreateApplicationDto
    {
        public string Phone { get; set; }
        public string Email { get; set; }
        public long AdvertId { get; set; }
    }
}