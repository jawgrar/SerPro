using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace SerPro.API.Controllers
{
    [RoutePrefix("api/Orders")]
    public class OrdersController : ApiController
    {
        [Authorize]
        [Route("")]
        public IHttpActionResult Get()
        {
            return Ok(Order.CreateOrders());
        }

    }

    #region Helpers

    public class Order
    {
        public int OrderID { get; set; }
        public string CustomerName { get; set; }
        public string ShipperCity { get; set; }
        public Boolean IsShipped { get; set; }

        public static List<Order> CreateOrders()
        {
            List<Order> OrderList = new List<Order> 
            {
                new Order {OrderID = 10248, CustomerName = "Bilbo Baggins", ShipperCity = "The Shire", IsShipped = true },
                new Order {OrderID = 10249, CustomerName = "Gandalf the Grey", ShipperCity = "Middle Earth", IsShipped = false},
                new Order {OrderID = 10250, CustomerName = "Tauriel", ShipperCity = "Mirkwood", IsShipped = false },
                new Order {OrderID = 10251, CustomerName = "Sauron", ShipperCity = "Dol Guldur", IsShipped = false},
                new Order {OrderID = 10252, CustomerName = "Thorin Oakenshield", ShipperCity = "Lonely Mountain", IsShipped = true}
            };

            return OrderList;
        }
    }

    #endregion
}
