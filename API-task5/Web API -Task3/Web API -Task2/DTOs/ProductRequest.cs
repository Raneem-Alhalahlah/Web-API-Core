namespace Web_API__Task2.DTOs
{
    public class ProductRequest
    {
        public string? ProductName { get; set; }

        public string? Description { get; set; }

        public int? Price { get; set; }

        public int? CategoryId { get; set; }

        public IFormFile? ProductImage { get; set; }
    }
}
