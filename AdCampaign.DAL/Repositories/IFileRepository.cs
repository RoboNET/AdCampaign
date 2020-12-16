using System.Threading.Tasks;
using AdCampaign.DAL.Entities;

namespace AdCampaign.DAL.Repositories
{
    /// <summary>
    /// Репозиторий для работы с файлами
    /// </summary>
    public interface IFileRepository
    {
        Task Create(string fileName, long size, byte[] content);

        Task<File?> Get(long id);
    }
}