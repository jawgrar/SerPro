
using SerPro.API.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SerPro.API.Controllers
{
    [Authorize]
    [RoutePrefix("api/orders")]
    public class OrdersController : ApiController
    {
        [HttpPut]
        [Route("refund/{orderId}")]
        public IHttpActionResult RefundOrder([FromUri]string orderId)
        {
            return Ok();
        }

        [Route("")]
        public IHttpActionResult Get()
        {
            return Ok();
        }
    }
}
