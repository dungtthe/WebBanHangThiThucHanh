using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebBanHang.Models
{

    [Table("Products")]
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(250)]
        [MinLength(5)]
        public string ?Name { get; set; }
        [Required]
        public int? Price { get; set; }

        [Required]
        public int CategoryId {  get; set; }
        [ForeignKey(nameof(CategoryId))]
        public Category ?Category { get; set; }
    }
}
