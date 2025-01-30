using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ProductSystem.Models
{
    public class ProductEditModel
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

        //[Required(ErrorMessage = "Category is required.")]
        [DisplayName("Category Name")]
        public int CategoryId { get; set; }
    }
}
