using System.IO;
using Microsoft.AspNetCore.Http;
using File = AdCampaign.DAL.Entities.File;

namespace AdCampaign.Extensions
{
    public static class FileExt
    {
        public static File ToFile(this IFormFile file) => new File
        {
            Content = ToArray(file),
            Name = file.FileName,
            Size = file.Length
        };
        private static byte[] ToArray(IFormFile file)
        {
            using var ms = new MemoryStream();
            file.CopyTo(ms);
            return ms.ToArray();
        }
    }
}