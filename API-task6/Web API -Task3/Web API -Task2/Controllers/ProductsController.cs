using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
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


        [HttpGet]
        public IActionResult GetAllProducts()
        {
            var products = _myDbContext.Products.ToList();
            return Ok(products);
        }



        [HttpGet("GetProductById/")]
        public IActionResult GetProductById([FromQuery] int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var product = _myDbContext.Products.FirstOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpGet("GetProductByCategoryId/{id:int}")]
        public IActionResult GetProductByCategoryId(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var products = _myDbContext.Products.Where(p => p.CategoryId == id).ToList();



            return Ok(products);
        }

        [HttpGet]
        [Route("api/Products/OrderByPriceDesc")]
        public IActionResult GetProductsOrderedByPriceDesc()
        {
            var products = _myDbContext.Products.OrderByDescending(p => p.Price).ToList();
            return Ok(products);
        }


        [HttpGet("GetProductByName")]
        public IActionResult GetProductByName([FromQuery] string name)
        {
            if (name == null)
            {
                return BadRequest();
            }


            var product = _myDbContext.Products
                .FirstOrDefault(p => p.ProductName == name);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }


        [HttpDelete("delete/{id:int}")]
        public IActionResult DeleteProduct([FromQuery] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var product = _myDbContext.Products.FirstOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            _myDbContext.Products.Remove(product);
            _myDbContext.SaveChanges();

            return NoContent();
        }



        //problem solving
        [HttpGet("last5product")]
        public IActionResult last5product()
        {

            var product = _myDbContext.Products.OrderBy(p => p.ProductName).ToList().TakeLast(5);//method1
            //var product = _myDbContext.Products.OrderBy(p => p.ProductName).Reverse().Take(5).Reverse().ToList();//method2
            //var product = _myDbContext.Products.OrderByDescending(p => p.ProductName).Take(5).OrderBy(p => p.ProductName).ToList();//method3


            return Ok(product);
        }



    }
}
