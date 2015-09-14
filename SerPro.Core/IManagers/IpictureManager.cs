using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using SerPro.Core.Entity;
using SerPro.Core.Managers.Picture;

namespace SerPro.Core.IManagers
{
    public interface IPictureManager
    {
        Task<IEnumerable<PictureView>> Get();
        Task<PictureActionResult> Delete(string fileName);

        Task<byte[]> GetImagebyte(HttpRequestMessage request);
        Task<IEnumerable<PictureView>> Add(HttpRequestMessage request);
        bool FileExists(string fileName);
    }
}
