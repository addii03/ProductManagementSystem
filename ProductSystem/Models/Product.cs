using System.ComponentModel.DataAnnotations;

namespace ProductSystem.Models
{
    public class Product
    {
        [Key]      
        public int ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string ProductDescription { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

    }
}
