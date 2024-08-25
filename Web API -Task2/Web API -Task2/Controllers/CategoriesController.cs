using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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




        [HttpGet("{id:int}")]
        [Route("GetCategoryById/{id}")]
        public IActionResult GetCategoryById(int id)
        {
            if (id < 5)
            {
                return BadRequest("ID must be 5 or greater.");
            }

            var category = _myDbContext.Categories.FirstOrDefault(c => c.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }




        [HttpGet("byname/{name}")]
        [Route("GetCategoryByName/{name}")]
        public IActionResult GetCategoryByName(string name)
        {
            var category = _myDbContext.Categories
                .FirstOrDefault(c => c.CategoryName.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpDelete("{id:int}")]
        [Route("DeleteCategory/{id}")]
        public IActionResult DeleteCategory(int id)
        {
            var category = _myDbContext.Categories.Include(c => c.Products).FirstOrDefault(c => c.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            if (category.Products.Any())
            {
                return BadRequest("Cannot delete category because it contains products.");
            }

            _myDbContext.Categories.Remove(category);
            _myDbContext.SaveChanges();

            return NoContent();
        }




    }
}
