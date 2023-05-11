using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Food_Ordering.Areas.Products.Models

{
    public class ProductsModel
    {
        public int? ProductId { get; set; }
        [Required(ErrorMessage = "Product Name is Required")]
        public string ProductName { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Price is Required")]
        public Decimal Price { get; set; }
        [Required(ErrorMessage = "Quantity Name is Required")]
        public int Quantity { get; set; }
        public IFormFile File { get; set; }
        public string ImageUrl { get; set; }
        [Required(ErrorMessage = "Select Category")]
        public int CategoryId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
