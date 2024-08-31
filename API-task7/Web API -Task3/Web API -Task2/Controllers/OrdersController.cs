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

        [HttpGet]
        public IActionResult GetAllOrders()
        {
            var orders = _myDbContext.Orders.ToList();
            return Ok(orders);
        }

        [HttpGet ("GetOrderById") ]
        public IActionResult GetOrderById([FromQuery]int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var order = _myDbContext.Orders.FirstOrDefault(o => o.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpGet("byname")]

        public IActionResult GetOrderByName([FromQuery] string name)
        {

            if (name == null)
            {
                return BadRequest();
            }

            var order = _myDbContext.Orders.Where(s=>s.User.Username==name).ToList();   
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }


        [HttpDelete("delete/{id:int}")]
        public IActionResult DeleteOrder([FromQuery]  int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

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
