using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProductSystem.Models
{
    public class CategoryViewModel
    {
        public int CategoryId { get; set; }

        [DisplayName("Category Name")]
        [Required(ErrorMessage = "Category Name is required.")]
        public string CategoryName { get; set; }

        [DisplayName("Category Description")]
        [Required(ErrorMessage = "Category Description is required.")]
        public string CategoryDescription { get; set; }
    }
}
