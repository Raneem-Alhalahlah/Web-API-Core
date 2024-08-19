using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task1_WepAPICore.Models;

namespace Task1_WepAPICore.Controllers
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
        public IActionResult getAllProducts()
        {

            var products = _myDbContext.Products.ToList();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {

            var productById = _myDbContext.Products
                                              .Include(p => p.Category)  
                                              .FirstOrDefault(c => c.ProductId == id);
            return Ok(productById);
        }
    }

}

