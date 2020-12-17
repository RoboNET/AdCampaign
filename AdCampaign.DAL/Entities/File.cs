namespace AdCampaign.DAL.Entities
{
    public class File
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long Size { get; set; }
        public byte[] Content { get; set; }
     }
}