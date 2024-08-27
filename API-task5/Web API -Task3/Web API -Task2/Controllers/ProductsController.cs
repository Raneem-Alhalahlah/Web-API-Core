using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_API__Task2.DTOs;
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
    public IActionResult GetProductById([FromQuery]  int id)
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
        public IActionResult GetProductByCategoryId( int id)
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
        public IActionResult DeleteProduct([FromQuery]  int id)
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
        //to post data from swagger
        [HttpPost]
        public IActionResult AddProduct([FromForm] ProductRequest productOTD)
        {
            var uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            if (!Directory.Exists(uploadsFolderPath))
            {
                Directory.CreateDirectory(uploadsFolderPath);
            }
            var filePath = Path.Combine(uploadsFolderPath, productOTD.ProductImage.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                productOTD.ProductImage.CopyToAsync(stream);
            }

            if (productOTD == null)
            {
                return BadRequest("not valid");
            }

            var y = new Product
            {
                ProductName = productOTD.ProductName,
                Description = productOTD.Description,
                Price = productOTD.Price,
                ProductImage = productOTD.ProductImage.FileName,
                CategoryId = productOTD.CategoryId



            };
            _myDbContext.Products.Add(y);
            _myDbContext.SaveChanges();

            return Ok("Category added successfully!");


        }


        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromForm] ProductRequest productOTD)
        {
            //var uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            //if (!Directory.Exists(uploadsFolderPath))
            //{
            //    Directory.CreateDirectory(uploadsFolderPath);
            //}
            //var filePath = Path.Combine(uploadsFolderPath, productOTD.ProductImage.FileName);
            //using (var stream = new FileStream(filePath, FileMode.Create))
            //{
            //    productOTD.ProductImage.CopyToAsync(stream);
            //}


            var existingProduct = _myDbContext.Products.FirstOrDefault(x => x.ProductId == id);

            if (existingProduct == null)
            {

                return NotFound();
            }


            existingProduct.ProductName = productOTD.ProductName;
            existingProduct.Description = productOTD.Description;
            existingProduct.Price = productOTD.Price;
            existingProduct.ProductImage = productOTD.ProductImage.FileName;
            existingProduct.CategoryId = productOTD.CategoryId;



            _myDbContext.Update(existingProduct);
            _myDbContext.SaveChanges();
            return Ok();







        }


    }
}
