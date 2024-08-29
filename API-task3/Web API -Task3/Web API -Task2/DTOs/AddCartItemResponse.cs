using Web_API__Task2.Models;

namespace Web_API__Task2.DTOs
{
    public class AddCartItemResponse
    {
        public int CartItemId { get; set; }

        public int? CartId { get; set; }

       

        public int Quantity { get; set; }


        public virtual ProductDTO? Product { get; set; }
    }

    public class ProductDTO
    {
        public int? ProductID { get; set; }

        public string? ProductName { get; set; }


        public int? Price { get; set; }

       

       
    }





}
