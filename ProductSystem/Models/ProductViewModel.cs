using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProductSystem.Models
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }
        [DisplayName("Product Name")]
        [Required(ErrorMessage = "Product Name is required.")]
        //[StringLength(100, MinimumLength = 3, ErrorMessage = "Product Name must be between 3 and 100 characters.")]
        public string ProductName { get; set; }

        [DisplayName("Product Description")]
        [Required(ErrorMessage = "Product Description is required.")]
       // [StringLength(500, MinimumLength = 10, ErrorMessage = "Product Description must be between 10 and 500 characters.")]
        public string ProductDescription { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        public int CategoryId { get; set; }

        [DisplayName("Category Name")]
        public string CategoryName { get; set; }
    }
}
