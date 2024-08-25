using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_API__Task2.Models;

namespace Web_API__Task2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly MyDbContext _myDbContext;

        public ProductsController(MyDbContext myDbContext)
        {
            _myDbContext = myDbContext;
        }

        // GET: api/products
        [HttpGet]
        public IActionResult GetAllProducts()
        {
            var products = _myDbContext.Products.ToList();
            return Ok(products);
        }

        // GET: api/products/{id}
        [HttpGet("{id:int}")]
        public IActionResult GetProductById(int id)
        {
            if (id > 10)
            {
                return BadRequest("ID must be 10 or less.");
            }

            var product = _myDbContext.Products.FirstOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // GET: api/products/byname?name={name}
        [HttpGet("byname")]
        public IActionResult GetProductByName([FromQuery] string name)
        {
            var product = _myDbContext.Products
                .FirstOrDefault(p => p.ProductName.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // DELETE: api/products/{id}
        [HttpDelete("{id:int}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = _myDbContext.Products.FirstOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            _myDbContext.Products.Remove(product);
            _myDbContext.SaveChanges();

            return NoContent();
        }
    }
}
