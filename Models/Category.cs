using System.ComponentModel.DataAnnotations;

namespace Mission08_Team0101.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        public string CategoryName { get; set; } = string.Empty;
    }
}