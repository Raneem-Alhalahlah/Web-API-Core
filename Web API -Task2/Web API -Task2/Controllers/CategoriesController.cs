using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_API__Task2.DTOs;
using Web_API__Task2.Models;

namespace Web_API__Task2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {

        private readonly MyDbContext _myDbContext;
        public CategoriesController(MyDbContext myDbContext)
        {
            _myDbContext = myDbContext;
        }



        [HttpGet]
        [Route("getAllCategories")]
        public IActionResult GetAllCategories()
        {
            var categories = _myDbContext.Categories.ToList();
            return Ok(categories);
        }




        [HttpGet]
        [Route("get/{id:int:min(5)}")]
        public IActionResult GetCategoryById(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var category = _myDbContext.Categories.FirstOrDefault(c => c.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }




        [HttpGet]
        [Route("GetCategoryByName/{name}")]
        public IActionResult GetCategoryByName(string name)
        {
            if (name == null)
            {
                return BadRequest();
            }

            var category = _myDbContext.Categories
                .FirstOrDefault(c => c.CategoryName==name);
            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpDelete]
        [Route("DeleteCategory/{id}")]
        public IActionResult DeleteCategory(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var product = _myDbContext.Products.Where(X => X.CategoryId == id).ToList();

            if (product == null)
            {
                return NotFound();
            }
            _myDbContext.Products.RemoveRange(product);
            _myDbContext.SaveChanges();

            var category = _myDbContext.Categories.Include(c => c.Products).FirstOrDefault(c => c.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            _myDbContext.Categories.Remove(category);
            _myDbContext.SaveChanges();

            return NoContent();
        }



        //to post data from swagger
        [HttpPost]
        public IActionResult AddCategory([FromForm] CategoryRequest categoryDTO)
        {
            if (categoryDTO == null)
            {
                return BadRequest("Invalid data.");
            }


            var x = new Category
            {

                CategoryName = categoryDTO.CategoryName,
               CategoryImage = categoryDTO.CategoryImage

            };
            _myDbContext.Categories.Add(x);
            _myDbContext.SaveChanges();

            return Ok("Category added successfully!");
        }



        [HttpPut("{id}")]
        public IActionResult updatCategory(int id, [FromForm] CategoryRequest categoryDTO)

        {
            var existingProduct = _myDbContext.Categories.FirstOrDefault(x => x.CategoryId== id);

            if (existingProduct == null)
            {  return NotFound(); }

            existingProduct.CategoryName = categoryDTO.CategoryName;
            existingProduct.CategoryImage = categoryDTO.CategoryImage;   


            _myDbContext.Update(existingProduct);
            _myDbContext.SaveChanges();

            return Ok(); 

        }
    }
}
