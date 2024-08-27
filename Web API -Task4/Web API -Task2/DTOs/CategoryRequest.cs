namespace Web_API__Task2.DTOs
{
    public class CategoryRequest
    {
        public string? CategoryName { get; set; }

        public IFormFile? CategoryImage { get; set; }
    }
}
