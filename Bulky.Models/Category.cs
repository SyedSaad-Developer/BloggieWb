using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyBook.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)] /*Validation: Category name must be less than 30*/
        [DisplayName("Category Name")]
        public string? Name { get; set; }
        [DisplayName("Display Order")]
        [Range(1, 100, ErrorMessage = "Display Order must be between 1-100")] /*Validation: Display Order range is from 1 to 100*/
        public int DisplayOrder { get; set; }

    }
}
