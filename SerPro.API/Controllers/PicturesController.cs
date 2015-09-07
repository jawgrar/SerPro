using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using SerPro.Core.IManagers;
using SerPro.Core.Managers;

namespace SerPro.API.Controllers
{
    [RoutePrefix("api/pictures")]
    public class PicturesController : ApiController
    {
        private readonly IPictureManager _pictureManager;

        public PicturesController()
            : this(new PictureManager(HttpRuntime.AppDomainAppPath + @"\Album"))
        {
        }

        public PicturesController(IPictureManager pictureManager)
        {
            _pictureManager = pictureManager;
        }

        // GET: api/pictures
        [Route("get")]
        public async Task<IHttpActionResult> Get()
        {
            var results = await _pictureManager.Get();
            return Ok(new { pictures = results });
        }

        // POST: api/pictures
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
                var pictures = await _pictureManager.Add(Request);
                return Ok(new { Message = "pictures uploaded ok", pictures });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }

        }

        // DELETE: api/pictures/5
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(string fileName)
        {
            if (!this._pictureManager.FileExists(fileName))
            {
                return NotFound();
            }

            var result = await this._pictureManager.Delete(fileName);

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
