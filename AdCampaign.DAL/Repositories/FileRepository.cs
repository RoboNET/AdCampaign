using System.Threading.Tasks;
using AdCampaign.DAL.Entities;

namespace AdCampaign.DAL.Repositories
{
    public class FileRepository : IFileRepository
    {
        private readonly AdCampaignContext _db;

        public FileRepository(AdCampaignContext db)
        {
            _db = db;
        }
        
        public async Task Create(string fileName, byte[] content)
        {
            var file = new File
            {
                Name = fileName,
                Size = content.Length,
                Content = content
            };

            await _db.Files.AddAsync(file);
            await _db.SaveChangesAsync();
        }

        public async Task<File?> Get(long id)
        {
            return await _db.FindAsync<File>(id);
        }
    }
}