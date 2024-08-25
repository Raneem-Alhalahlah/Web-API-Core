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

        //task1
        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {

            var productById = _myDbContext.Products
                                              .Include(p => p.Category)
                                              .FirstOrDefault(c => c.ProductId == id);
            return Ok(productById);
        }



        // هون لما ادخل اول شرط الي هو الكاتيجوريايدي رح يروحلي فقط على المنتجات الي رقم الكاتيجوري ايدي الهم حسب الي دخلته ,,,اما ثاني شرط الي للسعر فيه بدخل بالويب السعر الي انا بدي يجيبلي المنتجات الي سعرهم اكبر و الهم الكاتيجوري هيدي اله الي حددنه لو ما اضيف كلمة كاونت رح يطلعلي تفاصيل المنتجات الي تحقق عندهم الشرط لكن كلمة كاونت جابتلي عددهم فقط
        //[HttpGet("{id1}/{Price}")]
        //public IActionResult GetProductById(int id1, int Price)
        //{

        //    var productById = _myDbContext.Products.Where(w=>w.CategoryId == id1 &&  w.Price >Price ).Count();


        //    return Ok(productById);
        //}

    }

}

