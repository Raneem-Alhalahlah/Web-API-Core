using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_API__Task2.Models;

namespace Web_API__Task2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly MyDbContext _myDbContext;

        public OrdersController(MyDbContext myDbContext)
        {
            _myDbContext = myDbContext;
        }

        // GET: api/orders
        [HttpGet]
        public IActionResult GetAllOrders()
        {
            var orders = _myDbContext.Orders.ToList();
            return Ok(orders);
        }

        // GET: api/orders/{id}
        [HttpGet("{id:int}")]
        public IActionResult GetOrderById(int id)
        {
            var order = _myDbContext.Orders.FirstOrDefault(o => o.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        // GET: api/orders/byname?name={name}
        [HttpGet("byname")]
        public IActionResult GetOrderByName([FromQuery] string name)
        {
            var order = _myDbContext.Orders
                .FirstOrDefault(o => o.User.Username.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        // DELETE: api/orders/{id}
        [HttpDelete("{id:int}")]
        public IActionResult DeleteOrder(int id)
        {
            var order = _myDbContext.Orders.FirstOrDefault(o => o.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            _myDbContext.Orders.Remove(order);
            _myDbContext.SaveChanges();

            return NoContent();
        }
    }
}
