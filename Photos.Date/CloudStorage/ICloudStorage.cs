using System.IO;
using System.Threading.Tasks;

namespace Photos.Date.CloudStorage
{
    public interface ICloudStorage
    {
        Task<string> UploadFileAsync(Stream imageStream, string fileName);
        Task DeleteFileAsync(string fileName);
    }
}
