using System.Threading.Tasks;
using AdCampaign.DAL.Entities;

namespace AdCampaign.DAL.Repositories
{
    /// <summary>
    /// Репозиторий для работы с файлами
    /// </summary>
    public interface IFileRepository
    {
        Task<File> Create(string fileName, byte[] content);

        Task<File?> Get(long id);
        
        Task Delete(File file);
    }
}