using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task1_WepAPICore.Models;

namespace Task1_WepAPICore.Controllers
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
        public IActionResult getAllCategories() {

            var categories = _myDbContext.Categories.ToList();  
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public IActionResult GetCategoryById(int id)
        {

            var CategoryById = _myDbContext.Categories.Where(c => c.CategoryId == id).FirstOrDefault();
            return Ok(CategoryById);
        }
    }
}
