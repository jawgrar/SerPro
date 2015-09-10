using SerPro.API.Infrastructure;
using SerPro.API.Models;
using SerPro.Core.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace SerPro.API.Controllers
{
    [RoutePrefix("api/Product")]
    public class ProductController : ApiController
    {
        private ApplicationDbContext _dbContext = new ApplicationDbContext();

        // POST: api/product
        [HttpPost]
        [Route("post")]
        public IHttpActionResult Post(ProductModel objProductModel)
        {
            try
            {
                var img = objProductModel.Picture.Replace("data:image/png;base64,", "");
                byte[] bytes = Convert.FromBase64String(img);

                Image image = new Image();
                image.Photo = bytes;

                _dbContext.Image.Add(image);
                _dbContext.SaveChanges();

                int newImageId = image.ImageId;

                //save product to db

                Product objProduct = new Product();
                objProduct.Name = objProductModel.Name;
                objProduct.Desription = objProductModel.description;
                objProduct.Price = Convert.ToDouble(objProductModel.Price);
                objProduct.ImageId = newImageId;

                _dbContext.Product.Add(objProduct);
                _dbContext.SaveChanges();

                return Ok(new { Message = "Product saved successfully.", objProduct });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }

        }

    }
}
