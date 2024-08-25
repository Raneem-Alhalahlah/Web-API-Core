using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        //[HttpGet]
        //public IActionResult getAllCategories()
        //{

        //    var categories = _myDbContext.Categories.ToList();
        //    return Ok(categories);
        //}

        //[HttpGet("{id}")]
        //public IActionResult GetCategoryById(int id)
        //{

        //    var CategoryById = _myDbContext.Categories.Where(c => c.CategoryId == id).FirstOrDefault();
        //    return Ok(CategoryById);
        //}


        //[HttpGet("{name:alpha}")]
        //public IActionResult GetCategoryByName(string name)
        //{

        //    var CategoruByName = _myDbContext.Categories.Where(c => c.CategoryName == name).FirstOrDefault();
        //    return Ok(CategoruByName);

        //}


        [HttpDelete("{id1}")]
        public IActionResult DeleteCategoryById(int id1)
        {
            var Deleteitem = _myDbContext.Categories.Include(x => x.Products).FirstOrDefault(c => c.CategoryId == id1);
            if (Deleteitem == null)
            {
                return NotFound();

            }

            if (Deleteitem.Products.Any())
            {
                return BadRequest(" يوجد منتجات داخل هذا الصنف");
            }

            _myDbContext.Categories.Remove(Deleteitem);
            _myDbContext.SaveChanges();

            return NoContent();
        }




        //[HttpGet]
        //public IActionResult GetCategories([FromQuery] string name)
        //{
        //    if (!string.IsNullOrEmpty(name))
        //    {
        //        var categories = _myDbContext.Categories
        //            .Where(c => c.CategoryName.Contains(name))
        //            .ToList();

        //        if (categories.Count == 0)
        //        {
        //            return NotFound("لا توجد فئات بهذا الاسم.");
        //        }

        //        return Ok(categories);
        //    }


        //    var allCategories = _myDbContext.Categories.ToList();
        //    return Ok(allCategories);
        //}






    }
}
