using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebBanHang.Models
{
    [Table("Roles")]
    public class Role
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Tên vai trò không được để trống")]
        [MaxLength(100)]
        public string? RoleName {  get; set; }
        public List<AppUser>? Users { get; set; }
    }
}
