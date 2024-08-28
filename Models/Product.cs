using System.ComponentModel.DataAnnotations;

namespace CrudDapper.Models
{
    public class Product
    {
        [Key]
        public Guid NewId { get; set; }

        [Required]
        [Display(Name = "Product Name")]
        public string? Name { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Qty { get; set; }

        [Display(Name = "Prodcut Description")]
        public string? Description { get; set; }

        [Display(Name = "Create Date")]
        public DateTime? CreatedOn { get; set; }

        [Display(Name = "Update Date")]
        public DateTime? UpdateOn { get; set; }

    }
}
