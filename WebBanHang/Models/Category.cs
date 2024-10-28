using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebBanHang.Models
{

    [Table("Categories")]
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(250)]
        [MinLength(5)]
        public string? Name { get; set; }
        public List<Product>? Products { get; set; }
    }
}
