using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using SerPro.Core.IManagers;
using SerPro.Core.Managers;

namespace SerPro.API.Controllers
{
    [RoutePrefix("api/photo")]
    public class PhotoController : ApiController
    {
        private readonly IPhotoManager _photoManager;

        public PhotoController()
            : this(new PhotoManager(HttpRuntime.AppDomainAppPath + @"\Album"))
        {
        }

        public PhotoController(IPhotoManager photoManager)
        {
            _photoManager = photoManager;
        }

        // GET: api/Photo
        [Route("get")]
        public async Task<IHttpActionResult> Get()
        {
            var results = await _photoManager.Get();
            return Ok(new { photos = results });
        }

        // POST: api/Photo
        [Route("post")]
        public async Task<IHttpActionResult> Post()
        {
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent("form-data"))
            {
                return BadRequest("Unsupported media type");
            }

            try
            {
                var photos = await _photoManager.Add(Request);
                return Ok(new { Message = "Photos uploaded ok", Photos = photos });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }

        }

        // DELETE: api/Photo/5
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(string fileName)
        {
            if (!this._photoManager.FileExists(fileName))
            {
                return NotFound();
            }

            var result = await this._photoManager.Delete(fileName);

            if (result.Successful)
            {
                return Ok(new { message = result.Message });
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
    }
}
