using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_API__Task2.DTOs;
using Web_API__Task2.Models;

namespace Web_API__Task2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemController : ControllerBase
    {
        private readonly MyDbContext _myDbContext;

        public CartItemController(MyDbContext myDbContext)
        {
            _myDbContext = myDbContext;
        }




        [HttpGet]
        public IActionResult GetAllData()
        {

            var getData = _myDbContext.CartItems.Select(x =>
            new AddCartItemResponse
            {
                CartId = x.CartId,
                CartItemId = x.CartItemId,
                Quantity = x.Quantity,
                Product = new ProductDTO


                {

                    ProductID = x.Product.ProductId,
                    Price = x.Product.Price,
                    ProductName = x.Product.ProductName,


                }
            }

                );

            return Ok(getData);
        }


        [HttpPost]
        public IActionResult addCartItems([FromBody] AddCartItemRequestDTO CART)
        {
            var data = new CartItem
            {
                CartId = CART.CartId,
                Quantity = CART.Quantity,
                ProductId = CART.ProductId,

            };
            _myDbContext.CartItems.Add(data);
            _myDbContext.SaveChanges();

            return Ok();


        }

        [HttpPut("{id}")]
        public IActionResult UpdateCart(int id, [FromBody] quantityResponse UpdateDto)
        {

            var x = _myDbContext.CartItems.Find(id);
            x.Quantity = UpdateDto.Quantity;
            _myDbContext.CartItems.Update(x);
            _myDbContext.SaveChanges();
            return NoContent();


        }




        [Route("DeleteItem/{id}")]
     [HttpDelete]
        public IActionResult DeleteFromCart(int id)
        {
            if (id <= 0) { return BadRequest(); }

            var y = _myDbContext.CartItems.Find(id);
            if (y == null)
            {
                return NotFound();
            }
            _myDbContext.CartItems.Remove(y);
            _myDbContext.SaveChanges();
            return NoContent();

        }




    }
}
