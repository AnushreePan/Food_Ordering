namespace Food_Ordering.Areas.Category.Models
{
    public class CategoryModel
    {
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public IFormFile File { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedDate { get; set; }
    }
    public class CategoryDropDownModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
