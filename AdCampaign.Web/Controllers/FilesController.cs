using System.Threading.Tasks;
using AdCampaign.DAL.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdCampaign.Controllers
{
    [Route("file")]
    public class FilesController : Controller
    {
        private readonly IFileRepository _fileRepository;

        public FilesController(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        [Route("{id}")]
        [HttpGet]
        [AllowAnonymous]
        [ResponseCache(Duration = 604800)]
        public async Task<IActionResult> Get(long id)
        {
            var file = await _fileRepository.Get(id);
            if (file == null)
            {
                return NotFound();
            }

            return File(file.Content, System.Net.Mime.MediaTypeNames.Image.Jpeg, file.Name);
        }
    }
}